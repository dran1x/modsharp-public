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

#ifndef MS_ROOT_NETMESSAGE_H
#define MS_ROOT_NETMESSAGE_H

#include "definitions.h"

#include <cstdint>

enum HudPrint_t : uint32_t;
class IRecipientFilter;

void UTIL_SayText2Filter(IRecipientFilter* filter, bool chat, EntityIndex_t entity, const char* message, const char* param1 = nullptr, const char* param2 = nullptr, const char* param3 = nullptr, const char* param4 = nullptr);
void UTIL_SayText2Filter(int bitWide, NetworkReceiver_t pClients, bool chat, EntityIndex_t entity, const char* message, const char* param1 = nullptr, const char* param2 = nullptr, const char* param3 = nullptr, const char* param4 = nullptr);

void UTIL_TextMsgFilter(IRecipientFilter* filter, HudPrint_t hudPrint, const char* message, const char* param1 = nullptr, const char* param2 = nullptr, const char* param3 = nullptr, const char* param4 = nullptr);
void UTIL_TextMsgFilter(int bitWide, NetworkReceiver_t pClients, HudPrint_t hudPrint, const char* message, const char* param1 = nullptr, const char* param2 = nullptr, const char* param3 = nullptr, const char* param4 = nullptr);

void UTIL_PrintFilter(IRecipientFilter* filter, const char* message);
void UTIL_PrintFilter(int bitWide, NetworkReceiver_t pClients, const char* message);

void UTIL_ReplicateConVarFilter(IRecipientFilter* filter, const char* pName, const char* pValue);

inline void UTIL_TextMsgSingle(PlayerSlot_t slot, HudPrint_t hudPrint, const char* message, const char* param1 = nullptr, const char* param2 = nullptr, const char* param3 = nullptr, const char* param4 = nullptr)
{
    const int32_t           bitWide = slot + 1;
    const NetworkReceiver_t clients = BASE_RECEIVER_MAGIC << slot;
    UTIL_TextMsgFilter(bitWide, clients, hudPrint, message, param1, param2, param3, param4);
}

inline void UTIL_PrintSingle(PlayerSlot_t slot, const char* message)
{
    const int32_t           bitWide = slot + 1;
    const NetworkReceiver_t clients = BASE_RECEIVER_MAGIC << slot;
    UTIL_PrintFilter(bitWide, clients, message);
}

#endif
