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

namespace Sharp.Shared.Enums;

[Flags]
public enum DamageFlagBits : uint
{
    Generic = 0, // 0x0

    /// <summary>
    ///     物理挤压/物理冲击
    /// </summary>
    Crush = 1 << 0, // 0x1

    /// <summary>
    ///     子弹
    /// </summary>
    Bullet = 1 << 1, // 0x2

    /// <summary>
    ///     刀伤/Melee物理
    /// </summary>
    Slash = 1 << 2, // 0x4

    /// <summary>
    ///     燃烧瓶/火焰
    /// </summary>
    Burn = 1 << 3, // 0x8

    /// <summary>
    ///     载具创的
    /// </summary>
    Vehicle = 1 << 4, // 0x10

    /// <summary>
    ///     摔伤
    /// </summary>
    Fall = 1 << 5, // 0x20

    /// <summary>
    ///     爆炸默认/高爆手雷
    /// </summary>
    Blast = 1 << 6, // 0x40

    /// <summary>
    ///     道具砸人
    /// </summary>
    Club = 1 << 7, // 0x80  

    Shock        = 1 << 8,  // 0x100
    Sonic        = 1 << 9,  // 0x200
    EnergyBeam   = 1 << 10, // 0x400
    Buckshot     = 1 << 11, // 0x800
    Drown        = 1 << 14, // 0x4000
    Poison       = 1 << 15, // 0x8000
    Radiation    = 1 << 16, // 0x10000
    DrownRecover = 1 << 17, // 0x20000
    Acid         = 1 << 18, // 0x40000
    PhysGun      = 1 << 20, // 0x100000
    Dissolve     = 1 << 21, // 0x200000
    BlastSurface = 1 << 22, // 0x400000
    Headshot     = 1 << 23, // 0x800000
}
