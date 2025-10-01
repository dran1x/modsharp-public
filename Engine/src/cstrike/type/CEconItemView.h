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

#ifndef CSTRIKE_TYPE_ECONITEMVIEW_H
#define CSTRIKE_TYPE_ECONITEMVIEW_H

#include "cstrike/cstrike.h"

class CEconItemAttribute
{
};

class CAttributeList : public CStrikeObject
{
public:
    [[nodiscard]] CAttributeList* GetAttributes() const
    {
        const auto offset = GetFieldOffset("CAttributeList", "m_Attributes");
        return GetFieldPointer<CAttributeList*>(offset);
    }

    void SetOrAddAttributeValueByName(const char* pszAttributeDefName, float flValue);
};

class CEconItemView : public CStrikeObject
{
public:
    DECLARE_SCHEMA_STRUCT(CEconItemView)

    SCHEMA_POINTER_FIELD(CAttributeList, m_AttributeList)
    SCHEMA_POINTER_FIELD(CAttributeList, m_NetworkedDynamicAttributes)
    SCHEMA_FIELD(int32_t, m_iItemIDHigh)
    SCHEMA_FIELD(int32_t, m_iItemIDLow)
    SCHEMA_FIELD(int32_t, m_iItemID)
    SCHEMA_FIELD(int32_t, m_iEntityLevel)
    SCHEMA_FIELD(int32_t, m_iEntityQuality)
    SCHEMA_FIELD(int32_t, m_iInventoryPosition)
    SCHEMA_FIELD(uint32_t, m_iAccountID)
    SCHEMA_FIELD(uint16_t, m_iItemDefinitionIndex)
    SCHEMA_FIELD(bool, m_bInitialized)

    void SetCustomName(const char* name) const;
};

class CAttributeContainer : public CStrikeObject
{
public:
    CEconItemView* GetItem() const
    {
        const auto offset = GetFieldOffset("CAttributeContainer", "m_Item");
        return GetFieldPointer<CEconItemView*>(offset);
    }
};
#endif
