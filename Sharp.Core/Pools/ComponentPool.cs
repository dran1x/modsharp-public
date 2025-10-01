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
using Sharp.Core.GameEntities;
using Sharp.Shared.CStrike;
using Sharp.Shared.Utilities;

namespace Sharp.Core.Pools;

internal abstract class ComponentPool<T> where T : class, INativeObject
{
    // ReSharper disable InconsistentNaming
    // ReSharper disable MemberCanBePrivate.Global
    protected readonly Dictionary<uint, T> _pool;

    protected readonly int _offset;

    // ReSharper restore MemberCanBePrivate.Global
    // ReSharper restore InconsistentNaming

    protected ComponentPool(int buffer, int offset)
    {
        _pool   = new Dictionary<uint, T>(buffer);
        _offset = offset;

        Game.OnRoundRestart        += () => _pool.Clear();
        Game.OnGameActivate        += CleanObjectState;
        Game.OnGameShutdown        += CleanObjectState;
        Entity.OnEntityDeletedPost += OnEntityDeleted;
    }

    private void OnEntityDeleted(nint pointer)
    {
        if (pointer == nint.Zero)
        {
            return;
        }

        if (_pool.Count == 0)
        {
            return;
        }

        var handle = BaseEntity.GetHandle(pointer).GetValue();

        if (!_pool.Remove(handle, out var e))
        {
            return;
        }

        if (e is ContextObject context)
        {
            context.MarkAsDisposed();
        }
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

    internal T? Find(nint absPointer)
    {
        if (_pool.Count == 0)
        {
            return null;
        }

        var chainPointer = absPointer.GetObjectPtr(_offset);

        if (chainPointer == nint.Zero)
        {
            return null;
        }

        var handle = BaseEntity.GetHandle(chainPointer).GetValue();

        if (_pool.TryGetValue(handle, out var value))
        {
            if (!value.IsDisposed)
            {
                return value;
            }

            _pool.Remove(handle);
        }

        return null;
    }

    internal void Set(T component)
    {
        ArgumentNullException.ThrowIfNull(component, nameof(component));

        var chainPointer = component.GetAbsPtr().GetObjectPtr(_offset);

        if (chainPointer == nint.Zero)
        {
            // Error??
            return;
        }

        if (BaseEntity.IsMarkedForDelete(chainPointer))
        {
            return;
        }

        var handle = BaseEntity.GetHandle(chainPointer).GetValue();

        _pool[handle] = component;
    }
}
