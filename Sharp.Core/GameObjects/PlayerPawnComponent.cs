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
using Sharp.Core.CStrike;
using Sharp.Core.GameEntities;
using Sharp.Core.Helpers;
using Sharp.Shared.GameEntities;
using Sharp.Shared.GameObjects;
using Sharp.Shared.Utilities;

namespace Sharp.Core.GameObjects;

internal abstract partial class PlayerPawnComponent : SchemaObject, IPlayerPawnComponent
{
    internal static readonly Lazy<int> Offset
        = new (() => SchemaSystem.GetNetVarOffset("CPlayerPawnComponent", "__m_pChainEntity"));

    protected PlayerPawnComponent(nint ptr) : base(ptr)
    {
    }

    public IPlayerPawn? GetPlayer()
    {
        var ptr = _this.GetObjectPtr(Offset.Value);

        return ptr == nint.Zero ? null : PlayerPawn.Create(ptr);
    }
}
