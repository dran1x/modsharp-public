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

#ifndef CSTRIKE_TYPE_RESOURCE_SYSTEM_H
#define CSTRIKE_TYPE_RESOURCE_SYSTEM_H

using ResourceBlockId_t = uint32_t;

// 一般情况下用这个就好
inline constexpr ResourceBlockId_t g_ResourceBlockId_Data = 0x41544144;
inline constexpr ResourceBlockId_t g_ResourceBlockId_Flc  = 0x49434C46;

template <typename T>
struct CResourceArrayBase
{
    int32_t  m_nOffset;
    uint32_t m_nCount;
};

template <typename T>
struct CResourcePointerBase
{
    int32_t m_nOffset;
};

struct CResourceBlockInfo
{
    ResourceBlockId_t m_nBlockType{};
    char*             m_pBlockData{};
    int               m_nSize{};
};

struct ResourceBlockEntry_t
{
    ResourceBlockId_t          m_nBlockType;
    CResourcePointerBase<void> m_pBlockData;
    uint32_t                   m_nBlockSize;
};

struct ResourceFileHeader_t
{
    uint32_t                                 m_nNonStreamingSize;
    uint16_t                                 m_nHeaderVersion;
    uint16_t                                 m_nResourceVersion;
    CResourceArrayBase<ResourceBlockEntry_t> m_ResourceBlocks;
};

static bool Resource_FindBlockInfo(const ResourceFileHeader_t* header, ResourceBlockId_t block_id, CResourceBlockInfo& out_block_info)
{
    if (!header || header->m_nHeaderVersion != 12 || header->m_ResourceBlocks.m_nCount == 0)
    {
        return false;
    }

    const auto* entries = reinterpret_cast<const ResourceBlockEntry_t*>(
        (const char*)&header->m_ResourceBlocks.m_nOffset + header->m_ResourceBlocks.m_nOffset);

    for (uint32_t i = 0; i < header->m_ResourceBlocks.m_nCount; ++i)
    {
        const ResourceBlockEntry_t& entry = entries[i];

        if (entry.m_nBlockType == block_id)
        {
            out_block_info.m_nBlockType = entry.m_nBlockType;
            out_block_info.m_nSize      = entry.m_nBlockSize;

            if (entry.m_pBlockData.m_nOffset != 0)
            {
                out_block_info.m_pBlockData = (char*)&entry.m_pBlockData + entry.m_pBlockData.m_nOffset;
            }
            else
            {
                out_block_info.m_pBlockData = nullptr;
            }

            return true;
        }
    }

    return false;
}
#endif
