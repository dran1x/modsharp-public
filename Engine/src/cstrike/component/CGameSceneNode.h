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

#ifndef CSTRIKE_COMPONENT_GAMESCENENODE_H
#define CSTRIKE_COMPONENT_GAMESCENENODE_H

#include "cstrike/cstrike.h"
#include "cstrike/schema.h"
#include "cstrike/type/QAngle.h"
#include "cstrike/type/Vector.h"
#include "cstrike/type/matrix3x4.h"

#include <numbers>

class CBaseEntity;

class CGameSceneNode
{
public:
    DECLARE_SCHEMA_CLASS(CGameSceneNode)

    SCHEMA_FIELD(CEntityInstance*, m_pOwner)
    SCHEMA_FIELD(CGameSceneNode*, m_pParent)
    SCHEMA_FIELD(CGameSceneNode*, m_pChild)
    SCHEMA_FIELD(QAngle, m_angRotation)
    SCHEMA_FIELD(float, m_flScale)
    SCHEMA_FIELD(float, m_flAbsScale)
    SCHEMA_FIELD(Vector, m_vecAbsOrigin)
    SCHEMA_FIELD(QAngle, m_angAbsRotation)
    SCHEMA_FIELD(Vector, m_vRenderOrigin)

    matrix3x4_t EntityToWorldTransform();

    [[nodiscard]] CStrikeObject* GetStudioModel();

    [[nodiscard]] CBaseEntity* GetOwnerEntity()
    {
        return reinterpret_cast<CBaseEntity*>(m_pOwner());
    }
};

class CSkeletonInstance : public CGameSceneNode
{
public:
    DECLARE_SCHEMA_CLASS(CSkeletonInstance)

    SCHEMA_FIELD(bool, m_bIsAnimationEnabled)
};

#endif