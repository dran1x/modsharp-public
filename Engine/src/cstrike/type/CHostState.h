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

#ifndef CSTRIKE_TYPE_HOSTSTATE_H
#define CSTRIKE_TYPE_HOSTSTATE_H

#include "cstrike/type/CUtlString.h"

class KeyValues;
enum HostStateRequestType_t
{
    HSR_IDLE = 1,
    HSR_GAME,
    HSR_SOURCETV_RELAY,
    HSR_QUIT
};

enum HostStateRequestMode_t
{
    HM_LEVEL_LOAD_SERVER = 1,
    HM_CONNECT,
    HM_CHANGE_LEVEL,
    HM_LEVEL_LOAD_LISTEN,
    HM_LOAD_SAVE,
    HM_PLAY_DEMO,
    HM_SOURCETV_RELAY,
    HM_ADDON_DOWNLOAD
};

struct CHostStateRequest
{
    HostStateRequestType_t m_iType;
    CUtlString             m_LoopModeType;
    CUtlString             m_Desc;
    bool                   m_bActive;
    unsigned int           m_ID;
    HostStateRequestMode_t m_iMode;
    CUtlString             m_LevelName;
    bool                   m_bChangeLevel;
    CUtlString             m_SaveGame;
    CUtlString             m_Address;
    CUtlString             m_DemoFile;
    bool                   m_bLoadMap;
    CUtlString             m_Addons;
    KeyValues*             m_pKV;
};

#endif