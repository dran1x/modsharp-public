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

using Sharp.Core.CStrike;
using Sharp.Generator;
using Sharp.Shared;
using Sharp.Shared.CStrike;
using Sharp.Shared.GameObjects;
using Sharp.Shared.Units;

namespace Sharp.Core.GameObjects;

internal partial class DamageRecord : SchemaObject, IDamageRecord, INativeCreatable<DamageRecord>, INativeSizeable
{
    public static uint NativeSize => 120;

#region Schemas

    [NativeSchemaField("CDamageRecord", "m_DamagerXuid", typeof(SteamID))]
    private partial SchemaField GetDamagerXuidField();

    [NativeSchemaField("CDamageRecord", "m_RecipientXuid", typeof(SteamID))]
    private partial SchemaField GetRecipientXuidField();

    [NativeSchemaField("CDamageRecord", "m_flBulletsDamage", typeof(float))]
    private partial SchemaField GetBulletsDamageField();

    [NativeSchemaField("CDamageRecord", "m_flDamage", typeof(float))]
    private partial SchemaField GetDamageField();

    [NativeSchemaField("CDamageRecord", "m_flActualHealthRemoved", typeof(float))]
    private partial SchemaField GetActualHealthRemovedField();

    [NativeSchemaField("CDamageRecord", "m_iNumHits", typeof(int))]
    private partial SchemaField GetNumHitsField();

    [NativeSchemaField("CDamageRecord", "m_killType", typeof(KillTypes))]
    private partial SchemaField GetKillTypeField();

#endregion

    public override string GetSchemaClassname()
        => "CDamageRecord";
}

internal partial class DamageService : PlayerControllerComponent, IDamageService
{
#region Schemas

    [NativeSchemaField("CCSPlayerController_DamageServices",
                       "m_DamageList",
                       typeof(SchemaEmbeddedVector<DamageRecord, IDamageRecord>),
                       InlineObject = true,
                       ReturnType = typeof(ISchemaEmbeddedList<IDamageRecord>))]
    private partial SchemaField GetDamageListField();

#endregion

    public override string GetSchemaClassname()
        => "CCSPlayerController_DamageServices";
}
