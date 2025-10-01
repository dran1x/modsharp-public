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

namespace Sharp.Shared.Utilities;

public static class IntPtrExtensions
{
    public static unsafe nint Dereference(this nint ptr)
        => new (*(void**) ptr.ToPointer());

    public static unsafe nint Dereference(this nint ptr, int dereferences)
    {
        var x = ptr.ToPointer();

        while (dereferences-- > 0)
        {
            x = *(void**) x;
        }

        return new nint(x);
    }

    public static nint Add(this nint ptr, int offset)
        => nint.Add(ptr, offset);

    public static bool IsValid(this nint ptr)
        => ptr != nint.Zero;
}
