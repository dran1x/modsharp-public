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

public interface IDetourHook : IRuntimeNativeHook, IDisposable
{
    /// <summary>
    ///     准备Hook
    /// </summary>
    /// <param name="gamedata">gamedata key</param>
    /// <param name="hookFn">UnmanagedCallersOnly 静态函数</param>
    /// <exception cref="EntryPointNotFoundException">GameData找不到/空指针/无效字段</exception>
    /// <exception cref="KeyNotFoundException">GameData 找不到</exception>
    void Prepare(string gamedata, nint hookFn);

    /// <summary>
    ///     准备Hook <br />
    ///     <remarks>
    ///         需要自行转换为 nint <br />
    ///     </remarks>
    /// </summary>
    /// <example>
    ///     <code>
    ///         (nint) (delegate* unmanaged&lt;void&gt;) &amp;Test
    ///  </code>
    /// </example>
    /// <param name="pTargetFn">原生函数地址</param>
    /// <param name="hookFn"> UnmanagedCallersOnly 静态函数</param>
    void Prepare(nint pTargetFn, nint hookFn);
}
