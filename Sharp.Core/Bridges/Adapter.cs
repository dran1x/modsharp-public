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
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Sharp.Core.Utilities;
using Sharp.Shared.Types;
using Sharp.Shared.Types.Tier;

namespace Sharp.Core.Bridges;

[StructLayout(LayoutKind.Sequential)]
public unsafe struct ForwardItem
{
    public fixed sbyte Name[128];
    public       nint  Func;
    public       uint  InstallCount;

    public string NameString
    {
        get
        {
            fixed (sbyte* ptr = Name)
            {
                return new string(ptr);
            }
        }
    }
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct NativeItem
{
    public fixed sbyte Name[128];
    public       nint  Func;

    public string NameString
    {
        get
        {
            fixed (sbyte* ptr = Name)
            {
                return new string(ptr);
            }
        }
    }
}

internal static class Adapter
{
    internal static readonly Dictionary<string, nint> NativeFunctions = new (StringComparer.OrdinalIgnoreCase);

    public static unsafe bool Boot(nint natives, nint forwards)
    {
        ref var nativeVec = ref Unsafe.AsRef<CUtlVector<Pointer<NativeItem>>>(natives.ToPointer());

        foreach (var native in nativeVec)
        {
            InstallNative(ref native.AsRef());
        }

        ref var forwardVec = ref Unsafe.AsRef<CUtlVector<Pointer<ForwardItem>>>(forwards.ToPointer());

        foreach (var forward in forwardVec)
        {
            InstallForward(ref forward.AsRef());
        }

        return true;
    }

    private static bool InstallNative(ref NativeItem nativeItem)
    {
        var name = nativeItem.NameString;

        try
        {
            const string asmNameSpace = "Sharp.Core";

            var pos = name.LastIndexOf('.');

            var typeName = name[..pos]       ?? throw new Exception("Failed to parse native method namespace");
            var sMethod  = name[(pos + 1)..] ?? throw new Exception("Failed to parse native method name");

            var sType = $"{asmNameSpace}.Bridges.Natives.{typeName}";

            var type = AppDomain.CurrentDomain.GetAssemblies()
                                .Where(x => x.ToString().StartsWith(asmNameSpace))
                                .Select(x => x.GetType(sType))
                                .FirstOrDefault();

            if (type is null)
            {
                Printer.Error($"Failed to find type \"{sType}\"");

                return false;
            }

            if (type.GetField($"_{sMethod}", BindingFlags.Static | BindingFlags.NonPublic) is not { } field)
            {
                Printer.Error($"Failed to find method \"{sMethod}\" in type \"{sType}\"");

                return false;
            }

            field.SetValue(null, nativeItem.Func);

            // Console.WriteLine($"[CoreCLR] Success to bind native \"{sMethod}\" in type \"{sType}\" -> {name}");

            return NativeFunctions.TryAdd(name, nativeItem.Func);
        }
        catch (Exception ex)
        {
            Printer.Error("Failed to bind native.", ex);

            return false;
        }
    }

    private static bool InstallForward(ref ForwardItem forwardItem)
    {
        var name = forwardItem.NameString;

        if (string.IsNullOrEmpty(name))
        {
            return false;
        }

        try
        {
            const string asmNameSpace = "Sharp.Core";

            var pos = name.LastIndexOf('.');

            var typeName = name[..pos]       ?? throw new Exception("Failed to parse forward method namespace");
            var sMethod  = name[(pos + 1)..] ?? throw new Exception("Failed to parse forward method name");

            var sType = $"{asmNameSpace}.Bridges.Forwards.{typeName}";

            var type = AppDomain.CurrentDomain.GetAssemblies()
                                .Where(x => x.ToString().StartsWith(asmNameSpace))
                                .Select(x => x.GetType(sType))
                                .FirstOrDefault();

            if (type is null)
            {
                Printer.Error($"Failed to find type \"{sType}\"");

                return false;
            }

            if (type.GetMethod($"{sMethod}Export") is not { } method)
            {
                Printer.Error($"Failed to find method \"{sMethod}\" in type \"{sType}\"");

                return false;
            }

            forwardItem.Func = method.MethodHandle.GetFunctionPointer();

            return true;
        }
        catch (Exception ex)
        {
            Printer.Error("Failed to bind forward.", ex);

            return false;
        }
    }
}
