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

using Sharp.Shared.CStrike;
using Sharp.Shared.Enums;
using Sharp.Shared.Types;

namespace Sharp.Shared.Objects;

public interface IKeyValues : INativeObject
{
    /// <summary>
    ///     销毁实例 <br />
    ///     <remarks>只有创建出来的实例和Clone的是可以销毁的</remarks>
    /// </summary>
    void DeleteThis();

    /// <summary>
    ///     克隆一个新的kv
    /// </summary>
    IKeyValues Clone();

    /// <summary>
    ///     清空所有Key和SubKey
    /// </summary>
    void Clear();

    /// <summary>
    ///     从文件加载
    /// </summary>
    bool LoadFromFile(string filename, string? pathId = null);

    /// <summary>
    ///     保存到文件
    /// </summary>
    bool SaveToFile(string filename, string? pathId = null, bool allowEmptyString = false);

    /// <summary>
    ///     从文本加载
    /// </summary>
    bool LoadFromString(string buffer);

    /// <summary>
    ///     导出为String
    /// </summary>
    bool SaveToString(int size,
        out string        result,
        int               indent           = 0,
        bool              sort             = false,
        bool              allowEmptyString = false);

    /// <summary>
    ///     当前Section名
    /// </summary>
    string GetSectionName();

    /// <summary>
    ///     设置Section名
    /// </summary>
    void SetSectionName(string name);

    /// <summary>
    ///     获取第一个SubKey (不限制为Key, 可能是KV)
    /// </summary>
    IKeyValues? GetFirstSubKey();

    /// <summary>
    ///     获取最后一个SubKey (不限制为Key, 可能是KV)
    /// </summary>
    IKeyValues? FindLastSubKey();

    /// <summary>
    ///     获取下一个Key
    /// </summary>
    IKeyValues? GetNextKey();

    /// <summary>
    ///     获取第一个SubKey (限制为Key)
    /// </summary>
    IKeyValues? GetFirstTrueSubKey();

    /// <summary>
    ///     获取下一个SubKey (限制为Key)
    /// </summary>
    IKeyValues? GetNextTrueSubKey();

    /// <summary>
    ///     直接查找Key, 可创建
    /// </summary>
    /// <param name="name">name</param>
    /// <param name="bCreate">如果不存在则创建</param>
    IKeyValues? FindKey(string name, bool bCreate = false);

    /// <summary>
    ///     查找并删除指定名字的SubKey
    /// </summary>
    bool FindAndDeleteSubKey(string name);

    /// <summary>
    ///     添加Key
    /// </summary>
    IKeyValues AddKey(string name);

    /// <summary>
    ///     获取值的类型
    /// </summary>
    KeyValuesDataType GetDataType(string? name = null);

    int GetInt(string? name = null, int defaultValue = 0);

    ulong GetUint64(string? name = null, ulong defaultValue = 0);

    float GetFloat(string? name = null, float defaultValue = 0.0f);

    string GetString(string? name = null, string defaultValue = "");

    nint GetPtr(string? name = null);

    bool GetBool(string? name = null, bool defaultValue = false);

    bool IsEmpty(string? name = null);

    void SetString(string name, string value);

    void SetInt(string name, int value);

    void SetUint64(string name, ulong value);

    void SetFloat(string name, float value);

    void SetPtr(string name, nint value);

    void SetBool(string name, bool value);
}

public interface IKeyValues3 : INativeObject
{
    /// <summary>
    ///     销毁实例 <br />
    ///     <remarks>只有创建出来的实例和Clone的是可以销毁的</remarks>
    /// </summary>
    void DeleteThis();

    bool LoadFromFile(string file, string pathId, out string error);

    bool LoadFromCompiledFile(string file, string pathId, out string error);

    bool LoadFromBuffer(byte[] buffer, out string error);

    KeyValues3Type GetKvType();

    bool IsInvalid();

    bool IsNull();

    bool IsBool();

    bool IsInt();

    bool IsUInt();

    /// <summary>
    ///     IsInt || IsUInt
    /// </summary>
    /// <returns></returns>
    bool IsIntegral();

    bool IsDouble();

    bool IsString();

    /// <summary>
    ///     不知道干啥的
    /// </summary>
    /// <returns></returns>
    bool IsBinaryBlob();

    bool IsArray();

    bool IsTable();

    bool IsNullOrInvalid();

    KeyValues3SubType GetSubType();

    int GetArrayElementCount();

    IKeyValues3? GetArrayElement(int index);

    IKeyValues3? AddArrayElementToTail();

    void RemoveArrayElement(int index);

    int GetMemberCount();

    IKeyValues3? GetMember(int index);

    string GetMemberName(int index);

    IKeyValues3? FindMember(string name);

    IKeyValues3? FindOrCreateMember(string name, out bool create);

    bool RemoveMember(string name);

    // getter
    bool GetBool(bool defaultValue = false);

    sbyte GetInt8(sbyte defaultValue = 0);

    short GetInt16(short defaultValue = 0);

    int GetInt32(int defaultValue = 0);

    long GetInt64(long defaultValue = 0);

    byte GetUInt8(byte defaultValue = 0);

    ushort GetUInt16(ushort defaultValue = 0);

    uint GetUInt32(uint defaultValue = 0);

    ulong GetUInt64(ulong defaultValue = 0);

    float GetFloat(float defaultValue = 0);

    double GetDouble(double defaultValue = 0);

    string GetString(string defaultValue = "");

    Color32 GetColor();

    Vector GetVector();

    Vector GetQAngle();

    Matrix3x4 GetMatrix();

    // setter
    void SetBool(bool value);

    void SetInt8(sbyte value);

    void SetInt16(short value);

    void SetInt32(int value);

    void SetInt64(long value);

    void SetUInt8(byte value);

    void SetUInt16(ushort value);

    void SetUInt32(uint value);

    void SetUInt64(ulong value);

    void SetFloat(float value);

    void SetDouble(double value);

    void SetString(string value);

    void SetColor(Color32 value);

    void SetVector(Vector value);

    void SetQAngle(Vector value);

    void SetMatrix(Matrix3x4 value);
}
