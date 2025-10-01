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

#ifndef CSTRIKE_INTERFACE_RESOURCESYSTEM_H
#define CSTRIKE_INTERFACE_RESOURCESYSTEM_H

#include "cstrike/type/CBufferString.h"

#include <cstdint>

enum ResourceStatus_t
{
    RESOURCE_STATUS_UNKNOWN = 0,
    RESOURCE_STATUS_KNOWN_BUT_NOT_RESIDENT,
    RESOURCE_STATUS_PARTIALLY_RESIDENT,
    RESOURCE_STATUS_RESIDENT,
};

using ResourceId_t   = uint64_t;
using ResourceType_t = uint64_t;

struct CResourceNameTyped
{
    CFixedBufferString<200>           m_szResourcePath;
    ResourceId_t                      m_nResourceId;   // 0xd0
    ResourceType_t                    m_nResourceType; // 0xd8
};
static_assert(sizeof(CResourceNameTyped) == 0xe0, "CResourceNameTyped size mismatch");

class IResourceSystem
{
public:
    ResourceStatus_t GetResourceStatus(const CResourceNameTyped& resource);
};

#endif
