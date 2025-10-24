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
using System.Collections.Generic;
using Sharp.Shared.Objects;
using Sharp.Shared.Units;

namespace Sharp.Core.Objects;

internal class Admin : IAdmin
{
    public string  Name     { get; }
    public SteamID Identity { get; }
    public byte    Immunity { get; }

    private readonly HashSet<string> _permissions;

    public Admin(string name, SteamID identity, byte immunity)
    {
        Name     = name;
        Identity = identity;
        Immunity = immunity;

        _permissions = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
    }

    public IReadOnlySet<string> Permissions => _permissions;

    public bool HasPermission(string permission)
        => _permissions.Contains(permission);

    public bool AddPermission(string permission)
        => _permissions.Add(permission);

    public bool RemovePermission(string permission)
        => _permissions.Remove(permission);
}
