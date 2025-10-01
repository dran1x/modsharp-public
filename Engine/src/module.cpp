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

#include "module.h"
#include "global.h"
#include "memory/scan.h"

#include <algorithm>

CModule::CModule(std::string_view str)
{
    GetModuleInfo(str);
}

CAddress CModule::FindPattern(std::string_view pattern) const
{
    for (auto&& segment : _segments)
    {
        if ((segment.flags & FLAG_X) == 0)
            continue;

        const auto& data = segment.data;

        if (auto result = scan::FindPattern(const_cast<uint8_t*>(data.data()), data.size(), pattern))
        {
            if (result > 0)
                return segment.address + result;
        }
    }

    return {};
}

CAddress CModule::FindPatternStrict(std::string_view pattern) const
{
    const auto& result = FindPatternMulti(pattern);
    if (result.empty() || result.size() > 1)
        return {};
    return result[0];
}

CAddress CModule::FindString(const std::string& str, bool read_only) const
{
    for (auto&& segment : _segments)
    {
        if ((segment.flags & FLAG_X) != 0)
            continue;

        if (read_only && (segment.flags & FLAG_W) != 0)
            continue;

        if (auto result = scan::FindStr(reinterpret_cast<uint8_t*>(segment.address), segment.size, str, true))
        {
            if (result > 0)
                return segment.address + result;
        }
    }

    return {};
}

CAddress CModule::FindPtr(uintptr_t ptr) const
{
    for (const auto& segment : _segments)
    {
        const auto flags = segment.flags;

        if ((flags & FLAG_X) != 0)
            continue;

        auto res = scan::FindPtr(segment.address, segment.size, ptr);
        if (res > 0)
            return res + segment.address;
    }

    return {};
}

std::vector<CAddress> CModule::FindPtrs(std::uintptr_t ptr) const
{
    std::vector<CAddress> results{};

    for (auto&& segment : _segments)
    {
        if ((segment.flags & FLAG_X) != 0)
            continue;

        auto ptrs = scan::FindPtrs(segment.address, segment.size, ptr);
        if (ptrs.empty())
            continue;

        for (auto temp : ptrs)
        {
            results.emplace_back(temp + segment.address);
        }
    }

    if (!results.empty())
    {
        return results;
    }

    for (auto&& segment : _segments)
    {
        if ((segment.flags & FLAG_X) != 0)
            continue;

        const auto& data  = segment.data;
        const auto  start = reinterpret_cast<uintptr_t>(data.data());

        auto ptrs = scan::FindPtrs(start, data.size(), ptr);
        if (ptrs.empty())
            continue;

        for (auto temp : ptrs)
        {
            results.emplace_back(temp + segment.address);
        }
    }

    return results;
}

std::vector<CAddress> CModule::FindRVAs(uint32_t rva) const
{
    std::vector<CAddress> results{};

    for (auto&& segment : _segments)
    {
        if ((segment.flags & FLAG_X) != 0)
            continue;

        auto ptrs = scan::FindRVAs(segment.address, segment.size, rva);
        if (ptrs.empty())
            continue;

        for (auto temp : ptrs)
        {
            results.emplace_back(temp + segment.address);
        }
    }

    return results;
}

CAddress CModule::FindInterface(std::string_view name) const
{
    return reinterpret_cast<CreateInterface_t>(_createInterFaceFn)(name.data(), nullptr);
}

std::vector<CAddress> CModule::FindPatternMulti(std::string_view svPattern) const
{
    for (auto&& segment : _segments)
    {
        if ((segment.flags & FLAG_X) == 0)
            continue;

        const auto& data = segment.data;

        auto result = scan::FindPatternMulti(const_cast<uint8_t*>(data.data()), data.size(), svPattern);
        if (!result.empty())
        {
            std::ranges::transform(result, result.begin(), [&](CAddress address) {
                return address + segment.address;
            });

            return result;
        }
    }

    return {};
}

void CModule::LoopVFunctions(const std::string& vtable_name, const std::function<bool(CAddress)>& callback)
{
    auto vtable = GetVirtualTableByName(vtable_name);
    if (!vtable.IsValid())
        return;

    uintptr_t segmentStart = 0;
    uintptr_t segmentEnd   = 0;

    for (const auto& segment : _segments)
    {
        if ((segment.flags & FLAG_X) != 0)
        {
            segmentStart = segment.address;
            segmentEnd   = segment.address + segment.size;
            break;
        }
    }

    for (;;)
    {
        auto address = vtable.Get<uintptr_t>();
        if (address < segmentStart || address > segmentEnd)
            return;

        if (callback(address))
            return;

        vtable = vtable.Offset(sizeof(uintptr_t));
    }
}