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

namespace Sharp.Shared.Enums;

public enum WeaponSoundType : uint
{
    Empty            = 0x0,
    SecondaryEmpty   = 0x1,
    Single           = 0x2,
    SecondaryAttack  = 0x3,
    MeleeMiss        = 0x4,
    MeleeHit         = 0x5,
    MeleeHitWorld    = 0x6,
    MeleeHitPlayer   = 0x7,
    MeleeHitNpc      = 0x8,
    Special1         = 0x9,
    Special2         = 0xA,
    Special3         = 0xB,
    NearlyEmpty      = 0xC,
    Impact           = 0xD,
    Reflect          = 0xE,
    SecondaryImpact  = 0xF,
    SecondaryReflect = 0x10,
    Reload           = 0x11,
    SingleAccurate   = 0x12,
    ZoomIn           = 0x13,
    ZoomOut          = 0x14,
    MousePressed     = 0x15,
    Drop             = 0x16,
    RadioUse         = 0x17,
    NumTypes         = 0x18,
}
