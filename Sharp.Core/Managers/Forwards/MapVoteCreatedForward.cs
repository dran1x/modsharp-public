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

using Microsoft.Extensions.Logging;
using Sharp.Core.Objects;
using Sharp.Shared.HookParams;
using Sharp.Shared.Objects;

namespace Sharp.Core.Managers.Forwards;

internal class MapVoteCreatedForward : ForwardType<MapVoteCreatedForwardParams, MapVoteCreatedForward>
{
    public MapVoteCreatedForward(ILoggerFactory factory) : base(factory)
        => Bridges.Forwards.Game.OnMapVoteCreated += GameRules_MapVoteCreated;

    private void GameRules_MapVoteCreated(nint ptr)
    {
        if (!IsForwardInvokeRequired())
        {
            return;
        }

        var param = new MapVoteCreatedForwardParams(true, ptr);
        InvokeForward(param);
        param.MarkAsDisposed();
    }
}

internal sealed class MapVoteCreatedForwardParams : FunctionParams, IMapVoteCreatedForwardParams
{
    private readonly GameRules _gameRules;

    public MapVoteCreatedForwardParams(bool postHook, nint gameRulesPtr) : base(postHook)
        => _gameRules = Sharp.Core.Objects.GameRules.Create(gameRulesPtr)!;

    protected override void OnDisposed()
    {
        _gameRules.MarkAsDisposed();
        base.OnDisposed();
    }

    public IGameRules GameRules => _gameRules;
}
