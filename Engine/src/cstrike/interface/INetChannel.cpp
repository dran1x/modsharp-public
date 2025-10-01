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

#include "cstrike/interface/INetChannel.h"

#include "gamedata.h"
#include "global.h"

void INetChannel::SendNetMessage(INetworkSerializable* pSerializable, google::protobuf::Message* pMessage, int reliable)
{
    using fn          = void (*)(INetChannel*, INetworkSerializable*, google::protobuf::Message*, int);
    static auto pCall = g_pGameData->GetAddress<fn>("INetChannel::SendNetMessage");
    pCall(this, pSerializable, pMessage, reliable);
}
