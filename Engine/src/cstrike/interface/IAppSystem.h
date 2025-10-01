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

#ifndef CSTRIKE_INTERFACE_APP_H
#define CSTRIKE_INTERFACE_APP_H

enum InitReturnVal_t
{
    INIT_FAILED = 0,
    INIT_OK,

    INIT_LAST_VAL,
};

enum AppSystemTier_t
{
    APP_SYSTEM_TIER0 = 0,
    APP_SYSTEM_TIER1,
    APP_SYSTEM_TIER2,
    APP_SYSTEM_TIER3,
    APP_SYSTEM_TIER4,

    APP_SYSTEM_TIER_OTHER,
};

enum BuildType_t
{
    kBuildTypeUnknown,
    kBuildTypeDebug,
    kBuildTypeRelease
};

class IAppSystem
{
public:
    virtual bool Connect(/*CreateInterface*/ void* factory) = 0;
    virtual void Disconnect()                               = 0;

    virtual void* QueryInterface(const char* pInterfaceName) = 0;

    virtual InitReturnVal_t Init()        = 0;
    virtual void            Shutdown()    = 0;
    virtual void            PreShutdown() = 0;

    virtual const /*AppSystemInfo_t*/ void* GetDependencies() = 0;

    virtual AppSystemTier_t GetTier() = 0;

    virtual void Reconnect(/*CreateInterface*/ void* factory, const char* pInterfaceName) = 0;

    virtual bool IsSingleton() = 0;

    virtual BuildType_t GetBuildType() = 0;
};

/* 10 methods */
// 子类从11开始

#endif
