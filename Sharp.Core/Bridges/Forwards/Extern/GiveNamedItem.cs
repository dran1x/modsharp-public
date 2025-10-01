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

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Sharp.Shared.Enums;
using Sharp.Shared.GameEntities;
using Sharp.Shared.Types;
using Sharp.Shared.Utilities;

namespace Sharp.Core.Bridges.Forwards.Extern;

public static class GiveNamedItem
{
    public delegate HookReturnValue<IBaseWeapon> DelegateGiveNamedItemPre(nint ptrClient,
        nint                                                                   ptrController,
        nint                                                                   ptrPawn,
        ref string                                                             classname,
        ref bool                                                               ignoredCEconItemView);

    public delegate void DelegateGiveNamedItemPost(nint ptrClient,
        nint                                            ptrController,
        nint                                            ptrPawn,
        string                                          classname,
        bool                                            ignoredCEconItemView,
        EHookAction                                     hookAction,
        nint                                            returnValue);

    public delegate HookReturnValue<EAcquireResult> DelegateCanAcquirePre(nint ptrClient,
        nint                                                                   ptrController,
        nint                                                                   ptrPawn,
        ushort                                                                 itemIndex,
        EAcquireMethod                                                         acquireMethod,
        EAcquireResult                                                         defaultResult);

    public delegate void DelegateGiveGloveItemPost(nint ptrClient, nint ptrController, nint ptrPawn);

    public static event DelegateGiveNamedItemPre? OnGiveNamedItemPre;

    public static event DelegateGiveNamedItemPost? OnGiveNamedItemPost;

    public static event DelegateCanAcquirePre? OnCanAcquirePre;

    public static event DelegateGiveGloveItemPost? OnGiveGloveItemPost;

    [UnmanagedCallersOnly]
    public static unsafe EHookAction OnGiveNamedItemPreExport(nint ptrClient,
        nint                                                       ptrController,
        nint                                                       ptrPawn,
        sbyte*                                                     ptrClassname,
        bool*                                                      ignoredCEconItemView)
    {
        var     classname = new string(ptrClassname);
        ref var ignore    = ref Unsafe.AsRef<bool>(ignoredCEconItemView);

        if (OnGiveNamedItemPre is null)
        {
            return EHookAction.Ignored;
        }

        var action = OnGiveNamedItemPre.Invoke(ptrClient, ptrController, ptrPawn, ref classname, ref ignore);

        if (action.Action == EHookAction.SkipCallReturnOverride)
        {
            return EHookAction.SkipCallReturnOverride;
        }

        if (action.Action == EHookAction.Ignored)
        {
            return EHookAction.Ignored;
        }

        Utils.WriteString(ptrClassname, 32, classname);

        return action.Action;
    }

    [UnmanagedCallersOnly]
    public static unsafe void OnGiveNamedItemPostExport(nint ptrClient,
        nint                                                 ptrController,
        nint                                                 ptrPawn,
        sbyte*                                               ptrClassname,
        byte                                                 ignoredCEconItemView,
        EHookAction                                          hookAction,
        nint                                                 returnValue)
    {
        var classname = Utils.ReadString(ptrClassname);
        var ignore    = ignoredCEconItemView != 0;

        OnGiveNamedItemPost?.Invoke(ptrClient, ptrController, ptrPawn, classname, ignore, hookAction, returnValue);
    }

    [UnmanagedCallersOnly]
    public static void OnGiveGloveItemPostExport(nint ptrClient, nint ptrController, nint ptrPawn)
        => OnGiveGloveItemPost?.Invoke(ptrClient, ptrController, ptrPawn);

    [UnmanagedCallersOnly]
    public static unsafe EHookAction OnCanAcquirePreExport(nint ptrClient,
        nint                                                    ptrController,
        nint                                                    ptrPawn,
        ushort                                                  itemIndex,
        EAcquireMethod                                          method,
        EAcquireResult*                                         acquireResult)
    {
        if (OnCanAcquirePre is null)
        {
            return EHookAction.Ignored;
        }

        var defaultCanAcquire = *acquireResult;

        var action = OnCanAcquirePre.Invoke(ptrClient, ptrController, ptrPawn, itemIndex, method, defaultCanAcquire);

        *acquireResult = action.ReturnValue;

        return action.Action;
    }
}
