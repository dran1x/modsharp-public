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

#include "gamedata.h"
#include "global.h"
#include "vhook/call.h"

#include "cstrike/interface/IGameServer.h"

IGameEventManager2* CSource2Server::GetGameEventManager()
{
    DeclareVFuncIndex(CSource2Server, GetGameEventManager, offset);
    const auto pUnknown = VCall_Manual(offset, void*, this);
    return reinterpret_cast<IGameEventManager2*>(static_cast<char*>(pUnknown) - 8);
}
