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

public enum PlayerConnectedState : uint
{
    PlayerNeverConnected = 0xFFFFFFFF,
    PlayerConnected      = 0x0,
    PlayerConnecting     = 0x1,
    PlayerReconnecting   = 0x2,
    PlayerDisconnecting  = 0x3,
    PlayerDisconnected   = 0x4,
    PlayerReserved       = 0x5,
}
