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

#ifndef CSTRIKE_TYPE_MOVEDATA_H
#define CSTRIKE_TYPE_MOVEDATA_H

#include "cstrike/type/CTrace.h"
#include "cstrike/type/CUtlVector.h"
#include "cstrike/type/QAngle.h"
#include "cstrike/type/Vector.h"

#include <cstdint>

#define USE_MV_RE

struct touchlist_t
{
    Vector     deltaVelocity;
    CGameTrace trace;
};

struct SubtickMove
{
    float    when;
    uint64_t button;

    union {
        bool pressed;

        struct
        {
            float analog_forward_delta;
            float analog_left_delta;
            float analog_pitch_delta;
            float analog_yaw_delta;
        } analogMove;
    };
};

struct SubtickAttack
{
    float    when;
    uint64_t button;
    bool     pressed;
};

#pragma pack(push, 1)
class CMoveData
{
public:
    CMoveData() = default;

    uint8_t moveDataFlags; // 0x0
private:
    char pad_1[0x3];

public:
    uint32_t                  m_nPlayerHandle;           // 0x4
    QAngle                    m_vecAbsViewAngles;        // 0x8
    QAngle                    m_vecViewAngles;           // 0x14
    Vector                    m_vecLastMovementImpulses; // 0x20
    float                     m_flForwardMove;           // 0x2c
    float                     m_flSideMove;              // 0x30
    float                     m_flUpMove;                // 0x34
    Vector                    m_vecVelocity;             // 0x38
    Vector                    m_vecAngles;               // 0x44
    CUtlVector<SubtickMove>   m_SubtickMoves;            // 0x50
    CUtlVector<SubtickAttack> m_AttackSubtickMoves;      // 0x68
    bool                      m_bHasSubtickInputs;       // 0x80
private:
    char pad_81[0x7];

public:
    CUtlVector<touchlist_t> m_TouchList;               // 0x88
    Vector                  m_vecCollisionNormal;      // 0xa0
    Vector                  m_vecGroundNormal;         // 0xac
    Vector                  m_vecAbsOrigin;            // 0xb8
    int32_t                 m_nFullMoveStartTick;      // 0xc4
    int32_t                 m_nFullMoveEndTick;        // 0xc8
    float                   m_flFullMoveStartFraction; // 0xcc
    float                   m_flFullMoveEndFraction;   // 0xd0
private:
    char pad_d4[0x4];

public:
    Vector m_outWishVel;           // 0xd8
    Vector m_vecOldAngles;         // 0xe4
    Vector m_vecInputRotated;      // 0xf0
    Vector m_vecAccelPerSecond;    // 0xfc
    float  m_flMaxSpeed;           // 0x108
    float  m_flClientMaxSpeed;     // 0x10c
    float  m_flSubtickFraction;    // 0x110
    bool   m_bInAir;               // 0x114
    bool   m_bGameCodeMovedPlayer; // 0x115
}; // Size: 0x116
#pragma pack(pop)
static_assert(sizeof(CMoveData) == 0x116, "sizeof(CMoveData) != 0x116");

#endif
