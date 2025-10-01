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

#ifndef CSTRIKE_ENTITY_ECONENTITY_H
#define CSTRIKE_ENTITY_ECONENTITY_H

#include "cstrike/entity/CBaseModelEntity.h"

class CAttributeContainer;

class CEconEntity : public CBaseModelEntity
{
public:
    CAttributeContainer* GetAttributeManager() const
    {
        static auto offset = GetFieldOffset("CEconEntity", "m_AttributeManager");
        return GetFieldPointer<CAttributeContainer*>(offset);
    }

    void SetFallbackPaintKit(int32_t paint) const
    {
        static auto offset = GetFieldOffset("CEconEntity", "m_nFallbackPaintKit");
        const auto  field  = GetFieldPointer<int32_t*>(offset);
        *field             = paint;
    }

    void SetFallbackSeed(int32_t seed) const
    {
        static auto offset = GetFieldOffset("CEconEntity", "m_nFallbackSeed");
        const auto  field  = GetFieldPointer<int32_t*>(offset);
        *field             = seed;
    }
};

#endif