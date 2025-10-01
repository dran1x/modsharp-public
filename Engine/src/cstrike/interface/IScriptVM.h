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

#ifndef CSTRIKE_INTERFACE_SCRIPTVM_H
#define CSTRIKE_INTERFACE_SCRIPTVM_H

class CCSGOVScriptGameSystem;

struct ScriptVariant_t;

class IScriptVM
{
    // virtual ~IScriptVM() = 0;

    // virtual bool Init()     = 0;
    // virtual void Shutdown() = 0;

    virtual void Method_00() = 0;
    virtual void Method_01() = 0;
    virtual void Method_02() = 0;
    virtual void Method_03() = 0;
    virtual void Method_04() = 0;
    virtual void Method_05() = 0;
    virtual void Method_06() = 0;
    virtual void Method_07() = 0;
    virtual void Method_08() = 0;
    virtual void Method_09() = 0;
    virtual void Method_10() = 0;

    virtual void Method_21() = 0;
    virtual void Method_22() = 0;
    virtual void Method_23() = 0;
    virtual void Method_24() = 0;
    virtual void Method_25() = 0;
    virtual void Method_26() = 0;
    virtual void Method_27() = 0;
    virtual void Method_28() = 0;
    virtual void Method_29() = 0;
    virtual void Method_30() = 0;

    virtual void Method_31() = 0;

public:
    virtual bool SetValueString(void* pScope, const char* pKey, const char* pValue)             = 0;
    virtual bool SetValueVariant(void* pScore, const char* pKey, const ScriptVariant_t* pValue) = 0;
};

#endif
