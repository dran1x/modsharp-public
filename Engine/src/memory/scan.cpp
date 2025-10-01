/* 
 * ModSharp
 * Copyright (C) 2023-2025 Kxnrl. All Rights Reserved.
 *
 * This file is part of ModSharp.
 * ModSharp is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License as
 * published by the Free Software Foundation, either version 3 of the
 * License, or (at your option) any later version.
 *
 * ModSharp is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.
 *
 * You should have received a copy of the GNU Affero General Public License
 * along with ModSharp. If not, see <https://www.gnu.org/licenses/>.
 */

#include "scan.h"

#include "module.h"
#include <algorithm>
#include <bit>
#include <cstring>
#include <immintrin.h>
#include <ranges>

#ifdef PLATFORM_WINDOWS
#    include <array>
#    include <bitset>
#    include <intrin.h>
#else
#    include <cpuid.h>
#endif

struct Pattern
{
    Pattern() = default;

    static Pattern FromHexString(std::string_view input)
    {
        if (input.empty())
            [[unlikely]]
            throw;
        Pattern p{};

        static constexpr auto hex_char_to_byte = [](char c) -> int8_t {
            if (c >= '0' && c <= '9')
                return c - '0';
            if (c >= 'A' && c <= 'F')
                return c - 'A' + 10;
            if (c >= 'a' && c <= 'f')
                return c - 'a' + 10;
            return -1;
        };

        p._bytes.reserve(input.size() / 3 + 1);

        for (auto&& str : input | std::views::split(' '))
        {
            if (str.empty())
                [[unlikely]]
            {
                continue;
            }

            std::string_view byte(&*str.begin(), std::ranges::distance(str));

            if (byte[0] == '?')
            {
                p._bytes.push_back(-1);
                /*_has_wildcard = true;*/
                continue;
            }

            const auto high = hex_char_to_byte(byte[0]);
            const auto low  = hex_char_to_byte(byte[1]);

            p._bytes.push_back((high << 4) | low);
        }

        auto rit = std::ranges::find_if(std::ranges::reverse_view(p._bytes), [](auto n) { return n != -1; });

        // Erase all -1 elements from the end
        p._bytes.erase(rit.base(), p._bytes.end());
        return p;
    }

    static Pattern FromString(std::string_view input, bool zero_terminated = false)
    {
        Pattern p{};

        for (const char c : input)
        {
            p._bytes.push_back(c);
        }

        if (zero_terminated)
            p._bytes.push_back(0);

        return p;
    }

    [[nodiscard]] const std::vector<int16_t>& bytes() const
    {
        return _bytes;
    }

    bool is_match(uint8_t* data) const
    {
        return std::equal(_bytes.begin() + 1,
                          _bytes.end() - 1,
                          data + 1,
                          [](auto opt, auto byte) {
                              return opt == -1 || opt == byte;
                          });
    }

private:
    std::vector<int16_t> _bytes{};
};

class InstructionSet
{
public:
    InstructionSet()
    {
#ifndef PLATFORM_WINDOWS
        _supportAvx2 = __builtin_cpu_supports("avx2");
#else

        std::array<int, 4>               cpui{};
        std::bitset<32>                  f_1_ECX_{};
        std::bitset<32>                  f_1_EDX_{};
        std::bitset<32>                  f_7_EBX_{};
        std::vector<std::array<int, 4> > data_{};

        // Calling __cpuid with 0x0 as the function_id argument
        // gets the number of the highest valid function ID.
        __cpuid(cpui.data(), 0);
        auto nIds_ = cpui[0];
        data_.reserve(nIds_);

        for (int i = 0; i <= nIds_; ++i)
        {
            __cpuidex(cpui.data(), i, 0);
            data_.push_back(cpui);
        }

        // load bitset with flags for function 0x00000001
        if (nIds_ >= 1)
        {
            f_1_ECX_ = data_[1][2];
        }

        // load bitset with flags for function 0x00000007
        if (nIds_ >= 7)
        {
            f_7_EBX_ = data_[7][1];
        }

        if (f_1_ECX_[28] && f_7_EBX_[5])
        {
            _supportAvx2 = true;
        }
#endif
    }

    [[nodiscard]] bool SupportAvx2() const
    {
        return _supportAvx2;
    }

private:
    bool _supportAvx2{};
};

static InstructionSet s_InstructionSet{};

#if defined(__clang__) || defined(__GNUC__) || defined(__GNUG__)
#    define ATTRIBUTE_AVX2 __attribute__((target("avx2")))
#    define ATTRIBUTE_SSE __attribute__((target("sse4.1")))
#else
#    define ATTRIBUTE_AVX2
#    define ATTRIBUTE_SSE
#endif

namespace detail
{
enum class SearchAction : uint8_t
{
    Continue,
    Stop,
};

template <typename Callback>
static void FindPatternScalar(uint8_t* data, std::size_t size, const std::vector<int16_t>& pattern, Callback on_match) noexcept
{
    const auto patternSize = pattern.size();
    uint8_t*   end         = data + size - patternSize;
    const auto firstByte   = pattern[0];

    for (uint8_t* current = data; current <= end; ++current)
    {
        current = std::find(current, end, firstByte);

        if (current == end)
        {
            break;
        }

        if (std::equal(pattern.begin() + 1,
                       pattern.end(),
                       current + 1,
                       [](auto opt, auto byte) {
                           return opt == -1 || opt == byte;
                       }))
        {
            if (on_match(current) == SearchAction::Stop)
                return;
        }
    }
}

// Implementation from: http://0x80.pl/notesen/2016-11-28-simd-strfind.html

template <typename Callback>
static void FindPatternSSEImpl(uint8_t* data, std::size_t size, const Pattern& pattern, Callback on_match)
{
    constexpr auto type_size     = sizeof(__m128i);
    const auto&    pattern_bytes = pattern.bytes();
    const auto     pattern_size  = pattern_bytes.size();

    if (pattern_size < 2 || size < pattern_size) [[unlikely]]
    {
        FindPatternScalar(data, size, pattern_bytes, on_match);
        return;
    }

    const auto first = _mm_set1_epi8(static_cast<char>(pattern_bytes.front()));
    const auto last  = _mm_set1_epi8(static_cast<char>(pattern_bytes.back()));

    const auto bytes_to_search = size - pattern_size;

    size_t i = 0;
    while (i + type_size <= bytes_to_search)
    {
        const auto block_first = _mm_loadu_si128(reinterpret_cast<const __m128i*>(data + i));
        const auto block_last  = _mm_loadu_si128(reinterpret_cast<const __m128i*>(data + i + pattern_size - 1));

        const auto eq_first = _mm_cmpeq_epi8(first, block_first);
        const auto eq_last  = _mm_cmpeq_epi8(last, block_last);

        uint32_t mask = _mm_movemask_epi8(_mm_and_si128(eq_first, eq_last));

        while (mask != 0)
        {
            const uint32_t bit_pos = std::countr_zero(mask);
            auto           current = data + i + bit_pos;
            if (pattern.is_match(current)) [[unlikely]]
            {
                if (on_match(current) == SearchAction::Stop)
                {
                    return;
                }
            }
            mask = mask & (mask - 1);
        }

        i += type_size;
    }

    FindPatternScalar(data + i, size - i, pattern_bytes, on_match);
}

template <typename Callback>
ATTRIBUTE_AVX2 static void FindPatternAvx2Impl(uint8_t* data, std::size_t size, const Pattern& pattern, Callback on_match)
{
    constexpr auto type_size     = sizeof(__m256i);
    const auto&    pattern_bytes = pattern.bytes();
    const auto     pattern_size  = pattern_bytes.size();

    if (pattern_size < 2 || size < pattern_size) [[unlikely]]
    {
        FindPatternScalar(data, size, pattern_bytes, on_match);
        return;
    }

    const auto first = _mm256_set1_epi8(static_cast<char>(pattern_bytes.front()));
    const auto last  = _mm256_set1_epi8(static_cast<char>(pattern_bytes.back()));

    const auto bytes_to_search = size - pattern_size;

    size_t i = 0;
    while (i + type_size <= bytes_to_search)
    {
        const auto block_first = _mm256_loadu_si256(reinterpret_cast<const __m256i*>(data + i));
        const auto block_last  = _mm256_loadu_si256(reinterpret_cast<const __m256i*>(data + i + pattern_size - 1));

        const auto eq_first = _mm256_cmpeq_epi8(first, block_first);
        const auto eq_last  = _mm256_cmpeq_epi8(last, block_last);

        uint32_t mask = _mm256_movemask_epi8(_mm256_and_si256(eq_first, eq_last));

        while (mask != 0)
        {
            const uint32_t bit_pos = std::countr_zero(mask);
            auto           current = data + i + bit_pos;
            if (pattern.is_match(current)) [[unlikely]]
            {
                if (on_match(current) == SearchAction::Stop)
                {
                    return;
                }
            }
            mask = mask & (mask - 1);
        }

        i += type_size;
    }

    FindPatternScalar(data + i, size - i, pattern_bytes, on_match);
}

static CAddress FindPatternSSE(uint8_t* data, std::size_t size, const Pattern& pattern)
{
    CAddress   result{};
    const auto base = reinterpret_cast<uintptr_t>(data);

    FindPatternSSEImpl(data, size, pattern,
                       [&result, base](CAddress match) {
                           result = match - base;
                           return detail::SearchAction::Stop;
                       });
    return result;
}

static std::vector<CAddress> FindPatternMultiSSE(uint8_t* data, std::size_t size, const Pattern& pattern)
{
    std::vector<CAddress> results{};
    results.reserve(8);

    const auto base = reinterpret_cast<uintptr_t>(data);

    FindPatternSSEImpl(data, size, pattern,
                       [&results, base](CAddress match) {
                           results.emplace_back(match - base);
                           return detail::SearchAction::Continue;
                       });
    return results;
}

ATTRIBUTE_AVX2
static CAddress FindPatternAVX2(uint8_t* data, std::size_t size, const Pattern& pattern)
{
    CAddress   result{};
    const auto base = reinterpret_cast<uintptr_t>(data);

    FindPatternAvx2Impl(data, size, pattern,
                        [&result, base](CAddress match) {
                            result = match - base;
                            return detail::SearchAction::Stop;
                        });
    return result;
}

ATTRIBUTE_AVX2
static std::vector<CAddress> FindPatternMultiAVX2(uint8_t* data, std::size_t size, const Pattern& pattern)
{
    std::vector<CAddress> results{};
    results.reserve(8);

    const auto base = reinterpret_cast<uintptr_t>(data);

    FindPatternAvx2Impl(data, size, pattern,
                        [&results, base](CAddress match) {
                            results.emplace_back(match - base);
                            return detail::SearchAction::Continue;
                        });
    return results;
}

template <typename T, typename Callback>
ATTRIBUTE_SSE static void FindValueSSEImpl(std::uintptr_t data, std::size_t size, T value, Callback on_match) noexcept
{
    constexpr auto value_size = sizeof(T);
    constexpr auto simd_size  = sizeof(__m128i);

    auto current = reinterpret_cast<uint8_t*>(data);
    auto end     = reinterpret_cast<uint8_t*>(data + size - value_size + 1);

    __m128i v_value;
    if constexpr (value_size == 8)
    {
        v_value = _mm_set1_epi64x(static_cast<long long>(value));
    }
    else
    {
        v_value = _mm_set1_epi32(static_cast<int>(value));
    }

    while (current + simd_size <= end)
    {
        const auto v_data = _mm_loadu_si128(reinterpret_cast<const __m128i*>(current));
        __m128i    v_cmp;
        uint32_t   mask;
        if constexpr (value_size == 8)
        {
            v_cmp = _mm_cmpeq_epi64(v_data, v_value);
            mask  = _mm_movemask_pd(_mm_castsi128_pd(v_cmp));
        }
        else
        {
            v_cmp = _mm_cmpeq_epi32(v_data, v_value);
            mask  = _mm_movemask_ps(_mm_castsi128_ps(v_cmp));
        }

        while (mask != 0)
        {
            const auto element_index = std::countr_zero(mask);
            auto       offset        = (reinterpret_cast<std::uintptr_t>(current) + element_index * value_size) - data;

            if (on_match(offset) == SearchAction::Stop)
                return;
            mask &= mask - 1;
        }
        current += simd_size;
    }

    while (current < end)
    {
        if (*reinterpret_cast<T*>(current) == value)
        {
            const auto offset = reinterpret_cast<std::uintptr_t>(current) - data;
            if (on_match(offset) == SearchAction::Stop)
            {
                return;
            }
        }
        ++current;
    }
}

template <typename T, typename Callback>
ATTRIBUTE_AVX2 static void FindValueAVX2Impl(std::uintptr_t data, std::size_t size, T value, Callback on_match) noexcept
{
    constexpr auto value_size = sizeof(T);
    constexpr auto simd_size  = sizeof(__m256i);

    auto current = reinterpret_cast<uint8_t*>(data);
    auto end     = reinterpret_cast<uint8_t*>(data + size - value_size + 1);

    __m256i v_value;
    if constexpr (value_size == 8)
    {
        v_value = _mm256_set1_epi64x(static_cast<long long>(value));
    }
    else
    {
        v_value = _mm256_set1_epi32(static_cast<int>(value));
    }

    while (current + simd_size <= end)
    {
        const auto v_data = _mm256_loadu_si256(reinterpret_cast<const __m256i*>(current));
        __m256i    v_cmp;
        uint32_t   mask;
        if constexpr (value_size == 8)
        {
            v_cmp = _mm256_cmpeq_epi64(v_data, v_value);
            mask  = _mm256_movemask_pd(_mm256_castsi256_pd(v_cmp));
        }
        else
        {
            v_cmp = _mm256_cmpeq_epi32(v_data, v_value);
            mask  = _mm256_movemask_ps(_mm256_castsi256_ps(v_cmp));
        }

        while (mask != 0)
        {
            const auto element_index = std::countr_zero(mask);
            auto       offset        = (reinterpret_cast<std::uintptr_t>(current) + element_index * value_size) - data;

            if (on_match(offset) == SearchAction::Stop)
                return;
            mask &= mask - 1;
        }
        current += simd_size;
    }

    while (current < end)
    {
        if (*reinterpret_cast<T*>(current) == value)
        {
            const auto offset = reinterpret_cast<std::uintptr_t>(current) - data;
            if (on_match(offset) == SearchAction::Stop)
            {
                return;
            }
        }
        ++current;
    }
}
} // namespace detail

CAddress scan::FindPattern(uint8_t* data, std::size_t size, std::string_view pattern) noexcept
{
    auto pat = Pattern::FromHexString(pattern);

    if (s_InstructionSet.SupportAvx2())
        return detail::FindPatternAVX2(data, size, pat);

    return detail::FindPatternSSE(data, size, pat);
}

std::vector<CAddress> scan::FindPatternMulti(uint8_t* data, std::size_t size, std::string_view pattern) noexcept
{
    auto pat = Pattern::FromHexString(pattern);

    if (s_InstructionSet.SupportAvx2())
        return detail::FindPatternMultiAVX2(data, size, pat);

    return detail::FindPatternMultiSSE(data, size, pat);
}

CAddress scan::FindStr(uint8_t* data, std::size_t size, const std::string& str, bool zero_terminated) noexcept
{
    auto pat = Pattern::FromString(str, zero_terminated);

    if (s_InstructionSet.SupportAvx2())
        return detail::FindPatternAVX2(data, size, pat);

    return detail::FindPatternSSE(data, size, pat);
}

CAddress scan::FindPtr(std::uintptr_t data, std::size_t size, std::uintptr_t ptr) noexcept
{
    CAddress result{};
    auto     callback = [&result](CAddress address) {
        result = address;
        return detail::SearchAction::Stop;
    };

    if (s_InstructionSet.SupportAvx2())
    {
        detail::FindValueAVX2Impl(data, size, ptr, callback);
    }
    else
    {
        detail::FindValueSSEImpl(data, size, ptr, callback);
    }

    return result;
}

CAddress scan::FindRVA(std::uintptr_t data, std::size_t size, uint32_t rva) noexcept
{
    CAddress result{};
    auto     callback = [&result](CAddress address) {
        result = address;
        return detail::SearchAction::Stop;
    };

    if (s_InstructionSet.SupportAvx2())
    {
        detail::FindValueAVX2Impl(data, size, rva, callback);
    }
    else
    {
        detail::FindValueSSEImpl(data, size, rva, callback);
    }

    return result;
}

std::vector<CAddress> scan::FindRVAs(std::uintptr_t data, std::size_t size, uint32_t rva) noexcept
{
    std::vector<CAddress> result{};
    auto                  callback = [&result](CAddress address) {
        result.emplace_back(address);
        return detail::SearchAction::Continue;
    };

    if (s_InstructionSet.SupportAvx2())
    {
        detail::FindValueAVX2Impl(data, size, rva, callback);
    }
    else
    {
        detail::FindValueSSEImpl(data, size, rva, callback);
    }

    return result;
}

std::vector<CAddress> scan::FindPtrs(std::uintptr_t data, std::size_t size, std::uintptr_t ptr) noexcept
{
    std::vector<CAddress> result{};
    auto                  callback = [&result](CAddress address) {
        result.emplace_back(address);
        return detail::SearchAction::Continue;
    };

    if (s_InstructionSet.SupportAvx2())
    {
        detail::FindValueAVX2Impl(data, size, ptr, callback);
    }
    else
    {
        detail::FindValueSSEImpl(data, size, ptr, callback);
    }

    return result;
}
