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

#include "gamedata.h"
#include "address.h"
#include "logging.h"
#include "memory/memory_access.h"
#include "module.h"
#include "strtool.h"

#include "cstrike/type/CBufferString.h"
#include "cstrike/type/CUtlBuffer.h"
#include "cstrike/type/KeyValues.h"

#include <json.hpp>

#include <cstring>
#include <fstream>
#include <iostream>

// #define DEBUG

static std::vector<uint8_t> ParseStringToBytesVector(const std::string& content)
{
    if (content.empty())
        return {};

    std::vector<uint8_t> items;
    const auto           validates = StringSplit(content.c_str(), " ");

    for (const auto& item : validates)
    {
        if (item.empty())
            continue;

        if (item.length() > 2)
            continue;

        const auto byte = static_cast<uint8_t>(strtol(item.c_str(), nullptr, 16));
        items.emplace_back(byte);
    }

    return items;
}

static bool FindPattern(const std::string& module, const char* pattern, std::uintptr_t& address)
{
    auto FindPatternOrGetExport = [&address](CModule* module, std::string_view pattern) {
        if (pattern.empty())
            return false;

        address = pattern[0] == '@' ? module->GetFunctionByName(pattern.substr(1)) : module->FindPatternStrict(pattern);

        return true;
    };

    if (module == "engine")
    {
        return FindPatternOrGetExport(modules::engine, pattern);
    }

    if (module == "server")
    {
        return FindPatternOrGetExport(modules::server, pattern);
    }

    if (module == "tier0")
    {
        return FindPatternOrGetExport(modules::tier0, pattern);
    }

    if (module == "schemasystem")
    {
        return FindPatternOrGetExport(modules::schemas, pattern);
    }

    if (module == "resourcesystem")
    {
        return FindPatternOrGetExport(modules::resource, pattern);
    }

    if (module == "vscript")
    {
        return FindPatternOrGetExport(modules::vscript, pattern);
    }

    if (module == "vphysics2")
    {
        return FindPatternOrGetExport(modules::vphysics2, pattern);
    }

    if (module == "soundsystem")
    {
        return FindPatternOrGetExport(modules::sound, pattern);
    }

    if (module == "networksystem")
    {
        return FindPatternOrGetExport(modules::network, pattern);
    }

    if (module == "worldrenderer")
    {
        return FindPatternOrGetExport(modules::worldrenderer, pattern);
    }

    if (module == "matchmaking")
    {
        return FindPatternOrGetExport(modules::matchmaking, pattern);
    }

    if (module == "filesystem")
    {
        return FindPatternOrGetExport(modules::filesystem, pattern);
    }

    if (module == "steamsockets")
    {
        return FindPatternOrGetExport(modules::steamsockets, pattern);
    }

    if (module == "materialsystem2")
    {
        return FindPatternOrGetExport(modules::materialsystem2, pattern);
    }

    if (module == "animationsystem")
    {
        return FindPatternOrGetExport(modules::animationsystem, pattern);
    }

    return false;
}

static bool FindAddress(std::unordered_map<std::string, GameDataAddress>& addresses, const char* name, std::uintptr_t& pAddress)
{
    auto it = addresses.find(name);
    if (it == addresses.end())
    {
        return false;
    }

    auto& item = it->second;

    if (item.m_FoundAddress != 0)
    {
        pAddress = item.m_FoundAddress;
        return true;
    }

    uintptr_t address = 0;
    if (item.m_Base.has_value())
    {
        FindAddress(addresses, item.m_Base.value().c_str(), address);
    }
    else if (item.m_Signature.has_value() && item.m_Module.has_value())
    {
        FindPattern(item.m_Module.value(), item.m_Signature.value().c_str(), address);
    }

    if (address == 0)
    {
        printf("MS: Failed to find address for %s\n", name);
        return false;
    }

    // factory
    if (item.m_Factory.has_value())
    {
        const auto ops = StringSplit(item.m_Factory.value().c_str(), " ");

        for (auto& op : ops)
        {
            if (op.empty())
                continue;

            if (op.starts_with("r")) // resolve relative address
            {
                address = address + sizeof(int) + *reinterpret_cast<int*>(address);
            }
            else if (op.starts_with("d")) // dereference
            {
                address = *reinterpret_cast<uintptr_t*>(address);
            }
            else if (op.starts_with("-")) // offset -
            {
                address = address - strtol(op.c_str() + 1, nullptr, 10);
            }
            else if (op.starts_with("+")) // offset +
            {
                address = address + strtol(op.c_str() + 1, nullptr, 10);
            }
            else // offset +
            {
                address = address + strtol(op.c_str(), nullptr, 10);
            }
        }
    }

    pAddress            = address;
    item.m_FoundAddress = address;

    return true;
}

static bool LoadGameDataFile(const std::filesystem::path& base_path, std::string& output_content, std::string& error_msg)
{
    auto jsonc_path = base_path;
    jsonc_path.replace_extension("jsonc");

    const bool jsonc_exists = std::filesystem::exists(jsonc_path);

    if (!jsonc_exists)
    {
        error_msg = "Gamedata not found: " + jsonc_path.filename().generic_string();
        return false;
    }

    std::ifstream f(jsonc_path);
    if (!f.is_open())
    {
        error_msg = "Failed to open file: " + jsonc_path.generic_string();
        return false;
    }

    output_content.assign(std::istreambuf_iterator<char>(f), std::istreambuf_iterator<char>());
    return true;
}

bool GameData::Register(const char* name, char* error, int maxlen)
{
    std::filesystem::path path = "../../sharp/gamedata/";
    path /= name;

    std::string content{};
    std::string error_string{};

    if (!LoadGameDataFile(path, content, error_string))
    {
        snprintf(error, maxlen, "%s", error_string.c_str());
        return false;
    }

    if (content.empty())
    {
        snprintf(error, maxlen, "File '%s' is empty.", path.filename().generic_string().c_str());
        return false;
    }

    path.replace_extension("jsonc");

    return LoadRawTextJson(content.c_str(), path.generic_string().c_str(), error, maxlen);
}

bool GameData::Unregister(const char* name, char* error, int maxLen)
{
    std::filesystem::path file_path = "../../sharp/gamedata/";
    file_path /= name;

    for (auto it = m_Patches.cbegin(); it != m_Patches.cend();)
    {
        auto& patch = it->second;
        if (patch.m_File == file_path)
        {
            // restore patches
            const auto address = patch.m_Address;

            SetMemoryAccess(address, patch.m_StoreBytes.size(), g_nReadWriteExecuteAccess);

            memcpy(address, patch.m_StoreBytes.data(), patch.m_StoreBytes.size());

            SetMemoryAccess(address, patch.m_StoreBytes.size(), g_nReadExecuteAccess);

            it = m_Patches.erase(it);
        }
        else
        {
            ++it;
        }
    }

    auto predicate = [&](const auto& pair) {
        return pair.second.m_File == file_path;
    };

    std::erase_if(m_Addresses, predicate);
    std::erase_if(m_Offsets, predicate);
    std::erase_if(m_VFuncs, predicate);

    return true;
}

bool GameData::GetOffset(const char* name, int* offset)
{
    if (auto it = m_Offsets.find(name); it != m_Offsets.end())
    {
        *offset = it->second.m_Index;
        return true;
    }

    return false;
}

bool GameData::GetAddress(const char* name, std::uintptr_t& address)
{
    address = 0;

    return FindAddress(m_Addresses, name, address);
}

bool GameData::GetVFunctionIndex(const char* name, int* offset)
{
    if (auto it = m_VFuncs.find(name); it != m_VFuncs.end())
    {
        *offset = it->second.m_Index;
        return true;
    }

    return false;
}

bool GameData::InitPatch(const std::string& name, GameDataPatch* item)
{
    if (!item->m_StoreBytes.empty())
    {
        FERROR("Patch already initialized: %s\n", name.c_str());
        return false;
    }

    const auto address = GetAddress<uint8_t*>(item->m_AddressKey.c_str());
    item->m_Address    = address;

    // validate
    if (!item->m_ValidateBytes.empty())
    {
        int i = 0;
        for (const auto byte : item->m_ValidateBytes)
        {
            if (address[i++] != byte)
            {
                FERROR("Patch validate failed: %s\n", name.c_str());
                return false;
            }
        }
    }

    const auto size = item->m_PatchBytes.size();

    // unprotect memory
    SetMemoryAccess(address, size, g_nReadWriteExecuteAccess);

    // store and patch
    {
        item->m_StoreBytes.resize(size);
        memcpy(item->m_StoreBytes.data(), address, size);
        memcpy(address, item->m_PatchBytes.data(), size);
    }

    // protect memory
    SetMemoryAccess(address, item->m_PatchBytes.size(), g_nReadExecuteAccess);

    FLOG("Patch initialized: %s", name.c_str());
    return true;
}

static void ParseAddresses(const std::filesystem::path& path, const char* platform_name, const nlohmann::json& json, std::unordered_map<std::string, GameDataAddress>& out)
{
    auto address_object = json.find("Addresses");
    if (address_object == json.end())
        return;

    for (auto& [key, entry_object] : address_object->items())
    {
        if (!entry_object.is_object())
        {
            continue;
        }

        auto platform_object = entry_object.find(platform_name);
        if (platform_object == entry_object.end() || platform_object->is_null())
            continue;

        GameDataAddress item;
        item.m_File = path.string();

        if (auto library_object = entry_object.find("library"); library_object != entry_object.end() && library_object->is_string())
        {
            item.m_Module = library_object->get<std::string>();
        }

        if (auto base_object = entry_object.find("base"); base_object != entry_object.end() && base_object->is_string())
        {
            item.m_Base = base_object->get<std::string>();
        }

        if (platform_object->is_string())
        {
            auto str         = platform_object->get<std::string>();
            item.m_Signature = str;
        }
        else if (platform_object->is_object())
        {
            if (auto signature_object = platform_object->find("signature"); signature_object != platform_object->end() && signature_object->is_string())
            {
                item.m_Signature = signature_object->get<std::string>();
            }
            if (auto base_object = platform_object->find("base"); base_object != platform_object->end() && base_object->is_string())
            {
                item.m_Base = base_object->get<std::string>();
            }

            auto factory_object = platform_object->find("factory");
            if (factory_object == platform_object->end())
                continue;

            if (factory_object->is_string())
            {
                item.m_Factory = factory_object->get<std::string>();
            }
            else if (factory_object->is_number())
            {
                item.m_Factory = "+" + factory_object->dump();

                WARN("[%s] %s[\"%s\"][\"factory\"] is a number, this is not an expected behavior, parsing as string '%s'", path.filename().c_str(), key.c_str(), platform_name, item.m_Factory.value_or("").c_str());
            }
            else
            {
                FERROR("[%s] %s[\"%s\"][\"factory\"] must be a string", path.filename().c_str(), key.c_str(), platform_name);
            }
        }

        if (!item.m_Module && !item.m_Base)
        {
            continue;
        }

        out[key] = std::move(item);
    }
}

static void ParseData(const std::filesystem::path& path, const char* platform_name, const nlohmann::json& json, const std::string& section_name, std::unordered_map<std::string, GameDataOffset>& out)
{
    auto section_object = json.find(section_name);
    if (section_object == json.end())
        return;

    if (!section_object->is_object())
    {
        WARN("[%s] %s is not a JSON object, ignoring", path.filename().generic_string().c_str(), section_name.c_str());
        return;
    }

    for (const auto& [key, entry_json] : section_object->items())
    {
        if (!entry_json.is_object())
        {
            WARN("[%s] %s[\"%s\"] is not a JSON object, ignoring", path.filename().generic_string().c_str(), section_name.c_str(), key.c_str());
            continue;
        }

        auto platform_object = entry_json.find(platform_name);
        if (platform_object == entry_json.end())
            continue;

        if (!platform_object->is_number_integer())
        {
            WARN("[%s] %s[\"%s\"][\"%s\"] is not an integer, ignoring.", path.filename().generic_string().c_str(), section_name.c_str(), key.c_str(), platform_name);
            continue;
        }

        auto& item   = out[key];
        item.m_Index = platform_object->get<std::int32_t>();
        item.m_File  = path.generic_string();
    }
}

static void ParsePatches(const std::filesystem::path& path, const char* platform_name, const nlohmann::json& json, std::unordered_map<std::string, GameDataPatch>& out)
{
    auto patches = json.find("Patches");
    if (patches == json.end())
        return;

    if (!patches->is_object())
    {
        WARN("[%s] Patches is not a JSON object, ignoring", path.filename().generic_string().c_str());
        return;
    }

    for (const auto& [key, entry_json] : patches->items())
    {
        if (!entry_json.is_object())
        {
            WARN("[%s] Patches[\"%s\"] is not a JSON object, ignoring", path.filename().generic_string().c_str(), key.c_str());

            continue;
        }

        auto address_object = entry_json.find("address");
        if (address_object == entry_json.end())
        {
            WARN("[%s] Patches[\"%s\"] does not contain key \"address\", ignoring", path.filename().generic_string().c_str(), key.c_str());

            continue;
        }

        auto platform_object = entry_json.find(platform_name);
        if (platform_object == entry_json.end())
        {
            continue;
        }

        auto patch_object = platform_object->find("patch");
        if (patch_object == platform_object->end())
        {
            WARN("[%s] Patches[\"%s\"][\"%s\"] does not contain key \"patch\", ignoring", path.filename().generic_string().c_str(), key.c_str(), platform_name);

            continue;
        }

        if (patch_object->is_null())
        {
            WARN("[%s] Patches[\"%s\"][\"%s\"] \"patch\" key is null, ignoring", path.filename().generic_string().c_str(), key.c_str(), platform_name);

            continue;
        }

        std::string str = patch_object->dump();
        std::erase(str, '\"');

        auto patch_bytes = ParseStringToBytesVector(str);
        if (patch_bytes.empty())
        {
            WARN("[%s] Patches[\"%s\"] parsed patch byte is empty (str: %s), ignoring", path.filename().generic_string().c_str(), key.c_str(), str.c_str());

            continue;
        }

        auto& item = out[key];

        item.m_File       = path.generic_string();
        item.m_AddressKey = address_object->get<std::string>();

        if (auto validate_object = platform_object->find("validate"); validate_object != platform_object->end())
        {
            auto validate_str = validate_object->dump();
            std::erase(validate_str, '\"');

            item.m_ValidateBytes = ParseStringToBytesVector(validate_str);
        }

        item.m_PatchBytes = std::move(patch_bytes);
    }
}

bool GameData::LoadRawTextJson(const char* content, const std::filesystem::path& path, char* error, int maxlen)
{
#ifdef PLATFORM_WINDOWS
    static constexpr auto platform_name = "windows";
#else
    static constexpr auto platform_name = "linux";
#endif
    auto file_name = path.filename().generic_string();

    try
    {
        auto json = nlohmann::json::parse(content, /*callback*/ nullptr, /*allow_exception*/ true, /*ignore_comments*/ true);

        std::unordered_map<std::string, GameDataAddress> temp_addresses{};
        ParseAddresses(path, platform_name, json, temp_addresses);

        std::unordered_map<std::string, GameDataOffset> temp_offsets{};
        ParseData(path, platform_name, json, "Offsets", temp_offsets);

        std::unordered_map<std::string, GameDataOffset> temp_vtables{};
        ParseData(path, platform_name, json, "VFuncs", temp_vtables);

        std::unordered_map<std::string, GameDataPatch> temp_patches{};
        ParsePatches(path, platform_name, json, temp_patches);

        if (temp_addresses.empty() && temp_offsets.empty() && temp_vtables.empty() && temp_patches.empty())
        {
            snprintf(error, maxlen, "No valid gamedata (addresses, offsets, etc.) found for platform '%s' in %s.", platform_name, file_name.c_str());
            return false;
        }

        // merge check stage
        for (const auto& key : temp_offsets | std::views::keys)
        {
            if (m_Offsets.contains(key))
            {
                snprintf(error, maxlen, "Offset '%s' already exists.", key.c_str());
                return false;
            }
        }

        for (const auto& key : temp_vtables | std::views::keys)
        {
            if (m_VFuncs.contains(key))
            {
                snprintf(error, maxlen, "VFunc '%s' already exists.", key.c_str());
                return false;
            }
        }

        for (const auto& key : temp_addresses | std::views::keys)
        {
            if (m_Addresses.contains(key))
            {
                snprintf(error, maxlen, "Address '%s' already exists.", key.c_str());
                return false;
            }
        }

        for (const auto& key : temp_patches | std::views::keys)
        {
            if (m_Patches.contains(key))
            {
                snprintf(error, maxlen, "Patch '%s' already exists.", key.c_str());
                return false;
            }
        }

        // validate address
        std::vector<std::string> failed_addresses = {};
#if defined(DEBUG)
        std::vector<std::string> succeeded_addresses = {};
#endif

        for (const auto& item : temp_addresses)
        {
            std::uintptr_t address{};
            if (!FindAddress(temp_addresses, item.first.c_str(), address))
            {
                failed_addresses.emplace_back(item.first);
            }
#if defined(DEBUG)
            else
            {
                succeeded_addresses.emplace_back(item.first);
            }
#endif
        }

#if defined(DEBUG)
        if (!succeeded_addresses.empty())
        {
            std::string formatted_addresses;
            for (size_t i = 0; i < succeeded_addresses.size(); ++i)
            {
                formatted_addresses += " " + std::to_string(i + 1) + ": " + succeeded_addresses[i] + "\n";
            }
            FLOG("Address resolve succeeded:\n%s", formatted_addresses.c_str());
            fflush(stdout);
        }
#endif

        if (!failed_addresses.empty())
        {
            std::string formatted_addresses;
            for (size_t i = 0; i < failed_addresses.size(); ++i)
            {
                formatted_addresses += " " + std::to_string(i + 1) + ": " + failed_addresses[i] + "\n";
            }

            FERROR("Address resolve failed:\n%s", formatted_addresses.c_str());
            fflush(stdout);

            snprintf(error, maxlen, "Total %d sigs resolve failed.\n", static_cast<int>(failed_addresses.size()));
            return false;
        }

        m_Offsets.merge(temp_offsets);
        m_VFuncs.merge(temp_vtables);
        m_Addresses.merge(temp_addresses);

        for (auto& [name, patch] : temp_patches)
        {
            if (!InitPatch(name, &patch))
                return false;

            m_Patches[name] = std::move(patch);
        }
    }
    catch (std::exception& ex)
    {
        FERROR("Error when parsing json %s: %s", file_name.c_str(), ex.what());
        return false;
    }

    return true;
}

bool GameData::Register(const char* name)
{
    char error[256];
    if (!Register(name, error, sizeof(error)))
    {
        FERROR("%s: %s", name, error);
        return false;
    }
    return true;
}

void GameData::Unregister(const char* name)
{
    char error[256];
    if (!Unregister(name, error, sizeof(error)))
    {
        FatalError(error);
    }
}

int GameData::GetOffset(const char* name)
{
    int offset = 0;
    if (!GetOffset(name, &offset))
    {
        FatalError("Cant find offset: \"%s\"", name);
    }

    return offset;
}

int GameData::GetVFunctionIndex(const char* name)
{
    int index{};
    if (!GetVFunctionIndex(name, &index))
    {
        FatalError("Cant find vtable index: \"%s\"", name);
    }

    return index;
}

void* GameData::GetAddressInternal(const char* name)
{
    std::uintptr_t address{};

    if (!GetAddress(name, address))
    {
        FatalError("Cant find address: \"%s\"", name);
    }

    return reinterpret_cast<void*>(address);
}