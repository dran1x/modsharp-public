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

internal partial class BaseGrenadeProjectile : BaseAnimGraph, IBaseGrenadeProjectile
{
    public IPlayerPawn? ThrowerEntity => PlayerPawn.Create(ThrowerEntityHandle.GetEntityPtr());

#region Schemas

    [NativeSchemaField("CBaseGrenade", "m_hThrower", typeof(CEntityHandle<IPlayerPawn>))]
    private partial SchemaField GetThrowerEntityHandleField();

    [NativeSchemaField("CBaseGrenade", "m_bIsSmokeGrenade", typeof(bool))]
    private partial SchemaField GetIsSmokeGrenadeField();

    [NativeSchemaField("CBaseGrenade", "m_DmgRadius", typeof(float))]
    private partial SchemaField GetDamageRadiusField();

    [NativeSchemaField("CBaseGrenade", "m_flDamage", typeof(float))]
    private partial SchemaField GetDamageField();

    [NativeSchemaField("CBaseGrenade", "m_flDetonateTime", typeof(float))]
    private partial SchemaField GetDetonateTimeField();

    [NativeSchemaField("CBaseCSGrenadeProjectile", "m_flSpawnTime", typeof(float))]
    private partial SchemaField GetSpawnTimeField();

    [NativeSchemaField("CBaseCSGrenadeProjectile", "m_nItemIndex", typeof(ushort))]
    private partial SchemaField GetItemDefinitionIndexField();

    [NativeSchemaField("CBaseCSGrenadeProjectile", "m_vInitialPosition", typeof(Vector))]
    private partial SchemaField GetInitialPositionField();

    [NativeSchemaField("CBaseCSGrenadeProjectile", "m_vInitialVelocity", typeof(Vector))]
    private partial SchemaField GetInitialVelocityField();

    [NativeSchemaField("CBaseCSGrenadeProjectile", "m_vecOriginalSpawnLocation", typeof(Vector))]
    private partial SchemaField GetOriginalSpawnLocationField();

    [NativeSchemaField("CBaseCSGrenadeProjectile", "m_vecLastHitSurfaceNormal", typeof(Vector))]
    private partial SchemaField GetLastHitSurfaceNormalField();

    [NativeSchemaField("CBaseCSGrenadeProjectile", "m_nBounces", typeof(int))]
    private partial SchemaField GetBouncesField();

    [NativeSchemaField("CBaseCSGrenadeProjectile", "m_nTicksAtZeroVelocity", typeof(int))]
    private partial SchemaField GetTicksAtZeroVelocityField();

    [NativeSchemaField("CBaseCSGrenadeProjectile", "m_bHasEverHitEnemy", typeof(bool))]
    private partial SchemaField GetHasEverHitEnemyField();

#endregion
}
