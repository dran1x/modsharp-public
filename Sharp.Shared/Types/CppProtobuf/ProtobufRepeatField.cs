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
using System.Runtime.InteropServices;

namespace Sharp.Shared.Types.CppProtobuf;

[StructLayout(LayoutKind.Sequential, Pack = 8, Size = 0x18)]
public readonly unsafe ref struct RepeatedField<T> where T : unmanaged
{
    private readonly nint pArena;

    public readonly int nCurrentSize;

    public readonly int nTotalSize;

    private readonly RepeatedFieldElement<T>* Elements;

    // ReSharper disable once InconsistentNaming
    public int nAllocatedSize => Elements->nAllocatedSize;

    public T* this[int index]
    {
        get
        {
            if (index < 0 || index >= nCurrentSize || index >= Elements->nAllocatedSize)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            return (&Elements->Elements)[index];
        }
    }
}

[StructLayout(LayoutKind.Sequential, Pack = 8, Size = 0x10)]
internal readonly unsafe ref struct RepeatedFieldElement<T> where T : unmanaged
{
    public readonly int nAllocatedSize;

#region Padding

    private readonly int nPadding04;

#endregion

    public readonly T* Elements;
}
