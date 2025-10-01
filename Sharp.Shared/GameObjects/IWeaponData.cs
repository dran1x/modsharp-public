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

using Sharp.Shared.Attributes;
using Sharp.Shared.CStrike;
using Sharp.Shared.Enums;

namespace Sharp.Shared.GameObjects;

[NetClass("CCSWeaponBaseVData")]
public interface IWeaponData : IEntitySubclassVData
{
    /// <summary>
    ///     最大弹匣子弹
    /// </summary>
    int MaxClip { get; set; }

    /// <summary>
    ///     类型
    /// </summary>
    CStrikeWeaponType WeaponType { get; }

    /// <summary>
    ///     种类
    /// </summary>
    CStrikeWeaponCategory WeaponCategory { get; }

    /// <summary>
    ///     槽位
    /// </summary>
    GearSlot GearSlot { get; }

    /// <summary>
    ///     购买价格
    /// </summary>
    int Price { get; set; }

    /// <summary>
    ///     最大备单数量
    /// </summary>
    int PrimaryReserveAmmoMax { get; set; }

    /// <summary>
    ///     是否是近战武器
    /// </summary>
    bool IsMeleeWeapon { get; }

    /// <summary>
    ///     是否全自动
    /// </summary>
    bool IsFullAuto { get; }

    /// <summary>
    ///     弹丸数量
    /// </summary>
    int NumBullets { get; set; }

    /// <summary>
    ///     Primary射击循环周期
    /// </summary>
    float CycleTime { get; set; }

    /// <summary>
    ///     Secondary射击循环周期
    /// </summary>
    float CycleTimeAlt { get; set; }

    /// <summary>
    ///     抓在手上时的最大移动速度
    /// </summary>
    float MaxSpeed { get; set; }

    /// <summary>
    ///     Max Player Speed Alt
    /// </summary>
    float MaxSpeedAlt { get; set; }

    /// <summary>
    ///     伤害
    /// </summary>
    int Damage { get; set; }

    /// <summary>
    ///     爆头倍率
    /// </summary>
    float HeadshotMultiplier { get; set; }

    /// <summary>
    ///     护甲穿透倍率
    /// </summary>
    float ArmorRatio { get; set; }

    /// <summary>
    ///     射程
    /// </summary>
    float Range { get; set; }

    /// <summary>
    ///     距离衰减倍率
    /// </summary>
    float RangeModifier { get; set; }
}
