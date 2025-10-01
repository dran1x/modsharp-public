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

#ifndef CSTRIKE_INTERFACE_ICOMMANDLINE_H
#define CSTRIKE_INTERFACE_ICOMMANDLINE_H

#include "cstrike/type/CHashKey.h"

class ICommandLine
{
public:
    virtual void CreateCmdLine(const char* commandline)               = 0;
    virtual void CreateCmdLine(int argc, char** argv)                 = 0;
    virtual void CreateCmdLinePrependAppName(const char* commandline) = 0;

    // Check whether a particular parameter exists
    virtual const char* CheckParam(CUtlStringToken param, const char** ppszValue = nullptr) const = 0;
    virtual bool        HasParam(CUtlStringToken param) const                                     = 0;

    // Gets at particular parameters
    virtual int         ParamCount() const                     = 0;
    virtual int         FindParam(CUtlStringToken param) const = 0; // Returns 0 if not found.
    virtual const char* GetParam(int nIndex) const             = 0;

    // Returns the argument after the one specified, or the default if not found
    virtual const char* ParamValue(CUtlStringToken param, const char* pDefaultVal = nullptr) const = 0;
    virtual int         ParamValue(CUtlStringToken param, int nDefaultVal) const                   = 0;
    virtual float       ParamValue(CUtlStringToken param, float flDefaultVal) const                = 0;
    virtual bool        ParamValue(CUtlStringToken param, const char* pDefaultVal, void* bufOut)   = 0;

    virtual const char** GetParams() const                                         = 0;
    virtual const char*  GetCmdLine() const                                        = 0;
    virtual void         AppendParam(CUtlStringToken param, const char* pszValues) = 0;

    // Returns true if there's atleast one parm available
    virtual bool HasParams() const = 0;

    virtual const char* GetUnkString() = 0;
};

#endif