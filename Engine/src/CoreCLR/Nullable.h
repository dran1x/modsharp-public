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

#ifndef CORECLR_NULLABLE_H
#define CORECLR_NULLABLE_H

template <typename T>
class Nullable
{
public:
    Nullable() :
        m_hasValue(false) {}
    Nullable(T value) :
        m_hasValue(true), m_value(value) {}

    auto HasValue() const { return m_hasValue; }
    T&   GetValue() { return m_value; }
    T&   GetValueOrDefault(T defaultValue) { return m_hasValue ? m_value : defaultValue; }

private:
    bool m_hasValue;
    T    m_value;
};

#endif