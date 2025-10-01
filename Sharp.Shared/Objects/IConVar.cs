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

// ReSharper disable InconsistentNaming
// ReSharper disable MemberCanBeProtected.Global
// ReSharper disable MemberCanBePrivate.Global

namespace Sharp.Shared.Objects;

public interface IConVar : INativeObject
{
    /// <summary>
    ///     ConVar名
    /// </summary>
    string Name { get; }

    /// <summary>
    ///     默认值
    /// </summary>
    string DefaultValue { get; }

    /// <summary>
    ///     提示文本
    /// </summary>
    string HelpString { get; }

    /// <summary>
    ///     Flags
    /// </summary>
    ConVarFlags Flags { get; set; }

    /// <summary>
    ///     ConVar的值类型
    /// </summary>
    ConVarType Type { get; }

    bool GetBool();

    short GetInt16();

    ushort GetUInt16();

    int GetInt32();

    uint GetUInt32();

    long GetInt64();

    ulong GetUInt64();

    float GetFloat();

    double GetDouble();

    ref ConVarVariantValue Get();

    void Set(int value);

    void Set(bool value);

    void Set(float value);

    void Set(string value);

    void Set(ConVarVariantValue value);

    bool SetMinBound(ConVarVariantValue value);

    bool SetMaxBound(ConVarVariantValue value);

    /// <summary>
    ///     将字符串自动解析为ConVar对应的类型并设置 <br />
    ///     <remarks>不符合类型解析失败将设置对应类型默认值</remarks>
    /// </summary>
    /// <param name="value"></param>
    void SetString(string value);

    /// <summary>
    ///     将各种ConVar的值转换为字符串表达方式 <br />
    /// </summary>
    /// <returns></returns>
    string GetString();

    /// <summary>
    ///     ReplicateToClient 而不修改值
    /// </summary>
    void ReplicateToClient(IGameClient client, string value);
}
