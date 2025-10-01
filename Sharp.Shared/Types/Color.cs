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

namespace Sharp.Shared.Types;

[StructLayout(LayoutKind.Sequential)]
public struct Color : IEquatable<Color>
{
    public byte R { get; set; }
    public byte G { get; set; }
    public byte B { get; set; }

    public Color(byte r, byte g, byte b)
    {
        R = r;
        G = g;
        B = b;
    }

    public readonly bool Same(Color32 other)
        => R == other.R && G == other.G && B == other.B;

    public override bool Equals(object? obj)
    {
        if (obj is Color h)
        {
            return this == h;
        }

        return false;
    }

    public override int GetHashCode()
        => HashCode.Combine(R, G, B);

    public byte this[int key]
    {
        get
            => key switch
            {
                0 => R,
                1 => G,
                2 => B,
                _ => throw new ArgumentOutOfRangeException(nameof(key), key, null),
            };
        set
        {
            switch (key)
            {
                case 0:
                    R = value;

                    break;
                case 1:
                    G = value;

                    break;
                case 2:
                    B = value;

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(key), key, null);
            }
        }
    }

    public static bool operator ==(Color a, Color b)
        => a.R == b.R && a.G == b.G && a.B == b.B;

    public static bool operator !=(Color a, Color b)
        => !(a == b);

    public bool Equals(Color other)
        => R    == other.R
           && G == other.G
           && B == other.B;
}

[StructLayout(LayoutKind.Sequential)]
public struct Color32 : IEquatable<Color32>
{
    public byte R { get; set; }
    public byte G { get; set; }
    public byte B { get; set; }
    public byte A { get; set; }

    public Color32(byte r, byte g, byte b, byte a)
    {
        R = r;
        G = g;
        B = b;
        A = a;
    }

    public readonly bool Same(Color32 other)
        => R == other.R && G == other.G && B == other.B && A == other.A;

    public override bool Equals(object? obj)
    {
        if (obj is Color32 h)
        {
            return this == h;
        }

        return false;
    }

    public override int GetHashCode()
        => HashCode.Combine(R, G, B, A);

    public byte this[int key]
    {
        get
            => key switch
            {
                0 => R,
                1 => G,
                2 => B,
                3 => A,
                _ => throw new ArgumentOutOfRangeException(nameof(key), key, null),
            };
        set
        {
            switch (key)
            {
                case 0:
                    R = value;

                    break;
                case 1:
                    G = value;

                    break;
                case 2:
                    B = value;

                    break;
                case 3:
                    A = value;

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(key), key, null);
            }
        }
    }

    public static bool operator ==(Color32 a, Color32 b)
        => a.R == b.R && a.G == b.G && a.B == b.B && a.A == b.A;

    public static bool operator !=(Color32 a, Color32 b)
        => !(a == b);

    public static unsafe implicit operator uint(Color32 color)
        => *(uint*) &color;

    public bool Equals(Color32 other)
        => R    == other.R
           && G == other.G
           && B == other.B
           && A == other.A;
}
