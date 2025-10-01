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

#ifndef CSTRIKE_ENTITY_ENVEXPLOSION_H
#define CSTRIKE_ENTITY_ENVEXPLOSION_H

#include "cstrike/entity/CBaseEntity.h"

class CEnvExplosion : public CBaseEntity
{
public:
    DECLARE_SCHEMA_CLASS(CEnvExplosion)

    SCHEMA_FIELD(int32_t, m_iCustomDamageType)
    SCHEMA_FIELD(int32_t, m_iMagnitude)
};

class CBaseFilter : public CBaseEntity
{
public:
    DECLARE_SCHEMA_CLASS(CBaseFilter)

    SCHEMA_FIELD(bool, m_bNegated)
};

class FilterDamageType : public CBaseFilter
{
public:
    DECLARE_SCHEMA_CLASS(FilterDamageType)

    SCHEMA_FIELD(int32_t, m_iDamageType)
};

#endif
