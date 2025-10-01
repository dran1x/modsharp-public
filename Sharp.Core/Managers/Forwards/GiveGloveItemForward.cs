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

internal class GiveGloveItemForward : ForwardType<GiveGloveItemForwardForwardParams, GiveGloveItemForward>
{
    public GiveGloveItemForward(ILoggerFactory factory) : base(factory)
        => Bridges.Forwards.Extern.GiveNamedItem.OnGiveGloveItemPost += GiveNamedItem_OnGiveGloveItemPost;

    private void GiveNamedItem_OnGiveGloveItemPost(nint ptrClient, nint ptrController, nint ptrPawn)
    {
        if (!IsForwardInvokeRequired())
        {
            return;
        }

        var param = new GiveGloveItemForwardForwardParams(true, ptrClient, ptrController, ptrPawn);
        InvokeForward(param);

        param.MarkAsDisposed();
    }
}

internal sealed class GiveGloveItemForwardForwardParams : PlayerPawnFunctionParams, IGiveGloveItemForwardParams
{
    public GiveGloveItemForwardForwardParams(bool postHook, nint client, nint controller, nint pawn) : base(postHook,
        client,
        controller,
        pawn)
    {
    }
}
