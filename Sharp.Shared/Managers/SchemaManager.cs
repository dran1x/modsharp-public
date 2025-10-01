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
using Sharp.Shared.CStrike;
using Sharp.Shared.Types;
using Sharp.Shared.Types.Tier;

namespace Sharp.Shared.Managers;

/// <summary>
///     底层与Native Schema交互的接口。<br />
///     用于处理API覆盖不到的，特殊的Schema Class <br />
///     这套接口将会直接和底层数据接触<br />
///     除非你很清楚你在干什么，你要干什么，否则不要随意使用它们！
/// </summary>
public interface ISchemaManager
{
    SchemaField GetSchemaField(string classname, string field);

    int GetNetVarOffset(string classname, string field);

    bool GetNetVar<T>(INativeObject nativeObject, string classname, string field, ushort extraOffset = 0, bool? _ = null)
        where T : IComparable<bool>;

    byte GetNetVar<T>(INativeObject nativeObject, string classname, string field, ushort extraOffset = 0, byte? _ = null)
        where T : IComparable<byte>;

    short GetNetVar<T>(INativeObject nativeObject, string classname, string field, ushort extraOffset = 0, short? _ = null)
        where T : IComparable<short>;

    ushort GetNetVar<T>(INativeObject nativeObject, string classname, string field, ushort extraOffset = 0, ushort? _ = null)
        where T : IComparable<ushort>;

    int GetNetVar<T>(INativeObject nativeObject, string classname, string field, ushort extraOffset = 0, int? _ = null)
        where T : IComparable<int>;

    uint GetNetVar<T>(INativeObject nativeObject, string classname, string field, ushort extraOffset = 0, uint? _ = null)
        where T : IComparable<uint>;

    long GetNetVar<T>(INativeObject nativeObject, string classname, string field, ushort extraOffset = 0, long? _ = null)
        where T : IComparable<long>;

    ulong GetNetVar<T>(INativeObject nativeObject, string classname, string field, ushort extraOffset = 0, ulong? _ = null)
        where T : IComparable<ulong>;

    float GetNetVar<T>(INativeObject nativeObject, string classname, string field, ushort extraOffset = 0, float? _ = null)
        where T : IComparable<float>;

    nint GetNetVar<T>(INativeObject nativeObject, string classname, string field, ushort extraOffset = 0, nint? _ = null)
        where T : IComparable<nint>;

    string GetNetVar<T>(INativeObject nativeObject, string classname, string field, ushort extraOffset = 0, string? _ = null)
        where T : IComparable<string>;

    Vector GetNetVar<T>(INativeObject nativeObject, string classname, string field, ushort extraOffset = 0, Vector? _ = null)
        where T : IComparable<Vector>;

    string GetNetVarUtlSymbolLarge(INativeObject nativeObject, string classname, string field, ushort extraOffset = 0);

    ref CUtlSymbolLarge GetNetVarUtlSymbolLargeRef(INativeObject nativeObject,
        string                                                   classname,
        string                                                   field,
        ushort                                                   extraOffset = 0);

    string GetNetVarUtlString(INativeObject nativeObject, string classname, string field, ushort extraOffset = 0);

    ref CUtlString GetNetVarUtlStringRef(INativeObject nativeObject, string classname, string field, ushort extraOffset = 0);

    ISchemaArray<T> GetSchemaFixedArray<T>(nint pointer, string classname, string field, ISchemaObject chain)
        where T : unmanaged;

    ISchemaArray<T> GetSchemaFixedArray<T>(nint pointer, SchemaField field, ISchemaObject chain)
        where T : unmanaged;

    ISchemaList<T> GetSchemaList<T>(nint pointer, string classname, string field, ISchemaObject chain, bool isStruct)
        where T : unmanaged;

    ISchemaList<T> GetSchemaList<T>(nint pointer, SchemaField field, ISchemaObject chain, bool isStruct)
        where T : unmanaged;

    void SetNetVar(INativeObject nativeObject,
        string                   classname,
        string                   field,
        bool                     value,
        bool                     isStruct    = false,
        ushort                   extraOffset = 0);

    void SetNetVar(INativeObject nativeObject,
        string                   classname,
        string                   field,
        byte                     value,
        bool                     isStruct    = false,
        ushort                   extraOffset = 0);

    void SetNetVar(INativeObject nativeObject,
        string                   classname,
        string                   field,
        short                    value,
        bool                     isStruct    = false,
        ushort                   extraOffset = 0);

    void SetNetVar(INativeObject nativeObject,
        string                   classname,
        string                   field,
        ushort                   value,
        bool                     isStruct    = false,
        ushort                   extraOffset = 0);

    void SetNetVar(INativeObject nativeObject,
        string                   classname,
        string                   field,
        int                      value,
        bool                     isStruct    = false,
        ushort                   extraOffset = 0);

    void SetNetVar(INativeObject nativeObject,
        string                   classname,
        string                   field,
        uint                     value,
        bool                     isStruct    = false,
        ushort                   extraOffset = 0);

    void SetNetVar(INativeObject nativeObject,
        string                   classname,
        string                   field,
        long                     value,
        bool                     isStruct    = false,
        ushort                   extraOffset = 0);

    void SetNetVar(INativeObject nativeObject,
        string                   classname,
        string                   field,
        ulong                    value,
        bool                     isStruct    = false,
        ushort                   extraOffset = 0);

    void SetNetVar(INativeObject nativeObject,
        string                   classname,
        string                   field,
        float                    value,
        bool                     isStruct    = false,
        ushort                   extraOffset = 0);

    void SetNetVar(INativeObject nativeObject,
        string                   classname,
        string                   field,
        string                   value,
        int                      maxLen,
        bool                     isStruct    = false,
        ushort                   extraOffset = 0);

    void SetNetVar(INativeObject nativeObject,
        string                   classname,
        string                   field,
        Vector                   value,
        bool                     isStruct    = false,
        ushort                   extraOffset = 0);

    void SetNetVarUtlSymbolLarge(INativeObject nativeObject,
        string                                 classname,
        string                                 field,
        string                                 value,
        bool                                   isStruct    = false,
        ushort                                 extraOffset = 0);

    void SetNetVarUtlString(INativeObject ptr,
        string                            classname,
        string                            field,
        string                            value,
        bool                              isStruct    = false,
        ushort                            extraOffset = 0);

    void NetVarStateChanged(
        INativeObject    nativeObject,
        SchemaClass      schemaClass,
        SchemaClassField schemaField,
        ushort           extraOffset = 0,
        bool             isStruct    = false
    );
}
