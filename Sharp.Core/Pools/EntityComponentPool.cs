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

using Sharp.Core.Helpers;
using Sharp.Shared.CStrike;

namespace Sharp.Core.Pools;

internal sealed class EntityComponentPool<T> : ComponentPool<T> where T : class, IEntityComponent
{
    public EntityComponentPool(string schemaClass, string field) : base(512, SchemaSystem.GetNetVarOffset(schemaClass, field))
    {
    }
}

internal sealed class EntityObjectPool<T> : ComponentPool<T> where T : class, ISchemaObject
{
    public EntityObjectPool(int offset) : base(512, offset)
    {
    }
}
