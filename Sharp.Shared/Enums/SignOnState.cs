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

public enum SignOnState
{
    /// <summary>
    ///     no state yet; about to connect
    /// </summary>
    None = 0,

    /// <summary>
    ///     client challenging server; all OOB packets
    /// </summary>
    Challenge = 1,

    /// <summary>
    ///     client is connected to server; NetChan ready
    /// </summary>
    Connected = 2,

    /// <summary>
    ///     just got ServerInfo and string tables
    /// </summary>
    New = 3,

    /// <summary>
    ///     received signon buffers
    /// </summary>
    PreSpawn = 4,

    /// <summary>
    ///     ready to receive entity packets
    /// </summary>
    Spawn = 5,

    /// <summary>
    ///     fully connected; first non-delta packet received
    /// </summary>
    Full = 6,

    /// <summary>
    ///     server is changing level; please wait
    /// </summary>
    ChangeLevel = 7,
}
