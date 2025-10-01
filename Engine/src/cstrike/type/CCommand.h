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

#ifndef CSTRIKE_TYPE_CCOMMAND_H
#define CSTRIKE_TYPE_CCOMMAND_H

#include "cstrike/type/CUtlVector.h"

#include <cstdint>

enum CommandTarget_t
{
    CT_NO_TARGET                = -1,
    CT_FIRST_SPLITSCREEN_CLIENT = 0,
    CT_LAST_SPLITSCREEN_CLIENT  = 3,
};

class CCommandContext
{
public:
    CCommandContext(CommandTarget_t nTarget, int nSlot) :
        m_nTarget(nTarget), m_nPlayerSlot(nSlot)
    {
    }

    CommandTarget_t GetTarget() const
    {
        return m_nTarget;
    }

    int GetPlayerSlot() const
    {
        return m_nPlayerSlot;
    }

private:
    CommandTarget_t m_nTarget;
    int             m_nPlayerSlot;
};

class CCommand
{
public:
    CCommand();
    [[nodiscard]] bool Tokenize(const char* pCommand, void* pBreakSet = nullptr);

    [[nodiscard]] int ArgC() const noexcept
    {
        return m_Args.Count();
    }
    [[nodiscard]] const char* ArgS() const noexcept
    {
        return m_nArgv0Size ? (m_ArgSBuffer.Base() + m_nArgv0Size) : "";
    }
    [[nodiscard]] const char* GetCommandString() const noexcept
    {
        return ArgC() ? m_ArgSBuffer.Base() : "";
    }
    [[nodiscard]] const char* operator[](int nIndex) const noexcept
    {
        return Arg(nIndex);
    }
    [[nodiscard]] const char* Arg(int nIndex) const noexcept
    {
        if (nIndex < 0 || nIndex >= ArgC())
            return "";
        return m_Args[nIndex];
    }

private:
    static constexpr int32_t COMMAND_MAX_ARGC   = 64;
    static constexpr int32_t COMMAND_MAX_LENGTH = 512;

    int32_t                                           m_nArgv0Size;
    CUtlVectorFixedGrowable<char, COMMAND_MAX_LENGTH> m_ArgSBuffer;
    CUtlVectorFixedGrowable<char, COMMAND_MAX_LENGTH> m_ArgvBuffer;
    CUtlVectorFixedGrowable<char*, COMMAND_MAX_ARGC>  m_Args;
    int8_t                                            pad[24];
};

#endif
