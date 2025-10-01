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

#ifndef MS_ROOT_MODULE_H
#define MS_ROOT_MODULE_H

#include <cstdint>
#include <functional>
#include <string>
#include <unordered_map>
#include <vector>

struct CAddress
{
    CAddress() = default;
    CAddress(const void* val) :
        m_nValue(reinterpret_cast<uintptr_t>(val)) {}
    CAddress(const uintptr_t ptr) :
        m_nValue(ptr) {}

    operator uintptr_t() const noexcept
    {
        return m_nValue;
    }

    operator void*() const noexcept
    {
        return reinterpret_cast<void*>(m_nValue);
    }

    operator uint8_t*() const noexcept
    {
        return reinterpret_cast<uint8_t*>(m_nValue);
    }

    explicit operator bool() const noexcept
    {
        return m_nValue != 0;
    }

    template <typename T = void*>
    T As() noexcept
    {
        return T(m_nValue);
    }

    template <typename T = void*>
    T Get() noexcept
    {
        return *reinterpret_cast<T*>(m_nValue);
    }

    [[nodiscard]] CAddress Offset(int offset) const noexcept
    {
        return m_nValue + offset;
    }

    [[nodiscard]] CAddress ToAbsolute(intptr_t offset = 0x1) const noexcept
    {
        auto displacement = *reinterpret_cast<int32_t*>(m_nValue + offset);
        return m_nValue + offset + sizeof(int) + displacement;
    }

    CAddress Dereference(int dereferences = 1) noexcept
    {
        if (m_nValue)
            while (dereferences-- != 0) m_nValue = *reinterpret_cast<uintptr_t*>(m_nValue);
        return m_nValue;
    }

    [[nodiscard]] bool IsValid() const
    {
        return m_nValue >= 0x1000 && m_nValue < 0x7FFFFFFEFFFF;
    }

    template <std::integral T>
    bool operator==(T val) const noexcept
    {
        return m_nValue == static_cast<uintptr_t>(val);
    }

    template <std::integral T>
    bool operator!=(T val) const noexcept
    {
        return !(*this == val);
    }

    bool operator==(void* val) const
    {
        return (m_nValue == reinterpret_cast<uintptr_t>(val));
    }

    bool operator!=(void* val) const
    {
        return *this != reinterpret_cast<uintptr_t>(val);
    }

    uintptr_t GetPtr() const
    {
        return m_nValue;
    }

private:
    uintptr_t m_nValue{};
};

class IModule
{
public:
    virtual ~IModule() = default;

    [[nodiscard]] virtual void* FindPatternEx(const char* svPattern)                                      = 0;
    [[nodiscard]] virtual void* GetVirtualTableByNameEx(const char* svTableName, bool bDecorated = false) = 0;
    [[nodiscard]] virtual void* GetFunctionByNameEx(const char* svFunctionName) noexcept                  = 0;
    [[nodiscard]] virtual void* FindInterfaceEx(const char* svInterfaceName)                              = 0;
    [[nodiscard]] virtual void* FindPatternMultiEx(const char* svPattern, int* count)                     = 0;
    [[nodiscard]] virtual void* FindStringEx(const char* str)                                             = 0;
    [[nodiscard]] virtual void* FindPtrEx(const void* ptr)                                                = 0;
};

enum SegmentFlags : uint8_t
{
    FLAG_R = 1 << 0,
    FLAG_W = 1 << 1,
    FLAG_X = 1 << 2,
};

constexpr SegmentFlags operator|(SegmentFlags a, SegmentFlags b)
{
    return static_cast<SegmentFlags>(static_cast<uint8_t>(a) | static_cast<uint8_t>(b));
}

class CModule final : public IModule
{
public:
    struct FunctionEntry
    {
        uintptr_t start;
        uintptr_t end{};
    };

    struct Segments
    {
        Segments()                           = default;
        Segments(Segments&&)                 = default;
        Segments(const Segments&)            = delete;
        Segments& operator=(const Segments&) = delete;
        Segments& operator=(Segments&&)      = delete;

        uint8_t                   flags{};
        uintptr_t                 address{};
        std::size_t               size{};
        std::vector<uint8_t>      data{};
    };

private:
    std::vector<Segments> _segments{};
    uintptr_t             _base_address{};
    std::size_t           _size{};
    std::string           _module_name{};
    void*                 _createInterFaceFn;

    std::unordered_map<std::string, uintptr_t> _cached_vtables{};

    void GetModuleInfo(std::string_view mod);

#ifndef PLATFORM_WINDOWS
    std::unordered_map<std::string, uintptr_t>      _exports{};
    void                                            DumpExports(void* module_base);
#endif

public:
    CModule()                   = default;
    CModule(const CModule&)     = delete;
    CModule(CModule&&) noexcept = delete;

    CModule& operator=(const CModule&) = delete;
    CModule& operator=(CModule&&)      = delete;

    explicit CModule(std::string_view str);

    // get base address of a module
    [[nodiscard]] uintptr_t Base() const
    {
        return _base_address;
    }

    [[nodiscard]] std::string_view ModuleName() const
    {
        return _module_name;
    }

#ifdef PLATFORM_WINDOWS
    int GetVTableCount(const std::string& szVtableName);
#endif

    void LoopVFunctions(const std::string& vtable_name, const std::function<bool(CAddress)>& callback);

    [[nodiscard]] CAddress              FindPattern(std::string_view pattern) const;
    [[nodiscard]] CAddress              FindPatternStrict(std::string_view pattern) const;
    [[nodiscard]] CAddress              FindString(const std::string& str, bool read_only) const;
    [[nodiscard]] CAddress              FindPtr(std::uintptr_t ptr) const;
    [[nodiscard]] std::vector<CAddress> FindPtrs(std::uintptr_t ptr) const;
    [[nodiscard]] std::vector<CAddress> FindRVAs(uint32_t rva) const;
    [[nodiscard]] CAddress              GetVirtualTableByName(const std::string& name, bool decorated = false);
    [[nodiscard]] CAddress              FindInterface(std::string_view name) const;
    [[nodiscard]] std::vector<CAddress> FindPatternMulti(std::string_view svPattern) const;
    [[nodiscard]] CAddress              GetFunctionByName(std::string_view proc_name) const;

    // interface
    [[nodiscard]] void* FindPatternEx(const char* svPattern) override
    {
        return FindPattern(svPattern);
    }

    [[nodiscard]] void* GetVirtualTableByNameEx(const char* svTableName, bool bDecorated = false) override
    {
        return GetVirtualTableByName(svTableName);
    }

    [[nodiscard]] void* GetFunctionByNameEx(const char* svFunctionName) noexcept override
    {
        return GetFunctionByName(svFunctionName);
    }

    [[nodiscard]] void* FindInterfaceEx(const char* svInterfaceName) override
    {
        return FindInterface(svInterfaceName);
    }

    [[nodiscard]] void* FindPatternMultiEx(const char* svPattern, int* count) override
    {
        auto result = FindPatternMulti(svPattern);
        *count      = static_cast<int>(result.size());
        if (result.empty())
        {
            return nullptr;
        }
        return result.data();
    }

    [[nodiscard]] void* FindStringEx(const char* str) override
    {
        return FindString(str, false);
    }

    [[nodiscard]] void* FindPtrEx(const void* ptr) override
    {
        return FindPtr(reinterpret_cast<uintptr_t>(ptr));
    }
};

#endif