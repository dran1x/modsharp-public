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

using Sharp.Shared.Attributes;
using Sharp.Shared.Types;

namespace Sharp.Shared.GameEntities;

[NetClass("CBaseCSGrenadeProjectile")]
public interface IBaseGrenadeProjectile : IBaseAnimGraph
{
    /// <summary>
    ///     m_bIsSmokeGrenade
    /// </summary>
    bool IsSmokeGrenade { get; }

    /// <summary>
    ///     m_flSpawnTime
    /// </summary>
    float SpawnTime { get; set; }

    /// <summary>
    ///     m_nItemIndex
    /// </summary>
    ushort ItemDefinitionIndex { get; set; }

    /// <summary>
    ///     m_DmgRadius
    /// </summary>
    float DamageRadius { get; set; }

    /// <summary>
    ///     m_flDamage
    /// </summary>
    float Damage { get; set; }

    /// <summary>
    ///     m_flDetonateTime
    /// </summary>
    float DetonateTime { get; set; }

    /// <summary>
    ///     m_hThrower
    /// </summary>
    CEntityHandle<IPlayerPawn> ThrowerEntityHandle { get; set; }

    /// <summary>
    ///     m_hThrower
    /// </summary>
    IPlayerPawn? ThrowerEntity { get; }

    /// <summary>
    ///     m_vInitialPosition
    /// </summary>
    Vector InitialPosition { get; }

    /// <summary>
    ///     m_vInitialVelocity
    /// </summary>
    Vector InitialVelocity { get; }

    /// <summary>
    ///     m_vecOriginalSpawnLocation
    /// </summary>
    Vector OriginalSpawnLocation { get; }

    /// <summary>
    ///     m_vecLastHitSurfaceNormal
    /// </summary>
    Vector LastHitSurfaceNormal { get; }

    /// <summary>
    ///     m_nBounces
    /// </summary>
    int Bounces { get; set; }

    /// <summary>
    ///     m_nTicksAtZeroVelocity
    /// </summary>
    int TicksAtZeroVelocity { get; set; }

    /// <summary>
    ///     m_bHasEverHitEnemy
    /// </summary>

    bool HasEverHitEnemy { get; set; }
}
