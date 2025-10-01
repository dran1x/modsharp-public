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
using Sharp.Core.Bridges.Forwards;
using Sharp.Core.CStrike;
using Sharp.Core.GameObjects;
using Sharp.Shared.CStrike;
using Sharp.Shared.Utilities;

namespace Sharp.Core.Pools;

// VData通常只有地图读取和更换地图是才会失效

internal abstract class VDataPool<T> where T : class, INativeObject
{
    private readonly Dictionary<int, T> _pool;

    private readonly int _offsetKey;
    private readonly int _offsetName;

    protected VDataPool(int offsetKey, int offsetName)
    {
        _pool = new Dictionary<int, T>(1024);

        _offsetKey  = offsetKey;
        _offsetName = offsetName;

        Game.OnGameInit     += CleanObjectState;
        Game.OnGameShutdown += CleanObjectState;
    }

    private void CleanObjectState()
    {
        if (_pool.Count == 0)
        {
            return;
        }

        foreach (var (_, e) in _pool)
        {
            if (e is ContextObject context)
            {
                context.MarkAsDisposed();
            }
        }

        _pool.Clear();
    }

    internal T? Find(nint pointer)
    {
        if (_pool.Count == 0)
        {
            return null;
        }

        var currentKey  = pointer.Add(_offsetKey);
        var currentName = pointer.Add(_offsetName);
        var hashCode    = HashCode.Combine(pointer, currentKey, currentName);

        if (_pool.TryGetValue(hashCode, out var data))
        {
            if (!data.IsDisposed)
            {
                return data;
            }

            _pool.Remove(hashCode);
        }

        return null;
    }

    internal void Set(T data)
    {
        ArgumentNullException.ThrowIfNull(data, nameof(data));

        var pointer = data.GetAbsPtr();

        if (pointer == nint.Zero)
        {
            // Error??
            return;
        }

        var currentKey  = pointer.Add(_offsetKey);
        var currentName = pointer.Add(_offsetName);
        var hashCode    = HashCode.Combine(pointer, currentKey, currentName);

        _pool[hashCode] = data;
    }
}

internal sealed class WeaponDataPool : VDataPool<WeaponData>
{
    // 0x10 = Key
    // 0x18 = Name

    public WeaponDataPool() : base(0x10, 0x18)
    {
    }
}
