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

#ifndef CSTRIKE_TYPE_TRANSFORM_H
#define CSTRIKE_TYPE_TRANSFORM_H

#include "cstrike/type/Quaternion.h"
#include "cstrike/type/Vector.h"

class MS_ALIGN16 CTransform
{
public:
    CTransform() {}
    CTransform(const Vector& v, const Quaternion& q) :
        m_vPosition(v), m_orientation(q) {}

    bool IsValid() const
    {
        return m_vPosition.IsValid() && m_orientation.IsValid();
    }

    bool operator==(const CTransform& v) const;
    bool operator!=(const CTransform& v) const;

    void SetToIdentity();

public:
    VectorAligned     m_vPosition;
    QuaternionAligned m_orientation;

} MS_ALIGN16_POST;
static_assert(sizeof(CTransform) == 32, "CTransform is the wrong size!");

inline void CTransform::SetToIdentity()
{
    m_vPosition   = Vector(0, 0, 0);
    m_vPosition.w = 1.0f;
    m_orientation = QuaternionAligned(0, 0, 0, 1);
}

inline bool CTransform::operator==(const CTransform& t) const
{
    return t.m_vPosition == m_vPosition && t.m_orientation == m_orientation;
}

inline bool CTransform::operator!=(const CTransform& t) const
{
    return t.m_vPosition != m_vPosition || t.m_orientation != m_orientation;
}

#endif // TRANSFORM_H