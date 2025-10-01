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

#ifndef CSTRIKE_INTERFACE_NETWORKSERVERSERVICE_H
#define CSTRIKE_INTERFACE_NETWORKSERVERSERVICE_H

#include "cstrike/interface/IAppSystem.h"

#include <cstdint>

struct EngineLoopState_t;
struct EventMapRegistrationType_t;
class CNetworkGameServer;
class GameSessionConfiguration_t;
class ISource2WorldSession;
class IGameSpawnGroupMgr;
class ILoopModePrerequisiteRegistry;
class CRC32_t;

class IEngineService : public IAppSystem // 10
{
public:
    virtual void*       GetServiceDependencies()                                                                                  = 0;
    virtual const char* GetName() const                                                                                           = 0;
    virtual bool        ShouldActivate(const char*)                                                                               = 0;
    virtual void        OnLoopActivate(const EngineLoopState_t& loopState, /*CEventDispatcher<CEventIDManager_Default>*/ void*)   = 0;
    virtual void        OnLoopDeactivate(const EngineLoopState_t& loopState, /*CEventDispatcher<CEventIDManager_Default>*/ void*) = 0;
    virtual bool        IsActive() const                                                                                          = 0;
    virtual void        SetActive(bool)                                                                                           = 0;
    virtual void        SetName(const char* pszName)                                                                              = 0;
    virtual void        RegisterEventMap(/*CEventDispatcher<CEventIDManager_Default>*/ void*, EventMapRegistrationType_t) = 0;
    virtual uint16_t    GetServiceIndex()                                                                                         = 0;
    virtual void        SetServiceIndex(uint16_t index)                                                                           = 0;
};

class INetworkServerService : public IEngineService // 21
{
public:
    virtual ~INetworkServerService() {}
    virtual CNetworkGameServer* GetIGameServer()                                                                                             = 0; // 23
    virtual bool                IsActiveInGame() const                                                                                       = 0;
    virtual bool                IsMultiplayer() const                                                                                        = 0;
    virtual void                StartupServer(const GameSessionConfiguration_t& config, ISource2WorldSession* pWorldSession, const char*)    = 0; // 26
    virtual void                SetGameSpawnGroupMgr(IGameSpawnGroupMgr* pMgr)                                                               = 0;
    virtual void                AddServerPrerequisites(const GameSessionConfiguration_t&, const char*, ILoopModePrerequisiteRegistry*, bool) = 0;
    virtual void                SetServerSocket(int)                                                                                         = 0;
    virtual bool                IsServerRunning() const                                                                                      = 0;
    virtual void                DisconnectGameNow(int) /*ENetworkDisconnectionReason*/                                                       = 0;
    virtual void                PrintSpawnGroupStatus() const                                                                                = 0;
    virtual void                SetFinalSimulationTickThisFrame(int)                                                                         = 0;
    virtual void*               GetGameServer()                                                                                              = 0;
    virtual int                 GetTickInterval() const                                                                                      = 0;
    virtual void                ProcessSocket()                                                                                              = 0;
    virtual int                 GetServerNetworkAddress()                                                                                    = 0;
    virtual bool                GameLoadFailed() const                                                                                       = 0;
    virtual void                SetGameLoadFailed(bool bFailed)                                                                              = 0;
    virtual void                SetGameLoadStarted()                                                                                         = 0;
    virtual void                StartChangeLevel()                                                                                           = 0;
    virtual void                PreserveSteamID()                                                                                            = 0;
    virtual CRC32_t             GetServerSerializersCRC()                                                                                    = 0;
    virtual void*               GetServerSerializersMsg()                                                                                    = 0;
};

#endif