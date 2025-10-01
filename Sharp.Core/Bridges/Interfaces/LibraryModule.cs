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
using Sharp.Core.CStrike;
using Sharp.Generator;
using Sharp.Shared;

namespace Sharp.Core.Bridges.Interfaces;

[NativeVirtualObject(HasDestructors = true)]
internal partial class LibraryModule : NativeObject, ILibraryModule
{
#region Native

    public partial nint FindPatternEx(string svPattern, nint startAddress = 0);

    public partial nint GetVirtualTableByNameEx(string svTableName, bool decorated = false);

    public partial nint GetFunctionByNameEx(string svFunctionName);

    public partial nint FindInterfaceEx(string svInterfaceName);

    public partial nint FindPatternMultiEx(string svPattern, nint count);

    public partial nint FindStringEx(string str);

    public partial nint FindPtrEx(nint ptr);

#endregion

    public IntPtr FindPattern(string pattern, nint startAddress = 0)
        => FindPatternEx(pattern);

    public IntPtr GetVirtualTableByName(string tableName, bool decorated = false)
        => GetVirtualTableByNameEx(tableName, decorated);

    public IntPtr GetFunctionByName(string functionName)
        => GetFunctionByNameEx(functionName);

    public IntPtr FindPatternExactly(string pattern)
    {
        var results = FindPatternMulti(pattern);

        return results.Count switch
        {
            0   => 0,
            > 1 => throw new InvalidOperationException("Found more than one result for the pattern."),
            _   => results[0],
        };
    }

    public IntPtr FindInterface(string interfaceName)
        => FindInterfaceEx(interfaceName);

    public unsafe List<nint> FindPatternMulti(string pattern)
    {
        var count  = 0;
        var result = FindPatternMultiEx(pattern, (nint) (&count));

        if (count == 0)
        {
            return [];
        }

        var results = new List<IntPtr>();

        for (var i = 0; i < count; i++)
        {
            results.Add(*(nint*) (result + (sizeof(nint) * i)));
        }

        return results;
    }

    public nint FindString(string str)
        => FindStringEx(str);

    public nint FindPtr(nint ptr)
        => FindPtrEx(ptr);
}
