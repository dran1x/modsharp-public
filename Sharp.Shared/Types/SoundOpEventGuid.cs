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

using System.Runtime.InteropServices;

namespace Sharp.Shared.Types;

[StructLayout(LayoutKind.Sequential, Size = 8)]
public readonly struct SoundOpEventGuid
{
    public readonly SoundEventGuid Guid;
    public readonly int            Hash;

    private SoundOpEventGuid(SoundEventGuid guid, int hash)
    {
        Guid = guid;
        Hash = hash;
    }

    public static implicit operator SoundOpEventGuid(long value)
    {
        unchecked
        {
            var guid = (SoundEventGuid) (uint) (value & 0xFFFFFFFF);
            var hash = (int) ((value >> 32) & 0xFFFFFFFF);

            return new SoundOpEventGuid(guid, hash);
        }
    }

    public static implicit operator long(SoundOpEventGuid value)
    {
        long result = value.Hash;
        result = (result << 32) | (uint) value.Guid;

        return result;
    }

    public bool IsValid()
        => Guid.IsValid() && Hash != -1;
}
