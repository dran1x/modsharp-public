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

namespace Sharp.Shared;

public interface IGameData
{
    /// <summary>
    ///     搜索 Offset
    /// </summary>
    /// <param name="name">Key值</param>
    /// <param name="offset">搜索结果</param>
    /// <returns>是否搜索成功</returns>
    bool GetOffset(string name, out int offset);

    /// <summary>
    ///     搜索 Address
    /// </summary>
    /// <param name="name">Key值</param>
    /// <param name="address">搜索结果</param>
    /// <returns>是否搜索成功</returns>
    bool GetAddress(string name, out nint address);

    /// <summary>
    ///     搜索VTable Function Index
    /// </summary>
    /// <param name="name">Key值</param>
    /// <param name="index">搜索结果</param>
    /// <returns>是否搜索成功</returns>
    bool GetVFuncIndex(string name, out int index);

    /// <summary>
    ///     搜索 Offset 失败时抛出异常
    /// </summary>
    /// <param name="name">Key值</param>
    /// <returns>Offset值</returns>
    int GetOffset(string name);

    /// <summary>
    ///     自动合并为 $"{classname}::{name}" 并搜索 Offset
    /// </summary>
    /// <param name="classname">类名</param>
    /// <param name="name">成员名</param>
    /// <returns>搜索结果</returns>
    int GetOffset(string classname, string name);

    /// <summary>
    ///     搜索 Address 失败时抛出异常
    /// </summary>
    /// <param name="name">Key值</param>
    /// <returns>Address值</returns>
    nint GetAddress(string name);

    /// <summary>
    ///     自动合并为 $"{classname}::{name}" 并搜索 Address
    /// </summary>
    /// <param name="classname">类名</param>
    /// <param name="name">成员名</param>
    /// <returns>搜索结果</returns>
    nint GetAddress(string classname, string name);

    /// <summary>
    ///     搜索 VTable Function Index
    /// </summary>
    /// <param name="name"></param>
    /// <returns>Index</returns>
    int GetVFuncIndex(string name);

    /// <summary>
    ///     自动合并为 $"{classname}::{name}" 并搜索 VTable Function Index
    /// </summary>
    /// <param name="classname">类名</param>
    /// <param name="name">成员名</param>
    /// <returns>搜索结果</returns>
    int GetVFuncIndex(string classname, string name);

    /// <summary>
    ///     注册 GameData 文件
    /// </summary>
    /// <param name="path">文件路径 (自动搜索 GameData 目录)</param>
    void Register(string path);

    /// <summary>
    ///     卸载 GameData 文件
    /// </summary>
    /// <param name="path">文件路径 (自动搜索 GameData 目录)</param>
    void Unregister(string path);
}
