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

#ifndef CORECLR_SPAN_H
#define CORECLR_SPAN_H

template <typename T>
class NativeSpan
{
public:
    T*  m_pData;
    int m_nLength;

    T* begin() const { return m_pData; }
    T* end() const { return m_pData + m_nLength; }

    NativeSpan() = delete;

    explicit NativeSpan(T* pData, int iLength) :
        m_pData(pData), m_nLength(iLength) {}
};

template <typename T>
class NativeFixedSpan
{
public:
    T*  m_pData;
    int m_nCount;
    int m_nLength;

    NativeFixedSpan() = delete;

    explicit NativeFixedSpan(T* pData, int count, int length) :
        m_pData(pData), m_nCount(count), m_nLength(length) {}
};

#endif
