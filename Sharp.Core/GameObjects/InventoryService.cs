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
using Sharp.Shared.GameObjects;

namespace Sharp.Core.GameObjects;

internal partial class InventoryService : PlayerControllerComponent, IInventoryService
{
#region Schemas

    [NativeSchemaField("CCSPlayerController_InventoryServices", "m_unMusicID", typeof(ushort))]
    private partial SchemaField GetMusicIdField();

    [NativeSchemaField("CCSPlayerController_InventoryServices", "m_nPersonaDataXpTrailLevel", typeof(int))]
    private partial SchemaField GetPersonaDataXpTrailLevelField();

    [NativeSchemaField("CCSPlayerController_InventoryServices", "m_nPersonaDataPublicLevel", typeof(int))]
    private partial SchemaField GetPersonaDataPublicLevelField();

#endregion

    public override string GetSchemaClassname()
        => "CCSPlayerController_InventoryServices";
}
