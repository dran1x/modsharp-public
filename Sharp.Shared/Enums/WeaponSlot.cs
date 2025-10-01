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

public enum GearSlot
{
    Invalid = -1,

    /// <summary>
    ///     步枪
    /// </summary>
    Rifle = 0,

    /// <summary>
    ///     手枪
    /// </summary>
    Pistol = 1,

    /// <summary>
    ///     刀
    /// </summary>
    Knife = 2,

    /// <summary>
    ///     手雷
    /// </summary>
    Grenades = 3,

    /// <summary>
    ///     C4
    /// </summary>
    C4 = 4,

    ReservedSlot6  = 5,
    ReservedSlot7  = 6,
    ReservedSlot8  = 7,
    ReservedSlot9  = 8,
    ReservedSlot10 = 9,
    ReservedSlot11 = 10,
    Boosts         = 11,
    Utility        = 12,
}
