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

#ifndef CSTRIKE_COMPONENT_BODYCOMPONENT_H
#define CSTRIKE_COMPONENT_BODYCOMPONENT_H

#include "cstrike/cstrike.h"
#include "cstrike/schema.h"

class CGameSceneNode;

class CBodyComponent : public CStrikeObject
{
public:
    DECLARE_SCHEMA_CLASS(CBodyComponent)

    SCHEMA_FIELD(CGameSceneNode*, m_pSceneNode)

    CBodyComponent() = delete;

    [[nodiscard]] CBaseEntity* GetParentEntity() const
    {
        return m_pEntity;
    }

    template <typename T>
    [[nodiscard]] T GetParentEntityAs()
    {
        return reinterpret_cast<T>(m_pEntity);
    }

private:
    [[maybe_unused]] uint8_t m_Pad[48];
    CBaseEntity*             m_pEntity;
};

class CBodyComponentSkeletonInstance : public CBodyComponent
{
public:
    DECLARE_SCHEMA_CLASS(CBodyComponentSkeletonInstance)
};

#endif