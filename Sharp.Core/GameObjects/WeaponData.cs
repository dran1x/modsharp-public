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

using System;
using Sharp.Core.CStrike;
using Sharp.Core.Helpers;
using Sharp.Generator;
using Sharp.Shared;
using Sharp.Shared.Enums;
using Sharp.Shared.GameObjects;
using Sharp.Shared.Utilities;

namespace Sharp.Core.GameObjects;

internal partial class WeaponData : EntitySubclassVData, IWeaponData
{
    private static readonly Lazy<int> MaxSpeedOffset
        = new (() => SchemaSystem.GetNetVarOffset("CCSWeaponBaseVData", "m_flMaxSpeed"));

    private static readonly Lazy<int> CycleTimeOffset
        = new (() => SchemaSystem.GetNetVarOffset("CCSWeaponBaseVData", "m_flCycleTime"));

    public float MaxSpeedAlt
    {
        get => _this.GetFloat(MaxSpeedOffset.Value   + 4);
        set => _this.WriteFloat(MaxSpeedOffset.Value + 4, value);
    }

    public float CycleTimeAlt
    {
        get => _this.GetFloat(CycleTimeOffset.Value   + 4);
        set => _this.WriteFloat(CycleTimeOffset.Value + 4, value);
    }

#region Schemas

    [NativeSchemaField("CBasePlayerWeaponVData", "m_iMaxClip1", typeof(int))]
    private partial SchemaField GetMaxClipField();

    [NativeSchemaField("CCSWeaponBaseVData", "m_WeaponType", typeof(CStrikeWeaponType))]
    private partial SchemaField GetWeaponTypeField();

    [NativeSchemaField("CCSWeaponBaseVData", "m_WeaponCategory", typeof(CStrikeWeaponCategory))]
    private partial SchemaField GetWeaponCategoryField();

    [NativeSchemaField("CCSWeaponBaseVData", "m_GearSlot", typeof(GearSlot))]
    private partial SchemaField GetGearSlotField();

    [NativeSchemaField("CCSWeaponBaseVData", "m_nPrice", typeof(int))]
    private partial SchemaField GetPriceField();

    [NativeSchemaField("CCSWeaponBaseVData", "m_nPrimaryReserveAmmoMax", typeof(int))]
    private partial SchemaField GetPrimaryReserveAmmoMaxField();

    [NativeSchemaField("CCSWeaponBaseVData", "m_nDamage", typeof(int))]
    private partial SchemaField GetDamageField();

    [NativeSchemaField("CCSWeaponBaseVData", "m_bMeleeWeapon", typeof(bool))]
    private partial SchemaField GetIsMeleeWeaponField();

    [NativeSchemaField("CCSWeaponBaseVData", "m_bIsFullAuto", typeof(bool))]
    private partial SchemaField GetIsFullAutoField();

    [NativeSchemaField("CCSWeaponBaseVData", "m_nNumBullets", typeof(int))]
    private partial SchemaField GetNumBulletsField();

    [NativeSchemaField("CCSWeaponBaseVData", "m_flCycleTime", typeof(float))]
    private partial SchemaField GetCycleTimeField();

    [NativeSchemaField("CCSWeaponBaseVData", "m_flMaxSpeed", typeof(float))]
    private partial SchemaField GetMaxSpeedField();

    [NativeSchemaField("CCSWeaponBaseVData", "m_flHeadshotMultiplier", typeof(float))]
    private partial SchemaField GetHeadshotMultiplierField();

    [NativeSchemaField("CCSWeaponBaseVData", "m_flArmorRatio", typeof(float))]
    private partial SchemaField GetArmorRatioField();

    [NativeSchemaField("CCSWeaponBaseVData", "m_flRange", typeof(float))]
    private partial SchemaField GetRangeField();

    [NativeSchemaField("CCSWeaponBaseVData", "m_flRangeModifier", typeof(float))]
    private partial SchemaField GetRangeModifierField();

#endregion

    public override string GetSchemaClassname()
        => "CCSWeaponBaseVData";
}
