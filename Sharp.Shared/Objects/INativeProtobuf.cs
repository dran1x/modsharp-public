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

using Google.Protobuf;
using Sharp.Shared.CStrike;

namespace Sharp.Shared.Objects;

public interface INativeProtobuf : INativeObject
{
    string GetName();

    ulong GetSize();

    bool HasField(string field);

    int GetRepeatedFieldCount(string field);

    int? ReadInt32(string field, int repeatedIndex = -1);

    uint? ReadUInt32(string field, int repeatedIndex = -1);

    long? ReadInt64(string field, int repeatedIndex = -1);

    ulong? ReadUInt64(string field, int repeatedIndex = -1);

    bool? ReadBool(string field, int repeatedIndex = -1);

    int? ReadEnum(string field, int repeatedIndex = -1);

    float? ReadFloat(string field, int repeatedIndex = -1);

    double? ReadDouble(string field, int repeatedIndex = -1);

    string? ReadString(string field, int repeatedIndex = -1);

    byte[]? ReadBytes(string field, int repeatedIndex = -1);

    bool SetInt32(string field, int value, int repeatedIndex = -1);

    bool SetUInt32(string field, uint value, int repeatedIndex = -1);

    bool SetInt64(string field, long value, int repeatedIndex = -1);

    bool SetUInt64(string field, ulong value, int repeatedIndex = -1);

    bool SetBool(string field, bool value, int repeatedIndex = -1);

    bool SetEnum(string field, int value, int repeatedIndex = -1);

    bool SetFloat(string field, float value, int repeatedIndex = -1);

    bool SetDouble(string field, double value, int repeatedIndex = -1);

    bool SetString(string field, string value, int repeatedIndex = -1);

    bool SetBytes(string field, byte[] value, int repeatedIndex = -1);

    int AddInt32(string field, int value);

    int AddUInt32(string field, uint value);

    int AddInt64(string field, long value);

    int AddUInt64(string field, ulong value);

    int AddBool(string field, bool value);

    int AddEnum(string field, int value);

    int AddFloat(string field, float value);

    int AddDouble(string field, double value);

    int AddString(string field, string value);

    int AddBytes(string field, byte[] value);

    INativeProtobuf? ReadMessage(string field, int repeatedIndex = -1);

    INativeProtobuf? AddMessage(string field);

    bool RemoveRepeatedFieldValue(string field, int repeatedIndex);

    bool CopyFromOtherMessage(IMessage message);

    T Deserialize<T>() where T : class, IMessage<T>, IMessage, new();
}
