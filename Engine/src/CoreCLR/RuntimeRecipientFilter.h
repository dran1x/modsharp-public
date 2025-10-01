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

#ifndef CORECLR_RECIPIPENTFILTER_H
#define CORECLR_RECIPIPENTFILTER_H

#include "definitions.h"

#include <cstddef>
#include <cstdint>

enum CStrikeTeam_t : uint8_t;

enum class RuntimeRecipientFilterType
{
    All     = 0,
    Team    = 1,
    Players = 2,
    Single  = 3,
};

#pragma pack(push, 16)
struct RuntimeRecipientFilter
{
    RuntimeRecipientFilterType Type;
    CStrikeTeam_t              Team;
    NetworkReceiver_t          Receivers;
};
#pragma pack(pop)
static_assert(sizeof(RuntimeRecipientFilter) == 16);

#ifdef PLATFORM_WINDOWS
static_assert(offsetof(RuntimeRecipientFilter, Type) == 0);
static_assert(offsetof(RuntimeRecipientFilter, Team) == 4);
static_assert(offsetof(RuntimeRecipientFilter, Receivers) == 8);
#endif

#endif
