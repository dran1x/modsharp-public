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

namespace Sharp.Shared.Enums;

public enum KeyValues3Type : byte
{
    Invalid,
    Null,
    Bool,
    Int,
    UInt,
    Double,
    String,
    BinaryBlob,
    Array,
    Table,
}

public enum KeyValues3SubType : byte
{
    Invalid,

    Resource,
    ResourceName,
    Panorama,
    SoundEvent,

    SubClass,
    EntityName,
    Localize,

    UnSpecified,

    Null,
    BinaryBlob,
    Array,
    Table,
    Bool8,
    Char8,
    UChar32,
    Int8,
    UInt8,
    Int16,
    UInt16,
    Int32,
    UInt32,
    Int64,
    UInt64,
    Float32,
    Float64,
    String,
    Pointer,
    Color32,

    Vector,
    Vector2D,
    Vector4D,
    RotationVector,
    Quaternion,
    QAngle,
    Matrix3X4,
    Transform,

    StringToken,
    EHandle,
}
