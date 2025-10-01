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

#ifndef MS_HOOK_NETWORK_H
#define MS_HOOK_NETWORK_H

#include "cstrike/consts.h"

class CEffectData;
class IRecipientFilter;
class CBaseEntity;
class Vector;

extern bool s_bBypassNetMessageHook;

inline void SetNetworkMessageBypassHook(bool state)
{
    s_bBypassNetMessageHook = state;
}

bool AddHookedNetMsgId(uint16_t netMsgId);
void RemoveHookedNetMsgId(uint16_t netMsgId);

void DispatchEffect(const char* pName, CEffectData* pData);
void DispatchEffectFilter(IRecipientFilter* pFilter, float delay, const char* pName, CEffectData* pData);

// const char* pszParticleName, Vector* pOrigin, QAngle* pAngles, CBaseEntity* pEntity, bool bAllowDormantSpawn = false, int32_t nSplitScreenPlayerSlot = -1, IRecipientFilter* pFilter = nullptr, bool bResetAllParticlesOnEntity = false
int32_t DispatchParticleEffectFilter(const char* pszParticleName, Vector* pOrigin, Vector* pAngles, IRecipientFilter* pFilter = nullptr);
int32_t DispatchParticleEffectFilter(const char* pszParticleName, CBaseEntity* pEntity, Vector* pOrigin, Vector* pAngles, bool bResetAllParticlesOnEntity = false, IRecipientFilter* pFilter = nullptr);

// const char* pszParticleName, ParticleAttachment_t iAttachType, CBaseEntity* pEntity, uint8_t iAttachmentIndex, uint32_t /* CUtlStringToken */ iAttachmentName = 0, bool bResetAllParticlesOnEntity = false, int32_t nSplitScreenPlayerSlot = -1, IRecipientFilter* pFilter = nullptr, bool bAllowDormantSpawn = false
int32_t DispatchParticleEffectFilter(const char* pszParticleName, CBaseEntity* pEntity, ParticleAttachment_t iAttachType = PATTACH_ABSORIGIN, IRecipientFilter* pFilter = nullptr, uint8_t iAttachmentIndex = 0, bool bResetAllParticlesOnEntity = false);

#endif