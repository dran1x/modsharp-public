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

using Sharp.Core.Utilities;
using Sharp.Generator;
using Sharp.Shared;
using Sharp.Shared.GameEntities;
using Sharp.Shared.Types;

namespace Sharp.Core.GameEntities;

internal partial class BreakableProp : BaseAnimGraph, IBreakableProp
{
    public IBaseEntity? BreakerEntity      => BaseEntity.Create(BreakerEntityHandle.GetEntityPtr());
    public IBaseEntity? LastAttackerEntity => BaseEntity.Create(LastAttackerEntityHandle.GetEntityPtr());

#region Schemas

    [NativeSchemaField("CBreakableProp", "m_iMinHealthDmg", typeof(int))]
    private partial SchemaField GetMinHealthDamageField();

    [NativeSchemaField("CBreakableProp", "m_explodeDamage", typeof(float))]
    private partial SchemaField GetExplodeDamageField();

    [NativeSchemaField("CBreakableProp", "m_explodeRadius", typeof(float))]
    private partial SchemaField GetExplodeRadiusField();

    [NativeSchemaField("CBreakableProp", "m_explosionDelay", typeof(float))]
    private partial SchemaField GetExplosionDelayField();

    [NativeSchemaField("CBreakableProp", "m_hBreaker", typeof(CEntityHandle<IBaseEntity>))]
    private partial SchemaField GetBreakerEntityHandleField();

    [NativeSchemaField("CBreakableProp", "m_hLastAttacker", typeof(CEntityHandle<IBaseEntity>))]
    private partial SchemaField GetLastAttackerEntityHandleField();

#endregion
}
