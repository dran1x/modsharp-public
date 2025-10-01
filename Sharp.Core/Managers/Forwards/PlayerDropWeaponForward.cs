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
using Sharp.Shared.HookParams;

namespace Sharp.Core.Managers.Forwards;

internal class PlayerDropWeaponForward : ForwardType<PlayerDropWeaponForwardParams, PlayerDropWeaponForward>
{
    public PlayerDropWeaponForward(ILoggerFactory factory) : base(factory)
        => Bridges.Forwards.Player.OnPlayerDropWeapon += WeaponService_DropWeapon;

    private void WeaponService_DropWeapon(nint ptrClient, nint ptrController, nint ptrPawn, nint ptrService, nint ptrWeapon)
    {
        if (!IsForwardInvokeRequired())
        {
            return;
        }

        var param = new PlayerDropWeaponForwardParams(true, ptrClient, ptrController, ptrPawn, ptrService, ptrWeapon);
        InvokeForward(param);
        param.MarkAsDisposed();
    }
}

internal sealed class PlayerDropWeaponForwardParams : PlayerWeaponWithServiceFunctionParams, IPlayerDropWeaponForwardParams
{
    public PlayerDropWeaponForwardParams(bool postHook,
        nint                                  client,
        nint                                  controller,
        nint                                  pawn,
        nint                                  service,
        nint                                  weapon) : base(postHook,
                                                             client,
                                                             controller,
                                                             pawn,
                                                             weapon,
                                                             service)
    {
    }
}
