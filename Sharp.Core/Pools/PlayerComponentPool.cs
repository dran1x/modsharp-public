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

using Sharp.Core.GameObjects;
using Sharp.Shared.CStrike;

namespace Sharp.Core.Pools;

internal abstract class PlayerComponentPool<T> : ComponentPool<T> where T : class, INativeObject
{
    protected PlayerComponentPool(int offset) : base(128, offset)
    {
    }
}

internal sealed class PlayerPawnComponentPool<T> : PlayerComponentPool<T> where T : PlayerPawnComponent
{
    public PlayerPawnComponentPool() : base(PlayerPawnComponent.Offset.Value)
    {
    }
}

internal sealed class PlayerControllerComponentPool<T> : PlayerComponentPool<T> where T : PlayerControllerComponent
{
    public PlayerControllerComponentPool() : base(PlayerControllerComponent.Offset.Value)
    {
    }
}
