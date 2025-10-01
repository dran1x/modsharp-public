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

public enum QueryConVarValueStatus
{
    /// <summary>
    ///     It got the value fine.
    /// </summary>
    ValueIntact = 0,

    /// <summary>
    ///     Not Found
    /// </summary>
    CvarNotFound = 1,

    /// <summary>
    ///     There's a ConCommand, but it's not a ConVar.
    /// </summary>
    NotACvar = 2,

    /// <summary>
    ///     The cvar was marked with FCVAR_SERVER_CAN_NOT_QUERY, so the server is not allowed to have its value.
    /// </summary>
    CvarProtected = 3,
}
