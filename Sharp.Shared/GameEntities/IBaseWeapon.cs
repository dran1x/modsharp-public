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
using Sharp.Shared.Attributes;
using Sharp.Shared.Enums;
using Sharp.Shared.GameObjects;
using Sharp.Shared.Types;

namespace Sharp.Shared.GameEntities;

[NetClass("CCSWeaponBase")]
public interface IBaseWeapon : IEconEntity
{
    /// <summary>
    ///     是不是匕首
    /// </summary>
    bool IsKnife { get; }

    /// <summary>
    ///     m_iItemDefinitionIndex
    /// </summary>

    ushort ItemDefinitionIndex { get; }

    /// <summary>
    ///     武器的Classname
    /// </summary>
    /// <returns>基于ItemDefinitionIndex换算的Classname</returns>
    /// <exception cref="InvalidOperationException">如果没有判断是否是武器就会直接报错</exception>
    string GetWeaponClassname();

    /// <summary>
    ///     ItemDefinitionName
    /// </summary>
    /// <exception cref="InvalidOperationException">无效的Item或者Weapon会抛这个</exception>
    string GetItemDefinitionName();

    /// <summary>
    ///     ItemDefinition
    /// </summary>
    /// <exception cref="InvalidOperationException">无效的Item或者Weapon会抛这个</exception>
    IEconItemDefinition GetItemDefinition();

    /// <summary>
    ///     WeaponVData
    /// </summary>
    IWeaponData GetWeaponData();

    /// <summary>
    ///     m_nNextPrimaryAttackTick
    /// </summary>
    int NextPrimaryAttackTick { get; set; }

    /// <summary>
    ///     m_nNextSecondaryAttackTick
    /// </summary>
    int NextSecondaryAttackTick { get; set; }

    /// <summary>
    ///     武器弹匣, -1为不使用Clip1, 不是枪时返回-2 <br />
    ///     <remarks>如果超过MaxClip上限, 开枪时会直接刷新为最大值</remarks>
    /// </summary>
    int Clip { get; set; }

    /// <summary>
    ///     武器备弹, -1为不使用ReserveAmmo1, 不是枪时返回-2 <br />
    ///     <remarks>如果超过ReserveAmmo上限, 开枪时会直接刷新为最大值</remarks>
    /// </summary>
    int ReserveAmmo { get; set; }

    /// <summary>
    ///     弹匣最大子弹数 (From VData)
    /// </summary>
    int MaxClip { get; }

    /// <summary>
    ///     最大备弹数 (From VData)
    /// </summary>
    int PrimaryReserveAmmoMax { get; }

    /// <summary>
    ///     武器槽位 (From VData)
    /// </summary>
    GearSlot Slot { get; }

    /// <summary>
    ///     m_hPrevOwner
    /// </summary>
    IPlayerPawn? PrevOwnerEntity { get; }

    /// <summary>
    ///     m_iWeaponGameplayAnimState
    /// </summary>
    WeaponGameplayAnimState WeaponGameplayAnimState { get; set; }

    /// <summary>
    ///     m_flWeaponGameplayAnimStateTimestamp
    /// </summary>
    float WeaponGameplayAnimStateTimestamp { get; set; }

    /// <summary>
    ///     m_weaponMode
    /// </summary>
    CStrikeWeaponMode WeaponMode { get; set; }

    /// <summary>
    ///     m_hPrevOwner
    /// </summary>
    CEntityHandle<IPlayerPawn> PrevOwnerEntityHandle { get; set; }

    /// <summary>
    ///     m_nextOwnerTouchTime
    /// </summary>
    float NextOwnerTouchTime { get; set; }

    /// <summary>
    ///     m_nextPrevOwnerTouchTime
    /// </summary>
    float NextPrevOwnerTouchTime { get; set; }

    /// <summary>
    ///     m_bCanBePickedUp
    /// </summary>
    bool CanBePickedUp { get; set; }

    /// <summary>
    ///     m_fAccuracyPenalty
    /// </summary>
    float AccuracyPenalty { get; set; }

    /// <summary>
    ///     m_flLastAccuracyUpdateTime
    /// </summary>
    float LastAccuracyUpdateTime { get; set; }

    /// <summary>
    ///     m_flRecoilIndex
    /// </summary>
    float RecoilIndex { get; set; }

    /// <summary>
    ///     m_bInReload
    /// </summary>
    bool InReload { get; set; }

    /// <summary>
    ///     m_bSilencerOn
    /// </summary>
    bool SilencerOn { get; set; }

    /// <summary>
    ///     m_flTimeSilencerSwitchComplete
    /// </summary>
    float TimeSilencerSwitchComplete { get; set; }

    /// <summary>
    ///     m_fLastShotTime
    /// </summary>
    float LastShotTime { get; set; }

    /// <summary>
    ///     Holster
    /// </summary>
    void Holster();

    /// <summary>
    ///     Deploy
    /// </summary>
    void Deploy();
}
