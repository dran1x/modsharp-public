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

#if defined PLATFORM_POSIX

#    include "logging.h"
#    include "module.h"

#    include <algorithm>
#    include <cstring>
#    include <fcntl.h>
#    include <link.h>
#    include <sys/mman.h>
#    include <sys/stat.h>
#    include <unistd.h>

std::vector<std::pair<std::string, dl_phdr_info>> module_list{};

void CModule::GetModuleInfo(std::string_view mod)
{
    if (module_list.empty()) [[unlikely]]
    {
        dl_iterate_phdr(
            [](struct dl_phdr_info* info, size_t, void*) {
                std::string name = info->dlpi_name;

                if (name.rfind(".so") == std::string::npos)
                    return 0;

                /*if (name.find("/addons/") != std::string::npos)
                    return 0;*/

                constexpr std::string_view ROOTBIN = "/bin/linuxsteamrt64/";
                constexpr std::string_view GAMEBIN = "/csgo/bin/linuxsteamrt64/";

                bool isFromRootBin = name.find(ROOTBIN) != std::string::npos;
                bool isFromGameBin = name.find(GAMEBIN) != std::string::npos;
                if (!isFromGameBin && !isFromRootBin)
                    return 0;

                auto& mod_info = module_list.emplace_back();

                mod_info.first  = name;
                mod_info.second = *info;
                return 0;
            },
            nullptr);
    }

    const auto it = std::find_if(module_list.begin(),
                                 module_list.end(),
                                 [&](const auto& i) {
                                     return i.first.find(mod) != std::string::npos;
                                 });

    if (it == module_list.end())
    {
        return;
    }

    const std::string_view path = it->first;
    const auto             info = it->second;

    this->_base_address = info.dlpi_addr;
    this->_module_name  = path.substr(path.find_last_of('/') + 1);

    for (auto i = 0; i < info.dlpi_phnum; i++)
    {
        auto address            = _base_address + info.dlpi_phdr[i].p_paddr;
        auto size               = (uintptr_t)info.dlpi_phdr[i].p_memsz;
        auto type               = info.dlpi_phdr[i].p_type;
        auto is_dynamic_section = type == PT_DYNAMIC;

        auto flags = info.dlpi_phdr[i].p_flags;

        auto is_executable = (flags & PF_X) != 0;
        auto is_readable   = (flags & PF_R) != 0;
        auto is_writable   = (flags & PF_W) != 0;

        if (is_dynamic_section)
        {
            DumpExports(reinterpret_cast<void*>(address));
            continue;
        }

        if (type != PT_LOAD)
            continue;

        /*if (info.dlpi_phdr[i].p_paddr == 0)
            continue;*/

        auto* data = reinterpret_cast<std::uint8_t*>(address);

        auto& segment = _segments.emplace_back();

        segment.address = address;
        segment.data    = std::vector(data, data + size);
        segment.size    = size;

        if (is_executable)
            segment.flags |= FLAG_X;

        if (is_readable)
            segment.flags |= FLAG_R;
        if (is_writable)
            segment.flags |= FLAG_W;
    }
}

void CModule::DumpExports(void* module_base)
{
    auto dyn = (ElfW(Dyn)*)(module_base);
    // thanks to https://stackoverflow.com/a/57099317
    auto GetNumberOfSymbolsFromGnuHash = [](ElfW(Addr) gnuHashAddress) {
        // See https://flapenguin.me/2017/05/10/elf-lookup-dt-gnu-hash/ and
        // https://sourceware.org/ml/binutils/2006-10/msg00377.html
        struct Header
        {
            uint32_t nbuckets;
            uint32_t symoffset;
            uint32_t bloom_size;
            uint32_t bloom_shift;
        };

        auto       header         = (Header*)gnuHashAddress;
        const auto bucketsAddress = gnuHashAddress + sizeof(Header) + (sizeof(std::uintptr_t) * header->bloom_size);

        // Locate the chain that handles the largest index bucket.
        uint32_t lastSymbol    = 0;
        auto     bucketAddress = (uint32_t*)bucketsAddress;
        for (uint32_t i = 0; i < header->nbuckets; ++i)
        {
            uint32_t bucket = *bucketAddress;
            if (lastSymbol < bucket)
            {
                lastSymbol = bucket;
            }
            bucketAddress++;
        }

        if (lastSymbol < header->symoffset)
        {
            return header->symoffset;
        }

        // Walk the bucket's chain to add the chain length to the total.
        const auto chainBaseAddress = bucketsAddress + (sizeof(uint32_t) * header->nbuckets);
        for (;;)
        {
            auto chainEntry = (uint32_t*)(chainBaseAddress + (lastSymbol - header->symoffset) * sizeof(uint32_t));
            lastSymbol++;

            // If the low bit is set, this entry is the end of the chain.
            if (*chainEntry & 1)
            {
                break;
            }
        }

        return lastSymbol;
    };

    ElfW(Sym) * symbols{};
    ElfW(Word) * hash_ptr{};

    char*       string_table{};
    std::size_t symbol_count{};

    while (dyn->d_tag != DT_NULL)
    {
        if (dyn->d_tag == DT_HASH)
        {
            hash_ptr     = reinterpret_cast<ElfW(Word)*>(dyn->d_un.d_ptr);
            symbol_count = hash_ptr[1];
        }
        else if (dyn->d_tag == DT_STRTAB)
        {
            string_table = reinterpret_cast<char*>(dyn->d_un.d_ptr);
        }
        else if (!symbol_count && dyn->d_tag == DT_GNU_HASH)
        {
            symbol_count = GetNumberOfSymbolsFromGnuHash(dyn->d_un.d_ptr);
        }
        else if (dyn->d_tag == DT_SYMTAB)
        {
            symbols = reinterpret_cast<ElfW(Sym)*>(dyn->d_un.d_ptr);
        }
        dyn++;
    }

    for (auto i = 0; i < symbol_count; i++)
    {
        if (!symbols[i].st_name)
        {
            continue;
        }

        if (symbols[i].st_other != 0)
        {
            continue;
        }

        auto        address = symbols[i].st_value + _base_address;
        std::string name    = &string_table[symbols[i].st_name];
        _exports[name]      = address;
        if (name == "CreateInterface")
            _createInterFaceFn = reinterpret_cast<void*>(address);
    }
}

CAddress CModule::GetFunctionByName(std::string_view proc_name) const
{
    if (auto it = _exports.find(proc_name.data()); it != _exports.end())
        return it->second;
    return {};
}

[[nodiscard]] CAddress CModule::GetVirtualTableByName(const std::string& name, bool decorated)
{
    if (const auto it = _cached_vtables.find(name); it != _cached_vtables.end())
    {
        return it->second;
    }

    auto decoreated_name = decorated ? name : (std::to_string(name.length()) + name);
    auto type_info_name  = FindString(decoreated_name, true);
    if (!type_info_name.IsValid())
    {
        return {};
    }

    auto xrefs = FindPtrs(type_info_name.GetPtr());
    for (auto xref : xrefs)
    {
        auto type_info      = xref.Offset(-8);
        auto type_info_xref = FindPtr(type_info.GetPtr());
        if (type_info_xref.IsValid())
        {
            auto address          = type_info_xref.Offset(8);
            _cached_vtables[name] = address;
            return address;
        }
    }

    FatalError("Failed to find vtable for %s", name.c_str());
    return {};
}
#endif