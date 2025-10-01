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

using Sharp.Generator;
using Sharp.Shared;
using Sharp.Shared.GameEntities;
using Sharp.Shared.Types;

namespace Sharp.Core.GameEntities;

internal partial class BaseParticle : BaseModelEntity, IBaseParticle
{
#region Schemas

    [NativeSchemaField("CParticleSystem", "m_szSnapshotFileName", typeof(char[]))]
    private partial SchemaField GetSnapshotFileNameField();

    [NativeSchemaField("CParticleSystem", "m_bActive", typeof(bool))]
    private partial SchemaField GetActiveField();

    [NativeSchemaField("CParticleSystem", "m_bFrozen", typeof(bool))]
    private partial SchemaField GetFrozenField();

    [NativeSchemaField("CParticleSystem", "m_flFreezeTransitionDuration", typeof(float))]
    private partial SchemaField GetFreezeTransitionDurationField();

    [NativeSchemaField("CParticleSystem", "m_nStopType", typeof(int))]
    private partial SchemaField GetStopTypeField();

    [NativeSchemaField("CParticleSystem", "m_flStartTime", typeof(float))]
    private partial SchemaField GetStartTimeField();

    [NativeSchemaField("CParticleSystem", "m_flPreSimTime", typeof(float))]
    private partial SchemaField GetPreSimTimeField();

    [NativeSchemaField("CParticleSystem", "m_bNoSave", typeof(bool))]
    private partial SchemaField GetNoSaveField();

    [NativeSchemaField("CParticleSystem", "m_bNoFreeze", typeof(bool))]
    private partial SchemaField GetNoFreezeField();

    [NativeSchemaField("CParticleSystem", "m_bNoRamp", typeof(bool))]
    private partial SchemaField GetNoRampField();

    [NativeSchemaField("CParticleSystem", "m_bStartActive", typeof(bool))]
    private partial SchemaField GetStartActiveField();

    [NativeSchemaField("CParticleSystem", "m_nDataCP", typeof(int))]
    private partial SchemaField GetDataControlPointField();

    [NativeSchemaField("CParticleSystem", "m_vecDataCPValue", typeof(Vector))]
    private partial SchemaField GetDataControlPointValueField();

    [NativeSchemaField("CParticleSystem", "m_nTintCP", typeof(int))]
    private partial SchemaField GetTintControlPointField();

    [NativeSchemaField("CParticleSystem", "m_clrTint", typeof(Color32))]
    private partial SchemaField GetTintColorField();

    [NativeSchemaField("CParticleSystem",
                       "m_vServerControlPoints",
                       typeof(Vector[]))]
    private partial SchemaField GetServerControlPointsField();

    [NativeSchemaField("CParticleSystem",
                       "m_iServerControlPointAssignments",
                       typeof(byte[]))]
    private partial SchemaField GetServerControlPointAssignmentsField();

    [NativeSchemaField("CParticleSystem",
                       "m_hControlPointEnts",
                       typeof(CEntityHandle<IBaseEntity>[]))]
    private partial SchemaField GetControlPointEntitiesField();

#endregion
}
