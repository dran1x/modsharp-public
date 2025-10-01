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

#ifndef CSTRIKE_ENTITY_WEAPON_H
#define CSTRIKE_ENTITY_WEAPON_H

#include "cstrike/entity/CEconEntity.h"
#include "cstrike/type/VData.h"

class CBaseWeapon /* CCSWeaponBaseGun : CCSWeaponBase : CBasePlayerWeapon */ : public CEconEntity
{
public:
    SCHEMA_FIELD_MANUAL(CBasePlayerWeapon, m_iClip1, int32_t)
    SCHEMA_FIELD_MANUAL(CBasePlayerWeapon, m_pReserveAmmo, int32_t)

    SCHEMA_FIELD_MANUAL(CCSWeaponBase, m_iWeaponGameplayAnimState, WeaponGameplayAnimState)
    SCHEMA_FIELD_MANUAL(CCSWeaponBase, m_flWeaponGameplayAnimStateTimestamp, float)

    SCHEMA_FIELD_MANUAL(CCSWeaponBase, m_ePlayerFireEvent, int32_t)
    SCHEMA_FIELD_MANUAL(CCSWeaponBase, m_ePlayerFireEventAttackType, int32_t)
    SCHEMA_FIELD_MANUAL(CCSWeaponBase, m_seqFirePrimary, int32_t)

    SCHEMA_FIELD_MANUAL(CCSWeaponBase, m_flFireSequenceStartTime, float)
    SCHEMA_FIELD_MANUAL(CCSWeaponBase, m_nFireSequenceStartTimeChange, int32_t)
    SCHEMA_FIELD_MANUAL(CCSWeaponBase, m_nFireSequenceStartTimeAck, int32_t)

    [[nodiscard]] CCSWeaponBaseVData* GetVData() const;

    [[nodiscard]] GearSlot_t              GetSlot() const { return GetVData()->m_GearSlot(); }
    [[nodiscard]] int32_t                 GetMaxClip1() const { return GetVData()->m_iMaxClip1(); }
    [[nodiscard]] int32_t                 GetReserveAmmoMax() const { return GetVData()->m_nPrimaryReserveAmmoMax(); }
    [[nodiscard]] WeaponGameplayAnimState GetAnimState() const { return m_iWeaponGameplayAnimState(); }

    void Holster();
    void Deploy();

    // 字段不联网
    void SetCanBePickedUp(bool state) const
    {
        static auto offset = GetFieldOffset("CCSWeaponBase", "m_bCanBePickedUp");
        SetFieldValue(offset, state);
    }
};

using CWeaponCSBase    = CBaseWeapon;
using CCSWeaponBaseGun = CBaseWeapon;

class CKnife : public CBaseWeapon
{};

class CFists : public CBaseWeapon
{};

#endif
