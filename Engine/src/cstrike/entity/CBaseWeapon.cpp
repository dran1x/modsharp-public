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
#include "global.h"
#include "vhook/call.h"

#include "cstrike/entity/CBaseWeapon.h"
#include "cstrike/type/VData.h"

CCSWeaponBaseVData* CBaseWeapon::GetVData() const
{
    static auto offset = g_pGameData->GetOffset("CBaseWeapon::m_VData");
    return GetFieldValue<CCSWeaponBaseVData*>(offset);
}

void CBaseWeapon::Holster()
{
    VCall_AutoVoid(CBaseWeapon, Holster, this, nullptr);
}

void CBaseWeapon::Deploy()
{
    VCall_AutoVoid(CBaseWeapon, Deploy, this);
}