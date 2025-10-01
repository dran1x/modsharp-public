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

public enum CStrikeWeaponType : uint
{
    Knife         = 0x0,
    Pistol        = 0x1,
    SubmachineGun = 0x2,
    Rifle         = 0x3,
    Shotgun       = 0x4,
    SniperRifle   = 0x5,
    MachineGun    = 0x6,
    C4            = 0x7,
    Taser         = 0x8,
    Grenade       = 0x9,
    Equipment     = 0xA,
    StackableItem = 0xB,
    Unknown       = 0xC,
}
