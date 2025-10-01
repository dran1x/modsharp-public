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

#ifndef CSTRIKE_TYPE_VECTOR2D_H
#define CSTRIKE_TYPE_VECTOR2D_H

#include <cmath>

class Vector2D
{
public:
    // Members
    float x{}, y{};

    // Construction/destruction
    Vector2D(void) = default;
    Vector2D(float X, float Y);
    Vector2D(const float* pFloat);

    // Initialization
    void Init(float ix = 0.0f, float iy = 0.0f);

    // Got any nasty NAN's?
    bool IsValid() const;

    // array access...
    float  operator[](int i) const;
    float& operator[](int i);

    // Base address...
    float*       Base();
    const float* Base() const;

    // Initialization methods
    void Random(float minVal, float maxVal);

    // equality
    bool operator==(const Vector2D& v) const;
    bool operator!=(const Vector2D& v) const;

    // arithmetic operations
    Vector2D& operator+=(const Vector2D& v);
    Vector2D& operator-=(const Vector2D& v);
    Vector2D& operator*=(const Vector2D& v);
    Vector2D& operator*=(float s);
    Vector2D& operator/=(const Vector2D& v);
    Vector2D& operator/=(float s);

    // Get the Vector2D's magnitude.
    float Length() const;

    // Get the Vector2D's magnitude squared.
    float LengthSqr(void) const;

    // return true if this vector is (0,0) within tolerance
    bool IsZero(float tolerance = 0.01f) const
    {
        return (x > -tolerance && x < tolerance && y > -tolerance && y < tolerance);
    }

    // Normalize in place and return the old length.
    float NormalizeInPlace();

    // Get the distance from this Vector2D to the other one.
    float DistTo(const Vector2D& vOther) const;

    // Get the distance from this Vector2D to the other one squared.
    float DistToSqr(const Vector2D& vOther) const;

    // Dot product.
    float Dot(const Vector2D& vOther) const;

    // assignment
    Vector2D& operator=(const Vector2D& vOther);

    // copy constructors
    Vector2D(const Vector2D& vOther);

    // arithmetic operations
    Vector2D operator-(void) const;

    Vector2D operator+(const Vector2D& v) const;
    Vector2D operator-(const Vector2D& v) const;
    Vector2D operator*(const Vector2D& v) const;
    Vector2D operator/(const Vector2D& v) const;
    Vector2D operator*(float fl) const;
    Vector2D operator/(float fl) const;
};

inline Vector2D::Vector2D(float X, float Y)
{
    x = X;
    y = Y;
}

inline Vector2D::Vector2D(const float* pFloat)
{
    x = pFloat[0];
    y = pFloat[1];
}

inline void Vector2D::Init(float ix, float iy)
{
    x = ix;
    y = iy;
}

inline bool Vector2D::IsValid() const
{
    return std::isfinite(x) && std::isfinite(y);
}

inline Vector2D& Vector2D::operator=(const Vector2D& vOther) = default;

inline float& Vector2D::operator[](int i)
{
    return ((float*)this)[i];
}

inline float Vector2D::operator[](int i) const
{
    return ((float*)this)[i];
}

inline float* Vector2D::Base()
{
    return (float*)this;
}

inline const float* Vector2D::Base() const
{
    return (const float*)this;
}

inline bool Vector2D::operator==(const Vector2D& src) const
{
    return (src.x == x) && (src.y == y);
}

inline bool Vector2D::operator!=(const Vector2D& src) const
{
    return (src.x != x) || (src.y != y);
}

inline Vector2D& Vector2D::operator+=(const Vector2D& v)
{
    x += v.x;
    y += v.y;
    return *this;
}

inline Vector2D& Vector2D::operator-=(const Vector2D& v)
{
    x -= v.x;
    y -= v.y;
    return *this;
}

inline Vector2D& Vector2D::operator*=(float fl)
{
    x *= fl;
    y *= fl;
    return *this;
}

inline Vector2D& Vector2D::operator*=(const Vector2D& v)
{
    x *= v.x;
    y *= v.y;
    return *this;
}

inline Vector2D& Vector2D::operator/=(float fl)
{
    float oofl = 1.0f / fl;
    x *= oofl;
    y *= oofl;
    return *this;
}

inline float Vector2D::Length() const
{
    return std::sqrt(LengthSqr());
}

inline Vector2D& Vector2D::operator/=(const Vector2D& v)
{
    x /= v.x;
    y /= v.y;
    return *this;
}

inline float Vector2D::LengthSqr(void) const
{
    return (x * x + y * y);
}

inline float Vector2D::NormalizeInPlace()
{
    float l = Length();
    if (l != 0.0f)
    {
        x /= l;
        y /= l;
    }
    else
    {
        x = y = 0.0f;
    }

    return l;
}

inline float Vector2D::DistTo(const Vector2D& vOther) const
{
    Vector2D delta = *this - vOther;
    return delta.Length();
}

inline float Vector2D::DistToSqr(const Vector2D& vOther) const
{
    Vector2D delta = *this - vOther;
    return delta.LengthSqr();
}

inline float Vector2D::Dot(const Vector2D& b) const
{
    return x * b.x + y * b.y;
}

#endif