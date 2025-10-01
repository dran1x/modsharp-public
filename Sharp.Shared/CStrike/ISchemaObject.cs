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
using Sharp.Shared.Types;

namespace Sharp.Shared.CStrike;

public interface ISchemaObject : INativeObject
{
    string GetSchemaClassname();

    bool GetNetVar<T>(string fieldName, ushort extraOffset = 0, bool? _ = null)
        where T : IComparable<bool>;

    byte GetNetVar<T>(string fieldName, ushort extraOffset = 0, byte? _ = null)
        where T : IComparable<byte>;

    short GetNetVar<T>(string fieldName, ushort extraOffset = 0, short? _ = null)
        where T : IComparable<short>;

    ushort GetNetVar<T>(string fieldName, ushort extraOffset = 0, ushort? _ = null)
        where T : IComparable<ushort>;

    int GetNetVar<T>(string fieldName, ushort extraOffset = 0, int? _ = null)
        where T : IComparable<int>;

    uint GetNetVar<T>(string fieldName, ushort extraOffset = 0, uint? _ = null)
        where T : IComparable<uint>;

    long GetNetVar<T>(string fieldName, ushort extraOffset = 0, long? _ = null)
        where T : IComparable<long>;

    ulong GetNetVar<T>(string fieldName, ushort extraOffset = 0, ulong? _ = null)
        where T : IComparable<ulong>;

    float GetNetVar<T>(string fieldName, ushort extraOffset = 0, float? _ = null)
        where T : IComparable<float>;

    nint GetNetVar<T>(string fieldName, ushort extraOffset = 0, nint? _ = null)
        where T : IComparable<nint>;

    string GetNetVar<T>(string fieldName, ushort extraOffset = 0, string? _ = null)
        where T : IComparable<string>;

    Vector GetNetVar<T>(string fieldName, ushort extraOffset = 0, Vector? _ = null)
        where T : IComparable<Vector>;

    string GetNetVarUtlSymbolLarge(string fieldName, ushort extraOffset = 0);

    void SetNetVar(string field, bool value, bool isStruct = false, ushort extraOffset = 0);

    void SetNetVar(string field, byte value, bool isStruct = false, ushort extraOffset = 0);

    void SetNetVar(string field, short value, bool isStruct = false, ushort extraOffset = 0);

    void SetNetVar(string field, ushort value, bool isStruct = false, ushort extraOffset = 0);

    void SetNetVar(string field, int value, bool isStruct = false, ushort extraOffset = 0);

    void SetNetVar(string field, uint value, bool isStruct = false, ushort extraOffset = 0);

    void SetNetVar(string field, long value, bool isStruct = false, ushort extraOffset = 0);

    void SetNetVar(string field, ulong value, bool isStruct = false, ushort extraOffset = 0);

    void SetNetVar(string field, float value, bool isStruct = false, ushort extraOffset = 0);

    void SetNetVar(string field, string value, int maxLen, bool isStruct = false, ushort extraOffset = 0);

    void SetNetVar(string field, Vector value, bool isStruct = false, ushort extraOffset = 0);

    void SetNetVarUtlSymbolLarge(string field, string value, bool isStruct = false, ushort extraOffset = 0);

    bool FindNetVar(string field);

    int GetNetVarOffset(string field);

    void NetworkStateChanged(string field, bool isStruct = false, ushort extraOffset = 0);
}
