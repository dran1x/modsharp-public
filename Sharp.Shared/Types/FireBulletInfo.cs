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

[StructLayout(LayoutKind.Explicit, Pack = 4, Size = 56)]
public struct FireBulletInfo
{
    [FieldOffset(0)]
    public Vector ShootPosition;

    [FieldOffset(12)]
    public Vector ShootAngles;

    [FieldOffset(24)]
    public float Range;

    [FieldOffset(28)]
    public float RangeModifier;

    [FieldOffset(32)]
    public float Penetration;

    [FieldOffset(36)]
    public int PenetrationCount;

    [FieldOffset(40)]
    public int Damage;

    [FieldOffset(44)]
    public int CurrentBullet;

    [FieldOffset(48)]
    public float SpreadX;

    [FieldOffset(52)]
    public float SpreadY;
}
