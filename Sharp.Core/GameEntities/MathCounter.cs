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
using Sharp.Generator;
using Sharp.Shared;
using Sharp.Shared.GameEntities;

namespace Sharp.Core.GameEntities;

internal partial class MathCounter : BaseEntity, IMathCounter
{
    // m_OutValue :: CEntityOutputTemplate<float>
    // CEntityOutputTemplate + 24 = value
    // CEntityOutputTemplate + 32 = type

    private const ushort CEntityOutputTemplateOffsetValue = 24;

    private static SchemaField? _outValueSchemaField;

    public unsafe float Value
    {
        get
        {
            _outValueSchemaField ??= Helpers.SchemaSystem.GetSchemaField("CMathCounter", "m_OutValue");

            return *(float*) IntPtr.Add(_this, _outValueSchemaField.Offset + CEntityOutputTemplateOffsetValue);
        }
        set
        {
            _outValueSchemaField ??= Helpers.SchemaSystem.GetSchemaField("CMathCounter", "m_OutValue");

            // 不需要Network
            *(float*) IntPtr.Add(_this, _outValueSchemaField.Offset + CEntityOutputTemplateOffsetValue) = value;
        }
    }

#region Schemas

    [NativeSchemaField("CMathCounter", "m_flMin", typeof(float))]
    private partial SchemaField GetMinValueField();

    [NativeSchemaField("CMathCounter", "m_flMax", typeof(float))]
    private partial SchemaField GetMaxValueField();

    [NativeSchemaField("CMathCounter", "m_bDisabled", typeof(bool))]
    private partial SchemaField GetDisabledField();

#endregion
}
