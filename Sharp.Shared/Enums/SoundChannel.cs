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

public enum SoundChannel : int
{
    /// <summary>
    ///     Unknown
    /// </summary>
    Replace = -1,

    /// <summary>
    ///     Auto
    /// </summary>
    Auto = 0,

    /// <summary>
    ///     Weapons
    /// </summary>
    Weapon = 1,

    /// <summary>
    ///     Voices
    /// </summary>
    Voice = 2,

    /// <summary>
    ///     Items
    /// </summary>
    Item = 3,

    /// <summary>
    ///     Player?
    /// </summary>
    Body = 4,

    /// <summary>
    ///     "Stream channel from the static or dynamic area"
    /// </summary>
    Stream = 5,

    /// <summary>
    ///     "Stream channel from the static area"
    /// </summary>
    Static = 6,

    /// <summary>
    ///     "Channel for network voice data"
    /// </summary>
    VoiceBase = 7,

    /// <summary>
    ///     Anything >= this is allocated to game code
    /// </summary>
    UserBase = 135,
}
