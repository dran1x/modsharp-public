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

#ifndef MS_ROOT_GAMEFACTORY_H
#define MS_ROOT_GAMEFACTORY_H

#include "global.h"

class GameFactory
{
public:
    explicit GameFactory(const CreateInterface_t factory) :
        m_pFactory(factory) {}

    template <typename T = void*>
    T GetInterface(const char* name) const
    {
        return reinterpret_cast<T>(m_pFactory(name, nullptr));
    }

    void* CreateInterface(const char* name, int* ret) const
    {
        return m_pFactory(name, ret);
    }

private:
    CreateInterface_t m_pFactory;
};

namespace factory
{
extern GameFactory* engine;
extern GameFactory* server;
extern GameFactory* physics;
extern GameFactory* filesystem;
} // namespace factory

#endif
