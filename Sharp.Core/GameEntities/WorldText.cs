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

internal partial class WorldText : BaseModelEntity, IWorldText
{
#region Schemas

    [NativeSchemaField("CPointWorldText", "m_Color", typeof(Color32))]
    private partial SchemaField GetColorField();

    [NativeSchemaField("CPointWorldText", "m_messageText", typeof(char[]))]
    private partial SchemaField GetMessageField();

    [NativeSchemaField("CPointWorldText", "m_flFontSize", typeof(float))]
    private partial SchemaField GetFontSizeField();

    [NativeSchemaField("CPointWorldText", "m_bEnabled", typeof(bool))]
    private partial SchemaField GetEnabledField();

    [NativeSchemaField("CPointWorldText", "m_bFullbright", typeof(bool))]
    private partial SchemaField GetFullBrightField();

    [NativeSchemaField("CPointWorldText", "m_BackgroundMaterialName", typeof(char[]))]
    private partial SchemaField GetBackgroundMaterialNameField();

    [NativeSchemaField("CPointWorldText", "m_bDrawBackground", typeof(bool))]
    private partial SchemaField GetDrawBackgroundField();

    [NativeSchemaField("CPointWorldText", "m_flBackgroundBorderWidth", typeof(float))]
    private partial SchemaField GetBackgroundBorderWidthField();

    [NativeSchemaField("CPointWorldText", "m_flBackgroundBorderHeight", typeof(float))]
    private partial SchemaField GetBackgroundBorderHeightField();

    [NativeSchemaField("CPointWorldText", "m_flBackgroundWorldToUV", typeof(float))]
    private partial SchemaField GetBackgroundWorldToUVField();

#endregion
}
