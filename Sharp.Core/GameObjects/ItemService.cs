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
using Sharp.Core.GameEntities;
using Sharp.Core.VirtualCalls;
using Sharp.Generator;
using Sharp.Shared;
using Sharp.Shared.Enums;
using Sharp.Shared.GameEntities;
using Sharp.Shared.GameObjects;

namespace Sharp.Core.GameObjects;

internal partial class ItemService : PlayerPawnComponent, IItemService
{
    public IBaseWeapon? GiveNamedItem(string classname)
        => BaseWeapon.Create(ItemServiceVirtualCall.Instance.GiveNamedItem(this, classname));

    public IBaseWeapon? GiveNamedItem(EconItemId id)
    {
        if (!Enum.IsDefined(id) || !SharedGameObject.EconItemDefinitionsById.TryGetValue((ushort) id, out var value))
        {
            throw new InvalidOperationException($"Invalid ItemId {id}");
        }

        return GiveNamedItem(value.DefinitionName);
    }

    public void RemoveAllItems(bool removeSuit)
        => ItemServiceVirtualCall.Instance.RemoveAllItems(this, removeSuit);

#region Schemas

    [NativeSchemaField("CCSPlayer_ItemServices", "m_bHasDefuser", typeof(bool))]
    private partial SchemaField GetHasDefuserField();

    [NativeSchemaField("CCSPlayer_ItemServices", "m_bHasHelmet", typeof(bool))]
    private partial SchemaField GetHasHelmetField();

#endregion

    public override string GetSchemaClassname()
        => "CCSPlayer_ItemServices";
}
