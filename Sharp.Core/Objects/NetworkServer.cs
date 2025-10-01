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

using System.Collections.Generic;
using Sharp.Core.CStrike;
using Sharp.Core.Helpers;
using Sharp.Shared.Enums;
using Sharp.Shared.Objects;
using Sharp.Shared.Units;
using Sharp.Shared.Utilities;

namespace Sharp.Core.Objects;

internal partial class NetworkServer : NativeObject, INetworkServer
{
    public int GetClientCount()
    {
        CheckDisposed();

        return _this.GetInt32(CUtlVectorClientOffset);
    }

    public unsafe IGameClient? GetGameClient(PlayerSlot slot)
    {
        CheckDisposed();

        var count = _this.GetInt32(CUtlVectorClientOffset);

        if (slot > count)
        {
            return null;
        }

        var pVector = *(nint*) nint.Add(_this,   CUtlVectorClientOffset + 8).ToPointer();
        var ptr     = *(nint*) nint.Add(pVector, slot.AsPrimitive() * 8).ToPointer();

        return GameClient.Create(ptr);
    }

    public unsafe IReadOnlyCollection<IGameClient> GetGameClients()
    {
        CheckDisposed();

        var count = _this.GetInt32(CUtlVectorClientOffset);
        var list  = new List<IGameClient>(count);

        var pVector = *(nint*) nint.Add(_this, CUtlVectorClientOffset + 8).ToPointer();

        for (var index = 0; index < count; index++)
        {
            var actual = *(nint*) nint.Add(pVector, index * 8).ToPointer();
            var client = GameClient.Create(actual);

            if (client is null || client.SignOnState < SignOnState.Connected)
            {
                continue;
            }

            list.Add(client);
        }

        return list.AsReadOnly();
    }

    public unsafe int GetGameClientCount(bool fullyInGame = false)
    {
        CheckDisposed();

        var count = 0;
        var size  = _this.GetInt32(CUtlVectorClientOffset);
        var req   = fullyInGame ? SignOnState.Full : SignOnState.Connected;

        var pVector = *(nint*) nint.Add(_this, CUtlVectorClientOffset + 8).ToPointer();

        for (var index = 0; index < size; index++)
        {
            var actual = *(nint*) nint.Add(pVector, index * 8).ToPointer();

            if (actual == nint.Zero)
            {
                continue;
            }

            var signOnState = (SignOnState) actual.GetInt32(CoreGameData.GameClient.SignonState);

            if (signOnState < req)
            {
                continue;
            }

            count++;
        }

        return count;
    }

    private static int CUtlVectorClientOffset => CoreGameData.CNetworkGameServer.VecClients;
}
