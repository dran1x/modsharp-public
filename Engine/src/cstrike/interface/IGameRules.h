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

#ifndef CSTRIKE_GAMERULES__H
#define CSTRIKE_GAMERULES__H

#include <cstdint>

class CCSPlayerController;

struct TeamRewardInfo
{
    int m_nTeam;
    int reason;
};

class IGameRules
{
public:
    bool        PlayerCanHearChat(CCSPlayerController* pReceiverController, CCSPlayerController* pSenderController, bool bTeamOnly);
    void        TerminateRound(float flDelay, uint32_t iReason, bool bypassHook = false, TeamRewardInfo* reward_info = nullptr, uint32_t reward_size = 0);
    static bool IsBypassHook();
};
using CCSGameRules = IGameRules;

#endif
