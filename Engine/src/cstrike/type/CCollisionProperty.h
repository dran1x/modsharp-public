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

#ifndef CSTRIKE_TYPE_COLLISIONPROPERTY_H
#define CSTRIKE_TYPE_COLLISIONPROPERTY_H

#include "cstrike/consts.h"
#include "cstrike/cstrike.h"
#include "cstrike/schema.h"

#include <cstdint>

struct VPhysicsCollisionAttribute_t
{
    DECLARE_SCHEMA_STRUCT(VPhysicsCollisionAttribute_t)

    SCHEMA_FIELD(uint8_t, m_nCollisionGroup)
    SCHEMA_FIELD(uint64_t, m_nInteractsAs)
    SCHEMA_FIELD(uint64_t, m_nInteractsWith)
    SCHEMA_FIELD(uint64_t, m_nInteractsExclude)
    SCHEMA_FIELD(uint32_t, m_nEntityId)
    SCHEMA_FIELD(uint32_t, m_nOwnerId)
    SCHEMA_FIELD(uint16_t, m_nHierarchyId)
};

class CCollisionProperty : public CStrikeObject
{
public:
    DECLARE_SCHEMA_STRUCT(CCollisionProperty)

    SCHEMA_FIELD(SolidType_t, m_nSolidType)
    SCHEMA_FIELD(uint8_t, m_usSolidFlags)
    SCHEMA_FIELD(VPhysicsCollisionAttribute_t, m_collisionAttribute)

    void SetSolid(SolidType_t solidType)
    {
        address::server::CCollisionProperty_SetSolid(this, static_cast<uint8_t>(solidType));
    }
};

#endif