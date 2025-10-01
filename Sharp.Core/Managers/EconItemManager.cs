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

using System.Collections.Frozen;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Sharp.Shared;
using Sharp.Shared.Enums;
using Sharp.Shared.GameObjects;
using Sharp.Shared.Managers;

namespace Sharp.Core.Managers;

internal interface ICoreEconItemManager : IEconItemManager;

internal class EconItemManager : ICoreEconItemManager
{
    private readonly ILogger<EconItemManager> _logger;

    public EconItemManager(ILogger<EconItemManager> logger)
        => _logger = logger;

    public FrozenDictionary<ushort, IEconItemDefinition> GetEconItems()
        => SharedGameObject.EconItemDefinitionsById;

    public FrozenDictionary<uint, IPaintKit> GetPaintKits()
        => SharedGameObject.PaintKits;

    public IEconItemDefinition? GetEconItemDefinitionByIndex(ushort index)
        => SharedGameObject.EconItemDefinitionsById.GetValueOrDefault(index);

    public IEconItemDefinition? GetEconItemDefinitionByIndex(EconItemId index)
        => SharedGameObject.EconItemDefinitionsById.GetValueOrDefault((ushort) index);

    public IEconItemDefinition? GetEconItemDefinitionByName(string name)
        => SharedGameObject.EconItemDefinitionsByName.GetValueOrDefault(name);
}
