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

#ifndef CSTRIKE_TYPE_USERCMD_H
#define CSTRIKE_TYPE_USERCMD_H

#include <cstdint>

enum class EButtons : uint64_t
{
    IN_ATTACK    = 1 << 0,
    IN_JUMP      = 1 << 1,
    IN_DUCK      = 1 << 2,
    IN_FORWARD   = 1 << 3,
    IN_BACK      = 1 << 4,
    IN_USE       = 1 << 5,
    IN_MOVELEFT  = 1 << 9,
    IN_MOVERIGHT = 1 << 10,
    IN_ATTACK2   = 1 << 11,
    IN_RELOAD    = 1 << 13,
    IN_SCORE     = 1 << 16,
    IN_SPEED     = 1 << 17,
    IN_BULLRUSH  = 1 << 22
};

struct CButtons
{
    EButtons m_nKeyButtons;
    EButtons m_nKeyButtonsChanged;
    EButtons m_nScrollButtons;
};

#endif