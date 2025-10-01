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

internal partial class BaseAnimGraph : BaseModelEntity, IBaseAnimGraph
{
#region Schemas

    [NativeSchemaField("CBaseAnimGraph", "m_bAnimGraphUpdateEnabled", typeof(bool))]
    private partial SchemaField GetAnimGraphUpdateEnabledField();

    [NativeSchemaField("CBaseAnimGraph", "m_vecForce", typeof(Vector))]
    private partial SchemaField GetForceField();

    [NativeSchemaField("CBaseAnimGraph", "m_nForceBone", typeof(int))]
    private partial SchemaField GetForceBoneField();

#endregion
}
