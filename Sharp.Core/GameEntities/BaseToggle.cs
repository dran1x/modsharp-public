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
using Sharp.Shared.Enums;
using Sharp.Shared.GameEntities;
using Sharp.Shared.Types;

namespace Sharp.Core.GameEntities;

internal partial class BaseToggle : BaseModelEntity, IBaseToggle
{
    public IBaseEntity? ActivatorEntity => BaseEntity.Create(ActivatorEntityHandle.GetEntityPtr());

#region Schemas

    [NativeSchemaField("CBaseToggle", "m_flWait", typeof(float))]
    private partial SchemaField GetWaitField();

    [NativeSchemaField("CBaseToggle", "m_flMoveDistance", typeof(float))]
    private partial SchemaField GetMoveDistanceField();

    [NativeSchemaField("CBaseToggle", "m_vecPosition1", typeof(Vector))]
    private partial SchemaField GetPosition1Field();

    [NativeSchemaField("CBaseToggle", "m_vecPosition2", typeof(Vector))]
    private partial SchemaField GetPosition2Field();

    [NativeSchemaField("CBaseToggle", "m_hActivator", typeof(CEntityHandle<IBaseEntity>))]
    private partial SchemaField GetActivatorEntityHandleField();

    [NativeSchemaField("CBaseToggle", "m_toggle_state", typeof(ToggleState))]
    private partial SchemaField GetToggleStateField();

#endregion
}
