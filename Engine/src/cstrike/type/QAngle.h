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

#ifndef CSTRIKE_TYPE_QANGLE_H
#define CSTRIKE_TYPE_QANGLE_H

#include <cmath>

class QAngle
{
public:
    // Members
    float x{}, y{}, z{};

    // Construction/destruction
    QAngle(void) = default;
    QAngle(float X, float Y, float Z);

    void Init(float ix = 0.0f, float iy = 0.0f, float iz = 0.0f);

    bool IsValid() const;

    // array access...
    float  operator[](int i) const;
    float& operator[](int i);

    // Base address...
    float*       Base();
    const float* Base() const;

    // equality
    bool operator==(const QAngle& v) const;
    bool operator!=(const QAngle& v) const;

    // arithmetic operations
    QAngle& operator+=(const QAngle& v);
    QAngle& operator-=(const QAngle& v);
    QAngle& operator*=(float s);
    QAngle& operator/=(float s);

    // Get the vector's magnitude.
    float Length() const;
    float LengthSqr() const;

    // negate the QAngle components
    // void	Negate();

    // No assignment operators either...
    QAngle& operator=(const QAngle& src);

    // copy constructors

    // arithmetic operations
    QAngle operator-(void) const;

    QAngle operator+(const QAngle& v) const;
    QAngle operator-(const QAngle& v) const;
    QAngle operator*(float fl) const;
    QAngle operator/(float fl) const;
};

inline QAngle::QAngle(float X, float Y, float Z)
{
    x = X;
    y = Y;
    z = Z;
}

inline void QAngle::Init(float ix, float iy, float iz)
{
    x = ix;
    y = iy;
    z = iz;
}

//-----------------------------------------------------------------------------
// assignment
//-----------------------------------------------------------------------------
inline QAngle& QAngle::operator=(const QAngle& vOther)
{
    x = vOther.x;
    y = vOther.y;
    z = vOther.z;
    return *this;
}

//-----------------------------------------------------------------------------
// Array access
//-----------------------------------------------------------------------------
inline float& QAngle::operator[](int i)
{
    return ((float*)this)[i];
}

inline float QAngle::operator[](int i) const
{
    return ((float*)this)[i];
}

//-----------------------------------------------------------------------------
// Base address...
//-----------------------------------------------------------------------------
inline float* QAngle::Base()
{
    return (float*)this;
}

inline const float* QAngle::Base() const
{
    return (const float*)this;
}

//-----------------------------------------------------------------------------
// IsValid?
//-----------------------------------------------------------------------------
inline bool QAngle::IsValid() const
{
    return std::isfinite(x) && std::isfinite(y) && std::isfinite(z);
}

inline bool QAngle::operator==(const QAngle& src) const
{
    return (src.x == x) && (src.y == y) && (src.z == z);
}

inline bool QAngle::operator!=(const QAngle& src) const
{
    return (src.x != x) || (src.y != y) || (src.z != z);
}

inline QAngle& QAngle::operator+=(const QAngle& v)
{
    x += v.x;
    y += v.y;
    z += v.z;
    return *this;
}

inline QAngle& QAngle::operator-=(const QAngle& v)
{
    x -= v.x;
    y -= v.y;
    z -= v.z;
    return *this;
}

inline QAngle& QAngle::operator*=(float fl)
{
    x *= fl;
    y *= fl;
    z *= fl;
    return *this;
}

inline QAngle& QAngle::operator/=(float fl)
{
    float oofl = 1.0f / fl;
    x *= oofl;
    y *= oofl;
    z *= oofl;
    return *this;
}

//-----------------------------------------------------------------------------
// length
//-----------------------------------------------------------------------------
inline float QAngle::Length() const
{
    return std::sqrt(LengthSqr());
}

inline float QAngle::LengthSqr() const
{
    return x * x + y * y + z * z;
}

inline QAngle QAngle::operator-(void) const
{
    QAngle ret(-x, -y, -z);
    return ret;
}

inline QAngle QAngle::operator+(const QAngle& v) const
{
    QAngle res;
    res.x = x + v.x;
    res.y = y + v.y;
    res.z = z + v.z;
    return res;
}

inline QAngle QAngle::operator-(const QAngle& v) const
{
    QAngle res;
    res.x = x - v.x;
    res.y = y - v.y;
    res.z = z - v.z;
    return res;
}

inline QAngle QAngle::operator*(float fl) const
{
    QAngle res;
    res.x = x * fl;
    res.y = y * fl;
    res.z = z * fl;
    return res;
}

inline QAngle QAngle::operator/(float fl) const
{
    QAngle res;
    res.x = x / fl;
    res.y = y / fl;
    res.z = z / fl;
    return res;
}

inline QAngle operator*(float fl, const QAngle& v)
{
    QAngle ret(v * fl);
    return ret;
}

#endif