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
using Sharp.Shared.Enums;
using Sharp.Shared.Units;

namespace Sharp.Core.Bridges.Forwards;

internal static class Steam
{
    public delegate void DelegateOnGroupStatusResult(SteamID steamId, SteamID groupId, bool member, bool officer);

    public delegate void DelegateOnSteamServersConnected();

    public delegate void DelegateOnSteamServersDisconnected(SteamApiResult reason);

    public delegate void DelegateOnSteamServersConnectFailure(SteamApiResult reason, bool stillRetrying);

    public delegate void DelegateOnDownloadItemResult(ulong sharedFileId, SteamApiResult result);

    public delegate void DelegateOnItemInstalled(ulong sharedFileId);

    public static event DelegateOnGroupStatusResult?          OnGroupStatusResult;
    public static event DelegateOnSteamServersConnected?      OnSteamServersConnected;
    public static event DelegateOnSteamServersDisconnected?   OnSteamServersDisconnected;
    public static event DelegateOnSteamServersConnectFailure? OnSteamServersConnectFailure;
    public static event DelegateOnDownloadItemResult?         OnDownloadItemResult;
    public static event DelegateOnItemInstalled?              OnItemInstalled;

    [UnmanagedCallersOnly]
    public static void OnGroupStatusResultExport(SteamID steamId, SteamID groupId, bool member, bool officer)
        => OnGroupStatusResult?.Invoke(steamId, groupId, member, officer);

    [UnmanagedCallersOnly]
    public static void OnSteamServersConnectedExport()
        => OnSteamServersConnected?.Invoke();

    [UnmanagedCallersOnly]
    public static void OnSteamServersDisconnectedExport(SteamApiResult reason)
        => OnSteamServersDisconnected?.Invoke(reason);

    [UnmanagedCallersOnly]
    public static void OnSteamServersConnectFailureExport(SteamApiResult reason, bool stillRetrying)
        => OnSteamServersConnectFailure?.Invoke(reason, stillRetrying);

    [UnmanagedCallersOnly]
    public static void OnDownloadItemResultExport(ulong sharedFileId, SteamApiResult result)
        => OnDownloadItemResult?.Invoke(sharedFileId, result);

    [UnmanagedCallersOnly]
    public static void OnItemInstalledExport(ulong sharedFileId)
        => OnItemInstalled?.Invoke(sharedFileId);
}
