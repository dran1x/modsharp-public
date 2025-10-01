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

using System.Runtime.InteropServices;
using Sharp.Core.Objects;
using Sharp.Shared.Enums;
using Sharp.Shared.Types;
using Sharp.Shared.Units;
using Sharp.Shared.Utilities;

namespace Sharp.Core.Bridges.Forwards;

internal static unsafe class Network
{
    public delegate HookReturnValue<NetworkReceiver> DelegateOnTextMsg(HudPrintChannel channel,
        string                                                                         name,
        NetworkReceiver                                                                nReceivers);

    public delegate HookReturnValue<NetworkReceiver> DelegatePostEventAbstract(ProtobufNetMessageType netMsg,
        NetMessage                                                                                    data,
        NetworkReceiver                                                                               nReceivers);

    public static event DelegateOnTextMsg?         OnTextMsg;
    public static event DelegatePostEventAbstract? OnPostEventAbstract;

    [UnmanagedCallersOnly]
    public static EHookAction OnPostEventAbstractExport(ProtobufNetMessageType netMsg,
        nint                                                                   ptrData,
        NetworkReceiver                                                        receivers,
        bool*                                                                  pChangeReceiver,
        NetworkReceiver*                                                       pNewReceivers)
    {
        if (OnPostEventAbstract is null)
        {
            return EHookAction.Ignored;
        }

        var data   = new NetMessage(ptrData, netMsg);
        var action = OnPostEventAbstract.Invoke(netMsg, data, receivers);
        data.MarkAsDisposed();

        if (action.Action is not EHookAction.ChangeParamReturnDefault)
        {
            return action.Action;
        }

        *pChangeReceiver = true;
        *pNewReceivers   = action.ReturnValue;

        return action.Action;
    }

    [UnmanagedCallersOnly]
    public static EHookAction OnTextMsgExport(HudPrintChannel channel,
        sbyte*                                                pName,
        NetworkReceiver                                       receivers,
        bool*                                                 pChangeReceiver,
        NetworkReceiver*                                      pNewReceivers)
    {
        if (OnTextMsg is null)
        {
            return EHookAction.Ignored;
        }

        var name = Utils.ReadString(pName);

        var action = OnTextMsg.Invoke(channel, name, receivers);

        if (action.Action is not EHookAction.ChangeParamReturnDefault)
        {
            return action.Action;
        }

        *pChangeReceiver = true;
        *pNewReceivers   = action.ReturnValue;

        return action.Action;
    }
}
