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
public enum WorkshopItemState
{
    /// <summary>
    ///     item not tracked on client
    /// </summary>
    ItemStateNone = 0,

    /// <summary>
    ///     current user is subscribed to this item. Not just cached.
    /// </summary>
    ItemStateSubscribed = 1,

    /// <summary>
    ///     item was created with ISteamRemoteStorage
    /// </summary>
    ItemStateLegacyItem = 2,

    /// <summary>
    ///     item is installed and usable (but maybe out of date)
    /// </summary>
    ItemStateInstalled = 4,

    /// <summary>
    ///     items needs an update. Either because it's not installed yet or creator updated content
    /// </summary>
    ItemStateNeedsUpdate = 8,

    /// item update is currently downloading
    ItemStateDownloading = 16,

    /// DownloadItem() was called for this item, content isn't available until DownloadItemResult_t is fired
    ItemStateDownloadPending = 32,
}
