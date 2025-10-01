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

using System.Runtime.InteropServices;
using Sharp.Core.Objects;
using Sharp.Shared.Enums;
using Sharp.Shared.Utilities;

namespace Sharp.Core.Bridges.Forwards;

internal static class Cvar
{
    public delegate void DelegateOnConVarChanged(nint ptr);

    public static event DelegateOnConVarChanged? OnConVarChanged;

    [UnmanagedCallersOnly]
    public static void OnConVarChangedExport(nint ptr)
        => OnConVarChanged?.Invoke(ptr);

    public delegate ECommandAction DelegateOnConCommandTrigger(long handle, GameClient? client, string command);

    public static event DelegateOnConCommandTrigger? OnConCommandTrigger;

    [UnmanagedCallersOnly]
    public static unsafe ECommandAction OnConCommandTriggerExport(long handle, nint client, sbyte* pCommand)
        => OnConCommandTrigger?.Invoke(handle, GameClient.Create(client), Utils.ReadString(pCommand)) ?? ECommandAction.Skipped;
}
