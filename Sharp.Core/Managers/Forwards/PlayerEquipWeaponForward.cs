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

internal class PlayerEquipWeaponForward : ForwardType<PlayerEquipWeaponForwardParams, PlayerEquipWeaponForward>
{
    public PlayerEquipWeaponForward(ILoggerFactory factory) : base(factory)
        => Bridges.Forwards.Player.OnPlayerEquipWeapon += WeaponService_EquipWeapon;

    private void WeaponService_EquipWeapon(nint ptrClient,
        nint                                    ptrController,
        nint                                    ptrPawn,
        nint                                    ptrService,
        nint                                    ptrWeapon,
        long                                    value)
    {
        if (!IsForwardInvokeRequired())
        {
            return;
        }

        var param = new PlayerEquipWeaponForwardParams(true, ptrClient, ptrController, ptrPawn, ptrWeapon, ptrService, value);
        InvokeForward(param);
        param.MarkAsDisposed();
    }
}

internal sealed class PlayerEquipWeaponForwardParams : PlayerWeaponWithServiceFunctionParams, IPlayerEquipWeaponForwardParams
{
    public PlayerEquipWeaponForwardParams(bool postHook,
        nint                                   client,
        nint                                   controller,
        nint                                   pawn,
        nint                                   weapon,
        nint                                   service,
        long                                   equipmentValue) : base(postHook, client, controller, pawn, weapon, service)
        => EquipmentValue = equipmentValue;

    public long EquipmentValue { get; }
}
