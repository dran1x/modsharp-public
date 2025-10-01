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
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Sharp.Shared.Types.Tier;

// ReSharper disable InconsistentNaming
[StructLayout(LayoutKind.Sequential, Pack = 8)]
public unsafe struct CUtlLeanVectorBase<T, I> : IDisposable, IEnumerable<T> where T : unmanaged
    where I : unmanaged, IBinaryInteger<I>, IMinMaxValue<I>
{
    private I  _count;
    private I  _allocated;
    private T* _elements;

    public int  Count                 => int.CreateChecked(_count);
    public int  Size                  => int.CreateChecked(_count);
    public int  NumAllocated          => int.CreateChecked(_allocated & ~ EXTERNAL_BUFFER_MARKER);
    public bool IsExternallyAllocated => (_allocated & EXTERNAL_BUFFER_MARKER) != I.Zero;

    public Span<T> AsSpan()
        => new (_elements, Count);

    public ReadOnlySpan<T> AsReadOnlySpan()
        => new (_elements, Count);

    public ref T this[Index index] => ref _elements[index.GetOffset(Count)];

    public Span<T> this[Range range] => new Span<T>(_elements, Count)[range];

    private static readonly I EXTERNAL_BUFFER_MARKER = I.One << ((sizeof(I) * 8) - 1);

    public void Dispose()
    {
        Purge();
    }

    public T* Base()
        => _elements;

    public void Purge()
    {
        if (IsExternallyAllocated)
        {
            return;
        }

        MemoryAllocator.Free(_elements);
        _elements  = null;
        _count     = I.Zero;
        _allocated = I.Zero;
    }

    public void EnsureCapacity(int num, bool force = false)
    {
        if (num <= NumAllocated)
        {
            return;
        }

        var numAsTCount   = I.CreateChecked(num);
        var nMinAllocated = I.CreateChecked((31 + sizeof(T)) / sizeof(T));

        var nMaxAllocated = I.MaxValue & ~EXTERNAL_BUFFER_MARKER;

        if (numAsTCount > nMaxAllocated)
        {
            throw new
                OutOfMemoryException(
                    $"Cannot allocate {num} elements; exceeds the maximum capacity of {nMaxAllocated} for this vector type.");
        }

        I nNewAllocated;

        if (force)
        {
            nNewAllocated = numAsTCount;
        }
        else
        {
            var currentAllocated = _allocated & ~EXTERNAL_BUFFER_MARKER;
            nNewAllocated = CalcNewDoublingCount(currentAllocated, numAsTCount, nMinAllocated, nMaxAllocated);
        }

        T* pNew;

        var byteCount    = (nuint) int.CreateChecked(nNewAllocated) * (nuint) Unsafe.SizeOf<T>();
        var oldByteCount = (nuint) Count                            * (nuint) Unsafe.SizeOf<T>();

        if (IsExternallyAllocated)
        {
            pNew = (T*) MemoryAllocator.Alloc(byteCount);

            if (pNew == null)
            {
                throw new OutOfMemoryException();
            }

            if (_elements != null && Count > 0)
            {
                MemoryAllocator.MemMove(pNew, _elements, oldByteCount);
            }
        }
        else
        {
            pNew = (T*) MemoryAllocator.Realloc(_elements, byteCount);

            if (pNew == null)
            {
                throw new OutOfMemoryException();
            }
        }

        _elements  = pNew;
        _allocated = nNewAllocated;
    }

    public void AddToTail(T value)
        => AddToTail(ref value);

    public void AddToTail(ref T value)
    {
        var newElementIndex = _count;

        var requiredCount = _count + I.One;
        EnsureCapacity(int.CreateChecked(requiredCount));

        _elements[int.CreateChecked(newElementIndex)] = value;

        _count = requiredCount;
    }

    public void Add(T item)
        => Add(ref item);

    public void Add(ref readonly T item)
        => Insert(int.CreateChecked(_count), in item);

    public ref T AddToTailGetRef()
    {
        var newIndex = int.CreateChecked(_count);

        var requiredCount = _count + I.One;
        EnsureCapacity(int.CreateChecked(requiredCount));

        _count = requiredCount;

        return ref _elements[newIndex];
    }

    public T* AddToTailGetPtr()
    {
        var newIndex = int.CreateChecked(_count);

        var requiredCount = _count + I.One;
        EnsureCapacity(int.CreateChecked(requiredCount));

        _count = requiredCount;

        return _elements + newIndex;
    }

    public void Insert(int index, T item)
        => Insert(index, ref item);

    private void Insert(int index, ref readonly T value)
    {
        if (index < 0 || index > Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Index must be within the bounds of the vector [0, Count].");
        }

        var requiredCount = _count + I.One;
        EnsureCapacity(int.CreateChecked(requiredCount));

        var elementsToMove = Count - index;

        if (elementsToMove > 0)
        {
            var source      = _elements + index;
            var destination = _elements + index + 1;
            var bytesToMove = (nuint) elementsToMove * (nuint) Unsafe.SizeOf<T>();

            MemoryAllocator.MemMove(destination, source, bytesToMove);
        }

        _elements[index] = value;

        _count = requiredCount;
    }

    public void RemoveAll()
    {
        for (var i = int.CreateChecked(_count - I.One); i >= 0; i--)
        {
            _elements[i] = default;
        }

        _count = I.Zero;
    }

    public void Remove(int index)
    {
        if (index < 0 || index >= Count)
        {
            throw new ArgumentOutOfRangeException(nameof(index), "Index is out of range.");
        }

        ShiftElements(_elements + index,
                      _elements + index + 1,
                      _elements + int.CreateChecked(_count));

        _count -= I.One;

        _elements[int.CreateChecked(_count)] = default;
    }

    private static I CalcNewDoublingCount(
        I currentAllocated,
        I numRequired,
        I minAllocated,
        I maxAllocated)
    {
        var newAllocated = currentAllocated;

        var two = I.CreateChecked(2);

        while (newAllocated < numRequired)
        {
            if (newAllocated < maxAllocated / two)
            {
                newAllocated *= two;
                newAllocated =  I.Max(newAllocated, minAllocated);
            }
            else
            {
                newAllocated = maxAllocated;

                break;
            }
        }

        return newAllocated;
    }

    private static void ShiftElements(T* pDest, T* pSrc, T* pSrcEnd)
    {
        var numToMove = pSrcEnd - pSrc;

        if (numToMove > 0)
        {
            var count = (int) numToMove;

            var sourceSpan      = new Span<T>(pSrc,  count);
            var destinationSpan = new Span<T>(pDest, count);
            sourceSpan.CopyTo(destinationSpan);
        }
    }

    public struct Enumerator : IEnumerator<T>, IEnumerator
    {
        private readonly CUtlLeanVectorBase<T, I> _vector;
        private          int                      _index;

        public Enumerator(CUtlLeanVectorBase<T, I> vector)
        {
            _vector = vector;
            _index  = -1;
        }

        public bool MoveNext()
        {
            _index++;

            return _index < _vector.Size;
        }

        public void Reset()
        {
            _index = -1;
        }

        public T Current => _vector[_index];

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }
    }

    public Enumerator GetEnumerator()
        => new (this);

    IEnumerator IEnumerable.GetEnumerator()
        => ((IEnumerable<T>) this).GetEnumerator();

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
        => GetEnumerator();

#if false
    {
        for (var i = 0; i < int.CreateChecked(_count); i++)
        {
            yield return this[i];
        }
    }
#endif
}
