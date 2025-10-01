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

#ifdef PLATFORM_WINDOWS

#    include "logging.h"
#    include "module.h"

#    include <algorithm>
#    include <cinttypes>
#    include <format>
#    include <iostream>

#    include <windows.h>
#    include <winternl.h>

void CModule::GetModuleInfo(std::string_view mod)
{
    HMODULE handle = GetModuleHandleA(mod.data());
    if (!handle)
        return;

    _base_address = reinterpret_cast<uintptr_t>(handle);

    const auto dosHeader = reinterpret_cast<PIMAGE_DOS_HEADER>(handle);

    if (dosHeader->e_magic != IMAGE_DOS_SIGNATURE)
        return;

    const auto bytes = reinterpret_cast<uint8_t*>(handle);

    const auto ntHeader = reinterpret_cast<PIMAGE_NT_HEADERS>(bytes + dosHeader->e_lfanew);

    if (ntHeader->Signature != IMAGE_NT_SIGNATURE)
        return;

    auto section = IMAGE_FIRST_SECTION(ntHeader);

    for (auto i = 0u; i < ntHeader->FileHeader.NumberOfSections; i++, section++)
    {
        const auto isExecutable = (section->Characteristics & IMAGE_SCN_MEM_EXECUTE) != 0;
        const auto isReadable   = (section->Characteristics & IMAGE_SCN_MEM_READ) != 0;
        const auto isWritable   = (section->Characteristics & IMAGE_SCN_MEM_WRITE) != 0;

        const auto start = this->_base_address + section->VirtualAddress;
        const auto size  = section->Misc.VirtualSize;

        auto& segment   = _segments.emplace_back();
        segment.address = start;
        segment.size    = size;
        if (isExecutable)
            segment.flags |= FLAG_X;
        if (isReadable)
            segment.flags |= FLAG_R;
        if (isWritable)
            segment.flags |= FLAG_W;

        const auto data = reinterpret_cast<uint8_t*>(start);
        segment.data    = std::vector(&data[0], &data[size]);
    }

    _createInterFaceFn = GetFunctionByName("CreateInterface");
}

CAddress CModule::GetVirtualTableByName(const std::string& name, bool decorated)
{
    if (const auto it = _cached_vtables.find(name); it != _cached_vtables.end())
    {
        return it->second;
    }

    auto vtable_name     = decorated ? name : std::format(".?AV{}@@", name);
    auto type_descriptor = FindString(vtable_name, false);
    if (!type_descriptor.IsValid())
        return {};

    const auto rtti_rva = static_cast<uint32_t>((type_descriptor.Offset(-0x10) - _base_address));

    auto xrefs = FindRVAs(rtti_rva);

    for (auto xref : xrefs)
    {
        auto header = xref.Offset(-0xC);

        if (header.Get<int32_t>() == 1 && xref.Offset(-8).Get<int32_t>() == 0)
        {
            if (auto vtable = FindPtr(header); vtable.IsValid())
            {
                auto address          = vtable.Offset(8);
                _cached_vtables[name] = address;
                return address;
            }
        }
    }

    FatalError("Failed to find vtable for %s", name.c_str());
    return {};
}

CAddress CModule::GetFunctionByName(std::string_view proc_name) const
{
    return GetProcAddress(reinterpret_cast<HMODULE>(_base_address), proc_name.data());
}

int CModule::GetVTableCount(const std::string& szVtableName)
{
    auto vtable = GetVirtualTableByName(szVtableName);
    if (!vtable.IsValid())
        return 0;

    uintptr_t segmentStart = 0;
    uintptr_t segmentEnd   = 0;

    for (auto&& segment : _segments)
    {
        if ((segment.flags & FLAG_X) != 0)
        {
            segmentStart = segment.address;
            segmentEnd   = segment.address + segment.size;
            break;
        }
    }

    int count = 0;

    for (;;)
    {
        auto addr = *reinterpret_cast<uintptr_t*>(vtable.GetPtr());
        if (addr < segmentStart || addr > segmentEnd)
            break;
        count++;
        vtable = vtable.Offset(sizeof(uintptr_t));
    }

    return count;
}

#endif
