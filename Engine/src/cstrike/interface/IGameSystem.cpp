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

#include "cstrike/interface/IGameSystem.h"

#include <cstring>

class CGameSystemFactory
{
    virtual ~CGameSystemFactory() = 0;

public:
    CGameSystemFactory* m_pNext;
    const char*         m_pszName;
    IGameSystem*        m_pInstance;
};

void* FindGameSystemByName(const char* name)
{
    static auto pFactoryList = g_pGameData->GetAddress<CGameSystemFactory*>("GameSystemFactory::m_FactoryList");

    auto list = pFactoryList;

    while (list)
    {
        if (strcmp(list->m_pszName, name) == 0)
        {
            return list->m_pInstance;
        }

        list = list->m_pNext;
    }

    return nullptr;
}
