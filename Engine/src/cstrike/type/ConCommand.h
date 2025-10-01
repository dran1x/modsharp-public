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

#ifndef CSTRIKE_TYPE_CONCOMMAND_H
#define CSTRIKE_TYPE_CONCOMMAND_H

#include "cstrike/type/CCommand.h"

/* Called when a ConCommand needs to execute */
using FnCommandCallback_t          = void (*)(const CCommandContext& context, const CCommand& command);
using FnCommandCallbackNoContext_t = void (*)(const CCommand& command);
using FnCommandCallbackVoid_t      = void (*)();

/* Returns 0 to COMMAND_COMPLETION_MAXITEMS worth of completion strings */
using FnCommandCompletionCallback = int (*)(const char* partial, /*CUtlVector<CUtlString>&*/ void* commands);

class ICommandCallback
{
public:
    virtual void CommandCallback(const CCommandContext& context, const CCommand& command) = 0;
};

class ICommandCompletionCallback
{
public:
    virtual int CommandCompletionCallback(const char* pPartial, /*CUtlVector<CUtlString>&*/ void* commands) = 0;
};

class ConCommandHandle
{
public:
    ConCommandHandle(uint16_t index = 0xFFFF, uint32_t handle = 0) :
        m_commandIndex(index),
        m_handleIndex(handle)
    {}

    bool IsValid() const { return m_commandIndex != 0xFFFF; }
    void Invalidate()
    {
        m_commandIndex = 0xFFFF;
        m_handleIndex  = 0x0;
    }

    uint16_t GetConCommandIndex() const { return m_commandIndex; }
    uint32_t GetIndex() const { return m_handleIndex; }

private:
    uint16_t m_commandIndex;
    uint32_t m_handleIndex;
};

class ConCommandRefAbstract
{
public:
    ConCommandHandle handle;
};

class ConCommandBase
{
    friend class CCvar;
    friend class ConCommand;

protected:
    ConCommandBase();

public:
    ~ConCommandBase();

    bool        IsFlagSet(int64_t flag) const { return !!(flag & m_nFlags); }
    void        AddFlags(int64_t flags) { m_nFlags |= flags; }
    void        RemoveFlags(int64_t flags) { m_nFlags &= ~flags; }
    int64_t     GetFlags() const { return m_nFlags; }
    const char* GetName() const { return m_pszName; }
    const char* GetHelpText() const { return m_pszHelpString; }

private:
    // Static data
    const char* m_pszName;
    const char* m_pszHelpString;

    // ConVar flags
    int64_t m_nFlags;
};

class ConCommand : public ConCommandBase
{
    friend class CCvar;
    friend class ConCommandHandle;

public:
    using BaseClass = ConCommandBase;

    ~ConCommand();

    ConCommandRefAbstract* GetRef() const
    {
        return m_pReference;
    }

    void SetHandle(ConCommandHandle handle) const
    {
        m_pReference->handle = handle;
    }

private:
    // Call this function when executing the command
    class CallbackInfo_t
    {
    public:
        union {
            FnCommandCallback_t          m_fnCommandCallback;
            FnCommandCallbackVoid_t      m_fnVoidCommandCallback;
            FnCommandCallbackNoContext_t m_fnContextlessCommandCallback;
            ICommandCallback*            m_pCommandCallback;
        };

        bool m_bUsingCommandCallbackInterface : 1;
        bool m_bHasVoidCommandCallback : 1;
        bool m_bHasContextlessCommandCallback : 1;
    };

    CallbackInfo_t m_Callback;

    // NOTE: To maintain backward compat, we have to be very careful:
    // All public virtual methods must appear in the same order always
    // since engine code will be calling into this code, which *does not match*
    // in the mod code; it's using slightly different, but compatible versions
    // of this class. Also: Be very careful about adding new fields to this class.
    // Those fields will not exist in the version of this class that is instanced
    // in mod code.

    union {
        FnCommandCompletionCallback m_fnCompletionCallback;
        ICommandCompletionCallback* m_pCommandCompletionCallback;
    };

    bool m_bHasCompletionCallback : 1;
    bool m_bUsingCommandCompletionInterface : 1;

    ConCommandRefAbstract* m_pReference;
};

#endif