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
using System.Text;
using System.Text.Unicode;

namespace Sharp.Shared.Utilities;

public static class NativeString
{
    public static unsafe string ReadString(byte* ptr)
        => Marshal.PtrToStringUTF8((nint) ptr) ?? string.Empty;

    public static string ReadString(nint ptr)
        => Marshal.PtrToStringUTF8(ptr) ?? string.Empty;

    public static unsafe void WriteString(byte* ptr, int size, string value)
    {
        Utf8.FromUtf16(value, new Span<byte>(ptr, size - 1), out _, out var bytesWritten);
        ptr[bytesWritten] = 0;
    }

    public static int GetByteCount(string value)
        => Encoding.UTF8.GetByteCount(value);

    public static int GetMaxByteCount(string value)
        => Encoding.UTF8.GetMaxByteCount(value.Length);

    public static unsafe int CStringCmp(byte* s1, byte* s2)
    {
        while (*s1 != 0 && *s1 == *s2)
        {
            s1++;
            s2++;
        }

        return *s1 - *s2;
    }
}
