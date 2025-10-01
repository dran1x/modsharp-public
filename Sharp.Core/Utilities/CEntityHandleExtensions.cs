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

using Sharp.Core.GameEntities;
using Sharp.Shared.GameEntities;
using Sharp.Shared.Types;

namespace Sharp.Core.Utilities;

public static class CEntityHandleExtensions
{
    public static nint GetEntityPtr<T>(this CEntityHandle<T> handle) where T : class, IBaseEntity
        => Bridges.Natives.Entity.FindByEHandle(handle.GetValue());

    public static T? GetEntity<T>(this CEntityHandle<T> handle) where T : class, IBaseEntity
    {
        var entity = BaseEntity.Create(Bridges.Natives.Entity.FindByEHandle(handle.GetValue()));

        return entity?.As<T>();
    }
}
