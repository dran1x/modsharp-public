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

#ifndef CSTRIKE_INTERFACE_NETWORKSTRINGTABLE_H
#define CSTRIKE_INTERFACE_NETWORKSTRINGTABLE_H

#include "cstrike/interface/IAppSystem.h"

#include <cstdint>

#define INTERFACEVERSION_SERVERSTRINGTABLE "Source2EngineToServerStringTable001"

class INetworkStringTable;
class INetworkStringTableContainer;
class CNetworkStringTableItem;

class INetworkStringTableContainer : public IAppSystem
{
    virtual ~INetworkStringTableContainer() = 0;

public:
    // table creation/destruction
    virtual INetworkStringTable* CreateStringTable(const char* tableName, int maxentries, int userdatafixedsize = 0, int userdatanetworkbits = 0, int flags = 0) = 0;
    virtual void                 RemoveAllTables()                                                                                                               = 0;

    // table info
    virtual INetworkStringTable* FindTable(const char* tableName) const = 0;
    virtual INetworkStringTable* GetTable(int32_t stringTable) const    = 0;
    virtual int                  GetNumTables() const                   = 0;

    // ??
    virtual void SetAllowClientSideAddString(INetworkStringTable* table, bool bAllowClientSideAddString) = 0;
    virtual void CreateDictionary(const char* pchMapName)                                                = 0;
};

class INetworkStringDict;

class INetworkStringTable
{
    virtual ~INetworkStringTable() = 0;

public:
    virtual const char* GetTableName() const   = 0;
    virtual int32_t     GetTableId() const     = 0;
    virtual int64_t     GetStringCount() const = 0;

private:
    virtual void Method_004() = 0;
    virtual void Method_005() = 0;
    virtual void Method_006() = 0;

public:
    virtual int32_t     AddString(bool bIsServer, const char* value, const void* unk1 = nullptr)        = 0;
    virtual const char* GetString(int32_t index) const                                                  = 0;
    virtual void        Method01() const                                                                = 0;
    virtual void*       Method02() const                                                                = 0; 
    virtual int32_t     FindStringIndex(const char* value) const                                        = 0;
};

using CNetworkStringTableContainer = INetworkStringTableContainer;
using CNetworkStringTable          = INetworkStringTable;

#endif