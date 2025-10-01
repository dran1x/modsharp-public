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
using System.Collections.Generic;

namespace Sharp.Shared.Hooks;

public interface IVirtualHook : IRuntimeNativeHook
{
    /// <summary>
    ///     准备Hook
    /// </summary>
    /// <param name="module">dll</param>
    /// <param name="class">class</param>
    /// <param name="function">function</param>
    /// <param name="hookFn">UnmanagedCallersOnly 静态函数</param>
    /// <exception cref="EntryPointNotFoundException">找不到VTable</exception>
    /// <exception cref="KeyNotFoundException">GameData 找不到</exception>
    void Prepare(string module, string @class, string function, nint hookFn);

    /// <summary>
    ///     准备Hook
    /// </summary>
    /// <param name="module">dll</param>
    /// <param name="class">class</param>
    /// <param name="offset">offset</param>
    /// <param name="hookFn">UnmanagedCallersOnly 静态函数</param>
    /// <exception cref="EntryPointNotFoundException">找不到VTable</exception>
    /// <exception cref="KeyNotFoundException">GameData 找不到</exception>
    void Prepare(string module, string @class, int offset, nint hookFn);

    /// <summary>
    ///     准备Hook
    /// </summary>
    /// <param name="vTable">vtable地址</param>
    /// <param name="offset">offset</param>
    /// <param name="hookFn">UnmanagedCallersOnly 静态函数</param>
    /// <exception cref="EntryPointNotFoundException">VTable无效</exception>
    /// <exception cref="KeyNotFoundException">GameData 找不到</exception>
    void Prepare(nint vTable, int offset, nint hookFn);
}
