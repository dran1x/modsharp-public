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

#ifndef CSTRIKE_TYPE_QUATERNION_H
#define CSTRIKE_TYPE_QUATERNION_H

#include "platform.h"

#include "cstrike/type/Vector.h"

class Quaternion // same data-layout as engine's vec4_t,
{                //		which is a float[4]
public:
    Quaternion() = default;
    float x{}, y{}, z{}, w{};

    Quaternion(float ix, float iy, float iz, float iw) :
        x(ix), y(iy), z(iz), w(iw) {}

    inline void Init(float ix = 0.0f, float iy = 0.0f, float iz = 0.0f, float iw = 0.0f)
    {
        x = ix;
        y = iy;
        z = iz;
        w = iw;
    }

    inline void Init(const Vector& vImaginaryPart, float flRealPart)
    {
        x = vImaginaryPart.x;
        y = vImaginaryPart.y;
        z = vImaginaryPart.z;
        w = flRealPart;
    }

    bool IsValid() const;

    bool operator==(const Quaternion& src) const;
    bool operator!=(const Quaternion& src) const;
};

inline bool Quaternion::IsValid() const
{
    return std::isfinite(x) && std::isfinite(y) && std::isfinite(z) && std::isfinite(w);
}

inline bool Quaternion::operator==(const Quaternion& src) const
{
    return (x == src.x) && (y == src.y) && (z == src.z) && (w == src.w);
}

inline bool Quaternion::operator!=(const Quaternion& src) const
{
    return !operator==(src);
}

class MS_ALIGN16 QuaternionAligned : public Quaternion
{
public:
    inline QuaternionAligned(void) = default;
    inline QuaternionAligned(float X, float Y, float Z, float W)
    {
        Init(X, Y, Z, W);
    }

    operator Quaternion*() { return this; }
    operator const Quaternion*() const { return this; }

    explicit QuaternionAligned(const Quaternion& vOther)
    {
        Init(vOther.x, vOther.y, vOther.z, vOther.w);
    }

    QuaternionAligned& operator=(const Quaternion& vOther)
    {
        Init(vOther.x, vOther.y, vOther.z, vOther.w);
        return *this;
    }

    QuaternionAligned& operator=(const QuaternionAligned& vOther)
    {
        Init(vOther.x, vOther.y, vOther.z, vOther.w);
        return *this;
    }

} MS_ALIGN16_POST;

#endif