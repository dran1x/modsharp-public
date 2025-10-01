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

#ifndef CSTRIKE_TYPE_CBUFFERSTRING_H
#define CSTRIKE_TYPE_CBUFFERSTRING_H

#include "platform.h"

class CBufferString
{
protected:
    CBufferString(int32_t allocated_size, bool can_grow_on_heap = true) :
        m_nLength(0),
        m_bOverflowed(0),
        m_bShouldFreeHeapMemory(0),
        m_nAllocatedSize(allocated_size),
        m_bStackAllocated(true),
        m_bCanGrowOnHeap(can_grow_on_heap)
    {
    }

public:
    CBufferString(bool can_grow_on_heap = true) :
        m_nLength(0),
        m_bOverflowed(0),
        m_bShouldFreeHeapMemory(0),
        m_nAllocatedSize(0),
        m_bStackAllocated(0),
        m_bCanGrowOnHeap(can_grow_on_heap)
    {}

    CBufferString(const char* string, bool can_grow_on_heap = true) :
        CBufferString(can_grow_on_heap)
    {
        Insert(0, string);
    }

    CBufferString(const CBufferString& other) :
        CBufferString() { *this = other; }

    CBufferString& operator=(const CBufferString& src)
    {
        Clear();
        Insert(0, src.Get());
        return *this;
    }

    ~CBufferString()
    {
        Purge();
    }

    int32_t Length() const
    {
        return m_nLength;
    }

    const char* Get() const
    {
        if (auto base = Base())
            return base;

        return "";
    }

    void Clear()
    {
        auto base = Base();
        if (base != nullptr)
        {
            base[0] = '\0';
        }

        m_nLength = 0;
    }

    void Append(const char* string)
    {
        Insert(Length(), string);
    }

    MS_CLASS_IMPORT const char* Insert(int index, const char* string, int string_length = -1, bool _ = false);

    MS_CLASS_IMPORT void Purge(int bytes_to_preserve = 0);

private:
    int32_t m_nLength : 30;
    int32_t m_bOverflowed : 1;
    int32_t m_bShouldFreeHeapMemory : 1;

    int32_t m_nAllocatedSize : 30;
    int32_t m_bStackAllocated : 1;
    int32_t m_bCanGrowOnHeap : 1;

    union {
        char* m_pHeapString;
        char  m_szStackString[8];
    };

    char* Base()
    {
        if (m_bStackAllocated != 0)
            return m_szStackString;

        return m_pHeapString;
    }

    const char* Base() const
    {
        if (m_bStackAllocated != 0)
            return m_szStackString;

        return m_pHeapString;
    }
};

template <size_t SIZE>
class CFixedBufferString : public CBufferString
{
    static constexpr size_t AlignUp(size_t value, size_t alignment)
    {
        return (value + alignment - 1) & ~(alignment - 1);
    }

public:
    static constexpr size_t ALIGNMENT = 8;

    static constexpr size_t BASE_CLASS_OVERHEAD_ESTIMATE = 8;

    static_assert(SIZE > BASE_CLASS_OVERHEAD_ESTIMATE, "SIZE must be larger than the base class overhead.");

    static constexpr size_t DATA_SIZE = AlignUp(SIZE - BASE_CLASS_OVERHEAD_ESTIMATE, ALIGNMENT);

    CFixedBufferString(bool allowHeapAllocation = true) :
        CBufferString(DATA_SIZE, allowHeapAllocation), m_FixedString{} {}

    CFixedBufferString(const char* pString, bool allowHeapAllocation = true) :
        CFixedBufferString(allowHeapAllocation)
    {
        Insert(0, pString);
    }

    ~CFixedBufferString()
    {
        PurgeFixedString();
    }

    void PurgeFixedString()
    {
        Purge(DATA_SIZE);
    }

private:
    char m_FixedString[DATA_SIZE];
};
#endif