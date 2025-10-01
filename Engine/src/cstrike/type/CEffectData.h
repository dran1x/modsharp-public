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

#ifndef CSTRIKE_TYPE_EFFECTDATA_H
#define CSTRIKE_TYPE_EFFECTDATA_H

#include "cstrike/type/QAngle.h"
#include "cstrike/type/Vector.h"
#include "cstrike/cstrike.h"

#include <cstdint>

class CEffectData
{
    [[maybe_unused]] void* m_vfptr; // 0x0
public:
    Vector      m_vOrigin;      // 0x8
    Vector      m_vStart;       // 0x14
    Vector      m_vNormal;      // 0x20
    QAngle      m_vAngles;      // 0x2c
    CBaseHandle m_hEntity;      // 0x38
    CBaseHandle m_hOtherEntity; // 0x3c
    float       m_flScale;      // 0x40
    float       m_flMagnitude;  // 0x44
    float       m_flRadius;     // 0x48
    uint32_t    m_nSurfaceProp; // 0x4c /* CUtlStringToken */
    uint64_t    m_nEffectIndex; // 0x50 /* CWeakHandle<InfoForResourceTypeIParticleSystemDefinition>*/
    uint32_t    m_nDamageType;  // 0x58
    uint8_t     m_nPenetrate;   // 0x5c
private:
    [[maybe_unused]] uint8_t __pad005d[0x1]; // 0x5d
public:
    uint16_t m_nMaterial;        // 0x5e
    uint16_t m_nHitBox;          // 0x60
    uint8_t  m_nColor;           // 0x62
    uint8_t  m_fFlags;           // 0x63
    int32_t  m_nAttachmentIndex; // 0x64 /* AttachmentHandle_t*/
    uint32_t m_nAttachmentName;  // 0x68 /* CUtlStringToken */
    uint16_t m_iEffectName;      // 0x6c /* StringTable 'EffectDispatch' index */
    uint8_t  m_nExplosionType;   // 0x6e
};
static_assert(sizeof(CEffectData) == 0x70);

#ifdef PLATFORM_WINDOWS
static_assert(offsetof(CEffectData, m_nAttachmentName) == 0x68);
#endif

#endif