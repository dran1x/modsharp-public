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

using Microsoft.Extensions.Logging;
using Sharp.Core.Attributes;
using Sharp.Shared;
using Sharp.Shared.Managers;

namespace Sharp.Core.Managers;

internal interface ICoreLibraryModuleManager : ILibraryModuleManager;

[LibraryModuleManager]
internal partial class LibraryModuleManager : ICoreLibraryModuleManager
{
    [LibraryModule("server")]
    public ILibraryModule Server { get; }

    [LibraryModule("engine")]
    public ILibraryModule Engine { get; }

    [LibraryModule("tier0")]
    public ILibraryModule Tier0 { get; }

    [LibraryModule("schemas")]
    public ILibraryModule Schema { get; }

    [LibraryModule("resource")]
    public ILibraryModule Resource { get; }

    [LibraryModule("vscript")]
    public ILibraryModule VScript { get; }

    [LibraryModule("vphysics2")]
    public ILibraryModule VPhysics { get; }

    [LibraryModule("sound")]
    public ILibraryModule SoundSystem { get; }

    [LibraryModule("network")]
    public ILibraryModule NetworkSystem { get; }

    [LibraryModule("worldrenderer")]
    public ILibraryModule WorldRenderer { get; }

    [LibraryModule("MatchMaking")]
    public ILibraryModule MatchMaking { get; }

    [LibraryModule("FileSystem")]
    public ILibraryModule FileSystem { get; }

    [LibraryModule("steamsockets")]
    public ILibraryModule SteamNetworkingSocket { get; }

    [LibraryModule("materialsystem2")]
    public ILibraryModule MaterialSystem { get; }

    private readonly ILogger<LibraryModuleManager> _logger;
}
