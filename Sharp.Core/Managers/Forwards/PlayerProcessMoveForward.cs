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
using Microsoft.Extensions.Logging;
using Sharp.Shared.HookParams;

namespace Sharp.Core.Managers.Forwards;

internal class PlayerProcessMovePreForward : ForwardType<PlayerProcessMoveForwardParams, PlayerProcessMovePreForward>
{
    public PlayerProcessMovePreForward(ILoggerFactory factory) : base(factory)
        => Bridges.Forwards.Player.OnPlayerProcessMovePre += CCSPlayerMovementService_ProcessMovePre;

    private void CCSPlayerMovementService_ProcessMovePre(nint ptrClient,
        nint                                                  ptrController,
        nint                                                  ptrPawn,
        nint                                                  ptrService,
        nint                                                  ptrMoveData)
    {
        if (!IsForwardInvokeRequired())
        {
            return;
        }

        var param = new PlayerProcessMoveForwardParams(false, ptrClient, ptrController, ptrPawn, ptrService, ptrMoveData);
        InvokeForward(param);
        param.MarkAsDisposed();
    }
}

internal class PlayerProcessMovePostForward : ForwardType<PlayerProcessMoveForwardParams, PlayerProcessMovePostForward>
{
    public PlayerProcessMovePostForward(ILoggerFactory factory) : base(factory)
        => Bridges.Forwards.Player.OnPlayerProcessMovePost += CCSPlayerMovementService_ProcessMovePost;

    private void CCSPlayerMovementService_ProcessMovePost(nint ptrClient,
        nint                                                   ptrController,
        nint                                                   ptrPawn,
        nint                                                   ptrService,
        nint                                                   ptrMoveData)
    {
        if (!IsForwardInvokeRequired())
        {
            return;
        }

        var param = new PlayerProcessMoveForwardParams(true, ptrClient, ptrController, ptrPawn, ptrService, ptrMoveData);
        InvokeForward(param);
        param.MarkAsDisposed();
    }
}

internal sealed class PlayerProcessMoveForwardParams : PlayerMovementWithDataFunctionParams, IPlayerProcessMoveForwardParams
{
    public PlayerProcessMoveForwardParams(bool postHook,
        IntPtr                                 client,
        IntPtr                                 controller,
        IntPtr                                 pawn,
        IntPtr                                 ptrService,
        IntPtr                                 ptrInfo) : base(postHook, client, controller, pawn, ptrService, ptrInfo)
    {
    }
}
