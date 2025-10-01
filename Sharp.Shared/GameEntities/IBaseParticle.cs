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
using Sharp.Shared.CStrike;
using Sharp.Shared.Types;

namespace Sharp.Shared.GameEntities;

[NetClass("CParticleSystem")]
public interface IBaseParticle : IBaseModelEntity
{
    /// <summary>
    ///     m_szSnapshotFileName
    /// </summary>
    string SnapshotFileName { get; set; }

    /// <summary>
    ///     m_bActive
    /// </summary>
    bool Active { get; set; }

    /// <summary>
    ///     m_bFrozen
    /// </summary>
    bool Frozen { get; set; }

    /// <summary>
    ///     m_flFreezeTransitionDuration
    /// </summary>
    float FreezeTransitionDuration { get; set; }

    /// <summary>
    ///     m_nStopType
    /// </summary>
    int StopType { get; set; }

    /// <summary>
    ///     m_flStartTime
    /// </summary>
    float StartTime { get; set; }

    /// <summary>
    ///     m_flPreSimTime
    /// </summary>
    float PreSimTime { get; set; }

    /// <summary>
    ///     m_bNoFreeze
    /// </summary>
    bool NoFreeze { get; set; }

    /// <summary>
    ///     m_bNoRamp
    /// </summary>
    bool NoRamp { get; set; }

    /// <summary>
    ///     m_bStartActive
    /// </summary>
    bool StartActive { get; set; }

    /// <summary>
    ///     m_nDataCP
    /// </summary>
    int DataControlPoint { get; set; }

    /// <summary>
    ///     m_vecDataCPValue
    /// </summary>
    Vector DataControlPointValue { get; set; }

    /// <summary>
    ///     m_nTintCP
    /// </summary>
    int TintControlPoint { get; set; }

    /// <summary>
    ///     m_clrTint
    /// </summary>
    Color32 TintColor { get; set; }

    /// <summary>
    ///     m_vServerControlPoints
    /// </summary>
    ISchemaArray<Vector> GetServerControlPoints();

    /// <summary>
    ///     m_iServerControlPointAssignments
    /// </summary>
    ISchemaArray<byte> GetServerControlPointAssignments();

    /// <summary>
    ///     m_hControlPointEnts
    /// </summary>
    ISchemaArray<CEntityHandle<IBaseEntity>> GetControlPointEntities();
}
