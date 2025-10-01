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
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using Sharp.Shared.Utilities;

namespace Sharp.Shared.Types.Tier;

[StructLayout(LayoutKind.Sequential, Pack = 8, Size = 8)]
public readonly struct CUtlSymbolLarge : IComparable<CUtlSymbolLarge>, IEquatable<CUtlSymbolLarge>
{
    private readonly nint _value;

    public CUtlSymbolLarge()
        => _value = nint.Zero;

    public CUtlSymbolLarge(nint value)
        => _value = value;

    public string Get()
        => NativeString.ReadString(_value);

    public static bool operator ==(CUtlSymbolLarge lv, CUtlSymbolLarge rv)
        => lv._value == rv._value;

    public static bool operator !=(CUtlSymbolLarge lv, CUtlSymbolLarge rv)
        => lv._value != rv._value;

    public static bool operator <(CUtlSymbolLarge lv, CUtlSymbolLarge rv)
        => throw new NotSupportedException();

    public static bool operator >(CUtlSymbolLarge lv, CUtlSymbolLarge rv)
        => throw new NotSupportedException();

    public int CompareTo(CUtlSymbolLarge other)
        => _value.CompareTo(other._value);

    public bool Equals(CUtlSymbolLarge other)
        => _value == other._value;

    public override bool Equals([NotNullWhen(true)] object? obj)
    {
        if (obj is CUtlSymbolLarge r)
        {
            return _value == r._value;
        }

        return false;
    }

    public override int GetHashCode()
        => _value.GetHashCode();
}
