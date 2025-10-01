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

[StructLayout(LayoutKind.Sequential, Size = 4)]
public readonly struct SoundEventGuid
{
    public static implicit operator uint(SoundEventGuid guid)
        => guid.Id;

    public static implicit operator SoundEventGuid(uint id)
        => new (id);

    public readonly uint Id;

    private SoundEventGuid(uint id)
        => Id = id;

    public bool IsValid()
        => Id != 0 && Id != int.MaxValue;
}
