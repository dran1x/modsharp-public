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
using Sharp.Core.Objects;

namespace Sharp.Core.Pools;

internal sealed class ConVarPool<T> where T : ConVar
{
    private readonly Dictionary<nint, T> _pool = new (1024);

    internal T? Find(nint pointer)
    {
        if (_pool.Count == 0)
        {
            return null;
        }

        if (!_pool.TryGetValue(pointer, out var cvar))
        {
            return null;
        }

        if (cvar.MatchConVar(pointer))
        {
            return cvar;
        }

        _pool.Remove(pointer);

        return null;
    }

    internal void Set(T cvar)
    {
        ArgumentNullException.ThrowIfNull(cvar, nameof(cvar));

        _pool[cvar.GetAbsPtr()] = cvar;
    }
}
