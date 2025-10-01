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
public enum ConVarFlags : long
{
    None                  = 0,
    LinkedConCommand      = 1 << 0,
    DevelopmentOnly       = 1 << 1,
    GameDll               = 1 << 2,
    ClientDll             = 1 << 3,
    Hidden                = 1 << 4,
    Protected             = 1 << 5,
    SinglePlayerOnly      = 1 << 6,
    Archive               = 1 << 7,
    Notify                = 1 << 8,
    UserInfo              = 1 << 9,
    Reference             = 1 << 10,
    UnLogged              = 1 << 11,
    InitialSetValue       = 1 << 12,
    Replicated            = 1 << 13,
    Cheat                 = 1 << 14,
    PerUser               = 1 << 15,
    Demo                  = 1 << 16,
    DoNotRecord           = 1 << 17,
    PerformingCallback    = 1 << 18,
    Release               = 1 << 19,
    MenuBarItem           = 1 << 20,
    CommandLineEnforced   = 1 << 21,
    NotConnected          = 1 << 22,
    VConsoleFuzzyMatching = 1 << 23,
    ServerCanExecute      = 1 << 24,
    ClientCanExecute      = 1 << 25,
    ServerCannotQuery     = 1 << 26,
    VConsoleSetFocus      = 1 << 27,
    ClientCmdCanExecute   = 1 << 28,
    ExecutePerTick        = 1 << 29,
}
