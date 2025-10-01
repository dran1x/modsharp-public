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

using Sharp.Shared.Enums;
using Sharp.Shared.Units;

namespace Sharp.Shared.Listeners;

public interface ISteamListener
{
    const int ApiVersion = 2;

    /// <summary>
    ///     接口实现的版本号
    /// </summary>
    int ListenerVersion { get; }

    /// <summary>
    ///     优先级
    /// </summary>
    int ListenerPriority { get; }

    /// <summary>
    ///     调用 RequestUserGroupStatus 后触发
    /// </summary>
    void OnGroupStatusResult(SteamID steamId, SteamID groupId, bool isMember, bool isOfficer)
    {
    }

    /// <summary>
    ///     服务器连上Steam
    /// </summary>
    void OnSteamServersConnected()
    {
    }

    /// <summary>
    ///     服务器断联Steam
    /// </summary>
    void OnSteamServersDisconnected(SteamApiResult reason)
    {
    }

    /// <summary>
    ///     服务器连不上Steam
    /// </summary>
    void OnSteamServersConnectFailure(SteamApiResult reason, bool stillRetrying)
    {
    }

    /// <summary>
    ///     UGC下载
    /// </summary>
    void OnDownloadItemResult(ulong sharedFileId, SteamApiResult result)
    {
    }

    /// <summary>
    ///     UGC安装
    /// </summary>
    /// <param name="publishedFileId">创意工坊ID</param>
    void OnItemInstalled(ulong publishedFileId)
    {
    }
}
