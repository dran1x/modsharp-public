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

#include "gamedata.h"
#include "global.h"
#include "vhook/call.h"

#include "cstrike/component/CGameSceneNode.h"

matrix3x4_t CGameSceneNode::EntityToWorldTransform()
{
    matrix3x4_t mat;

    static auto SinCos = [](float rad, float* sine, float* cosine) {
        *sine   = sinf(rad);
        *cosine = cosf(rad);
    };

    static auto DEG2RAD = [](const float deg) {
        return deg * (std::numbers::pi_v<float> / 180.f);
    };

    auto  angles = this->m_angAbsRotation();
    float sr, sp, sy, cr, cp, cy;
    SinCos(DEG2RAD(angles[1]), &sy, &cy);
    SinCos(DEG2RAD(angles[0]), &sp, &cp);
    SinCos(DEG2RAD(angles[2]), &sr, &cr);
    mat[0][0] = cp * cy;
    mat[1][0] = cp * sy;
    mat[2][0] = -sp;

    float crcy = cr * cy;
    float crsy = cr * sy;
    float srcy = sr * cy;
    float srsy = sr * sy;
    mat[0][1]  = sp * srcy - crsy;
    mat[1][1]  = sp * srsy + crcy;
    mat[2][1]  = sr * cp;

    mat[0][2] = (sp * crcy + srsy);
    mat[1][2] = (sp * crsy - srcy);
    mat[2][2] = cr * cp;

    auto pos  = this->m_vecAbsOrigin();
    mat[0][3] = pos.x;
    mat[1][3] = pos.y;
    mat[2][3] = pos.z;

    return mat;
}

[[nodiscard]] CStrikeObject* CGameSceneNode::GetStudioModel()
{
    DeclareVFuncIndex(CGameSceneNode, GetStudioModel, offset);
    return CALL_VIRTUAL(CStrikeObject*, offset, this);
}