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

#ifndef CSTRIKE_TYPE_COLOR32_H
#define CSTRIKE_TYPE_COLOR32_H

#include <cstdint>

class Color32
{
public:
    // constructors
    Color32()
    {
        *reinterpret_cast<int32_t*>(this) = 0;
    }
    Color32(int _r, int _g, int _b)
    {
        SetColor(_r, _g, _b, 0);
    }
    Color32(int _r, int _g, int _b, int _a)
    {
        SetColor(_r, _g, _b, _a);
    }

    void SetColor(int _r, int _g, int _b, int _a = 0)
    {
        _color[0] = static_cast<uint8_t>(_r);
        _color[1] = static_cast<uint8_t>(_g);
        _color[2] = static_cast<uint8_t>(_b);
        _color[3] = static_cast<uint8_t>(_a);
    }

    [[nodiscard]] uint8_t r() const
    {
        return _color[0];
    }

    [[nodiscard]] uint8_t g() const
    {
        return _color[1];
    }

    [[nodiscard]] uint8_t b() const
    {
        return _color[2];
    }

    [[nodiscard]] uint8_t a() const
    {
        return _color[3];
    }

private:
    uint8_t _color[4];
};

#endif