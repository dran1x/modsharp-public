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

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Sharp.Shared.Types.Runtime;

[StructLayout(LayoutKind.Sequential)]
public readonly unsafe struct NativeSpan<T> where T : unmanaged
{
    public readonly T*  Data;
    public readonly int Length;

    public NativeSpan(T* data, int length)
    {
        Data   = data;
        Length = length;
    }

    public static implicit operator NativeSpan<T>(Span<T> span)
        => new ((T*) Unsafe.AsPointer(ref span.GetPinnableReference()), span.Length);

    public static implicit operator Span<T>(NativeSpan<T> span)
        => new (span.Data, span.Length);

    public Span<T> AsSpan()
        => new (Data, Length);

    public T this[int index]
    {
        get => Data[index];
        set => Data[index] = value;
    }

    public void Set(int index, T value)
        => Data[index] = value;
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct NativeFixedSpan<T> where T : unmanaged
{
    public readonly T*  Data;
    public          int Count;
    public readonly int Length;

    public NativeFixedSpan(T* data, int count, int length)
    {
        Data   = data;
        Count  = count;
        Length = length;
    }

    public readonly Span<T> AsSpan()
        => new (Data, Length);

    public readonly ReadOnlySpan<T> AsReadOnlySpan()
        => new (Data, Count);

    public T this[int index]
    {
        get => Data[index];
        set => Data[index] = value;
    }

    public void Set(int index, T value)
        => Data[index] = value;
}
