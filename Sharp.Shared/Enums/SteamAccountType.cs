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

public enum SteamAccountType : byte
{
    Invalid = 0,

    /// <summary>
    ///     single user account
    /// </summary>
    Individual = 1,

    /// <summary>
    ///     multiseat (e.g. cybercafe) account
    /// </summary>
    Multiseat = 2,

    /// <summary>
    ///     game server account
    /// </summary>
    GameServer = 3,

    /// <summary>
    ///     anonymous game server account
    /// </summary>
    AnonGameServer = 4,

    /// <summary>
    ///     pending
    /// </summary>
    Pending = 5,

    /// <summary>
    ///     content server
    /// </summary>
    ContentServer = 6,

    /// <summary>
    ///     Steam Group
    /// </summary>
    Clan = 7,

    /// <summary>
    ///     Chat
    /// </summary>
    Chat = 8,

    /// <summary>
    ///     Fake SteamID for local PSN account on PS3 or Live account on 360, etc.
    /// </summary>
    ConsoleUser = 9,

    /// <summary>
    ///     Anonymous
    /// </summary>
    AnonUser = 10,

    // Max of 16 items in this field
    AccountTypeMax,
}
