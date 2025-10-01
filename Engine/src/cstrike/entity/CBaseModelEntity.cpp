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

#include "address.h"
#include "logging.h"

#include "cstrike/component/CBodyComponent.h"
#include "cstrike/component/CGameSceneNode.h"
#include "cstrike/entity/CBaseModelEntity.h"

void CBaseModelEntity::SetBodyGroupByName(const char* name, int32_t value)
{
    address::server::CBaseModelEntity_SetBodyGroupByName(this, name, value);
}

void CBaseModelEntity::SetMaterialGroupMask(uint64_t mask)
{
    address::server::CBaseModelEntity_SetMaterialGroupMask(this, mask);
}

int32_t CBaseModelEntity::LookupAttachment(const char* pAttachmentName)
{
    const auto pNode  = m_CBodyComponent()->m_pSceneNode();
    const auto pModel = pNode->GetStudioModel();

#ifdef PLATFORM_WINDOWS
    int32_t iAttachment = 0;
    address::server::StudioModel_LookupAttachment(pModel, &iAttachment, pAttachmentName);
    return iAttachment;
#else
    return address::server::StudioModel_LookupAttachment(pModel, pAttachmentName);
#endif
}

void CBaseModelEntity::GetAttachment(int32_t iAttachment, Vector* absOrigin, Vector* absAngles)
{
    if (iAttachment <= 0)
        return;

    const auto pNode  = m_CBodyComponent()->m_pSceneNode();
    const auto pModel = pNode->GetStudioModel();

    absOrigin->Init();
    absAngles->Init();

    address::server::StudioModel_GetAttachment(pModel, iAttachment, absOrigin, absAngles);
}

int32_t CBaseModelEntity::LookupBone(const char* pBoneName)
{
    return address::server::CBaseModelEntity_LookupBone(this, pBoneName);
}

void CBaseModelEntity::GetBoneTransform(int32_t iBone, matrix3x4_t* transform)
{
    if (iBone < 0)
        return;

    address::server::CBaseModelEntity_GetBoneTransform(this, iBone, transform);
}

void CBaseModelEntity::SetModelScale(float scale)
{
    address::server::CBaseModelEntity_SetModelScale(this, scale);
}

void CBaseModelEntity::SetCollisionBounds(const Vector* mins, const Vector* maxs)
{
    address::server::CBaseModelEntity_SetCollisionBounds(this, mins, maxs);
}