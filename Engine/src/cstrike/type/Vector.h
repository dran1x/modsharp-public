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

#ifndef CSTRIKE_TYPE_VECTOR_H
#define CSTRIKE_TYPE_VECTOR_H

#include "platform.h"

#include <cfloat>
#include <cmath>

class Vector
{
public:
    // Members
    float x, y, z;

    // Construction/destruction:
    Vector(void) = default;
    Vector(float X, float Y, float Z);

    // Initialization
    void Init(float ix = 0.0f, float iy = 0.0f, float iz = 0.0f);
    // TODO (Ilya): Should there be an init that takes a single float for consistency?

    // Got any nasty NAN's?
    bool IsValid() const;

    // array access...
    float  operator[](int i) const;
    float& operator[](int i);

    // Base address...
    float*       Base();
    const float* Base() const;

    // equality
    bool operator==(const Vector& v) const;
    bool operator!=(const Vector& v) const;

    // arithmetic operations
    Vector& operator+=(const Vector& v);
    Vector& operator-=(const Vector& v);
    Vector& operator*=(const Vector& v);
    Vector& operator*=(float s);
    Vector& operator/=(const Vector& v);
    Vector& operator/=(float s);
    Vector& operator+=(float fl); ///< broadcast add
    Vector& operator-=(float fl); ///< broadcast sub

    // Get the vector's magnitude.
    inline float Length() const;

    // Get the vector's magnitude squared.
    float LengthSqr(void) const
    {
        return (x * x + y * y + z * z);
    }

    // return true if this vector is (0,0,0) within tolerance
    bool IsZero(float tolerance = 0.01f) const
    {
        return (x > -tolerance && x < tolerance && y > -tolerance && y < tolerance && z > -tolerance && z < tolerance);
    }

    // return true if this vector is exactly (0,0,0) -- only fast if vector is coming from memory, not registers
    bool IsZeroFast() const
    {
        return (*reinterpret_cast<const int*>(&x) == 0 && *reinterpret_cast<const int*>(&y) == 0 && *reinterpret_cast<const int*>(&z) == 0);
    }

    float  NormalizeInPlace();
    Vector Normalized() const;

    // Get the distance from this vector to the other one.
    float DistTo(const Vector& vOther) const;

    // Get the distance from this vector to the other one squared.
    // NJS: note, VC wasn't inlining it correctly in several deeply nested inlines due to being an 'out of line' inline.
    // may be able to tidy this up after switching to VC7
    float DistToSqr(const Vector& vOther) const
    {
        Vector delta;

        delta.x = x - vOther.x;
        delta.y = y - vOther.y;
        delta.z = z - vOther.z;

        return delta.LengthSqr();
    }

    // Multiply, add, and assign to this (ie: *this = a + b * scalar). This
    // is about 12% faster than the actual vector equation (because it's done per-component
    // rather than per-vector).
    void MulAdd(const Vector& a, const Vector& b, float scalar);

    // Dot product.
    float Dot(const Vector& vOther) const;

    // assignment
    Vector& operator=(const Vector& vOther);

    // 2d
    float Length2D(void) const;
    float Length2DSqr(void) const;

#ifndef VECTOR_NO_SLOW_OPERATIONS
    // copy constructors
    //	Vector(const Vector &vOther);

    // arithmetic operations
    Vector operator-(void) const;

    Vector operator+(const Vector& v) const;
    Vector operator-(const Vector& v) const;
    Vector operator*(const Vector& v) const;
    Vector operator/(const Vector& v) const;
    Vector operator*(float fl) const;
    Vector operator/(float fl) const;

    // Cross product between two vectors.
    Vector Cross(const Vector& vOther) const;

#else

private:
    // No copy constructors allowed if we're in optimal mode
    Vector(const Vector& vOther);
#endif
};

inline float VectorNormalize(Vector& vec)
{
    float radius = sqrtf(vec.x * vec.x + vec.y * vec.y + vec.z * vec.z);

    // FLT_EPSILON is added to the radius to eliminate the possibility of divide by zero.
    float iradius = 1.f / (radius + FLT_EPSILON);

    vec.x *= iradius;
    vec.y *= iradius;
    vec.z *= iradius;

    return radius;
}
inline Vector::Vector(float X, float Y, float Z)
{
    x = X;
    y = Y;
    z = Z;
}

inline void Vector::Init(float ix, float iy, float iz)
{
    x = ix;
    y = iy;
    z = iz;
}

inline bool Vector::IsValid() const
{
    return std::isfinite(x) && std::isfinite(y) && std::isfinite(z);
}

inline bool Vector::operator==(const Vector& src) const
{
    return (src.x == x) && (src.y == y) && (src.z == z);
}

inline bool Vector::operator!=(const Vector& src) const
{
    return (src.x != x) || (src.y != y) || (src.z != z);
}

inline Vector& Vector::operator+=(const Vector& v)
{
    x += v.x;
    y += v.y;
    z += v.z;
    return *this;
}

inline Vector& Vector::operator-=(const Vector& v)
{
    x -= v.x;
    y -= v.y;
    z -= v.z;
    return *this;
}

inline Vector& Vector::operator*=(float fl)
{
    x *= fl;
    y *= fl;
    z *= fl;
    return *this;
}

inline Vector& Vector::operator*=(const Vector& v)
{
    x *= v.x;
    y *= v.y;
    z *= v.z;
    return *this;
}

// this ought to be an opcode.
inline Vector& Vector::operator+=(float fl)
{
    x += fl;
    y += fl;
    z += fl;
    return *this;
}

inline Vector& Vector::operator-=(float fl)
{
    x -= fl;
    y -= fl;
    z -= fl;
    return *this;
}

inline float Vector::Length() const
{
    return std::sqrt(LengthSqr());
}

inline float Vector::Dot(const Vector& b) const
{
    return x * b.x + y * b.y + z * b.z;
}

inline Vector& Vector::operator=(const Vector& vOther) = default;

inline float Vector::Length2D() const
{
    return std::sqrt(Length2DSqr());
}

inline float Vector::Length2DSqr() const
{
    return (x * x + y * y);
}

inline Vector& Vector::operator/=(float fl)
{
    float oofl = 1.0f / fl;
    x *= oofl;
    y *= oofl;
    z *= oofl;
    return *this;
}

inline Vector& Vector::operator/=(const Vector& v)
{
    x /= v.x;
    y /= v.y;
    z /= v.z;
    return *this;
}

inline Vector Vector::operator-(void) const
{
    return Vector(-x, -y, -z);
}

inline Vector Vector::operator+(const Vector& v) const
{
    Vector res;

    res.x = x + v.x;
    res.y = y + v.y;
    res.z = z + v.z;

    return res;
}

inline Vector Vector::operator-(const Vector& v) const
{
    Vector res;

    res.x = x - v.x;
    res.y = y - v.y;
    res.z = z - v.z;
    return res;
}

inline Vector Vector::operator*(float fl) const
{
    Vector res;

    res.x = x * fl;
    res.y = y * fl;
    res.z = z * fl;
    return res;
}

inline Vector Vector::operator*(const Vector& v) const
{
    Vector res;

    res.x = x * v.x;
    res.y = y * v.y;
    res.z = z * v.z;
    return res;
}

inline Vector Vector::operator/(float fl) const
{
    Vector res;

    res.x = x / fl;
    res.y = y / fl;
    res.z = z / fl;
    return res;
}

inline Vector Vector::Cross(const Vector& b) const
{
    return {y * b.z - z * b.y, z * b.x - x * b.z, x * b.y - y * b.x};
}

inline Vector Vector::operator/(const Vector& v) const
{
    Vector res;

    res.x = x / v.x;
    res.y = y / v.y;
    res.z = z / v.z;
    return res;
}

inline Vector operator*(float fl, const Vector& v)
{
    return v * fl;
}

//-----------------------------------------------------------------------------
// Array access
//-----------------------------------------------------------------------------
inline float& Vector::operator[](int i)
{
    return reinterpret_cast<float*>(this)[i];
}

inline float Vector::operator[](int i) const
{
    return ((float*)this)[i];
}

//-----------------------------------------------------------------------------
// Base address...
//-----------------------------------------------------------------------------
inline float* Vector::Base()
{
    return reinterpret_cast<float*>(this);
}

inline const float* Vector::Base() const
{
    return reinterpret_cast<const float*>(this);
}

inline float Vector::NormalizeInPlace()
{
    return VectorNormalize(*this);
}

inline Vector Vector::Normalized() const
{
    Vector norm = *this;
    VectorNormalize(norm);
    return norm;
}

inline float Vector::DistTo(const Vector& vOther) const
{
    Vector delta = *this - vOther;
    return delta.Length();
}

class MS_ALIGN16 VectorAligned : public Vector
{
public:
    VectorAligned(void) = default;
    VectorAligned(float X, float Y, float Z)
    {
        Init(X, Y, Z);
    }

    explicit VectorAligned(const Vector& vOther)
    {
        Init(vOther.x, vOther.y, vOther.z);
    }

    VectorAligned& operator=(const Vector& vOther)
    {
        Init(vOther.x, vOther.y, vOther.z);
        return *this;
    }

    VectorAligned& operator=(const VectorAligned& vOther)
    {
        Init(vOther.x, vOther.y, vOther.z);
        return *this;
    }

    float w; // this space is used anyway
} MS_ALIGN16_POST;

#endif
