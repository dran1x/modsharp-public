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

using System.Runtime.InteropServices;
using Sharp.Shared.Enums;
using Sharp.Shared.Types;
using Sharp.Shared.Utilities;

namespace Sharp.Core.Bridges.Forwards;

internal static class Entity
{
    public delegate void DelegateOnEntityCreated(nint entity);

    public delegate void DelegateOnEntityDeleted(nint entity);

    public delegate void DelegateOnEntitySpawned(nint entity);

    public delegate void DelegateOnEntityFollowed(nint entity, nint owner);

    public delegate EHookAction DelegateOnEntityFireOutput(nint entity, string output, nint activator, float delay);

    public delegate EHookAction DelegateOnEntityAcceptInput(nint entity,
        string                                                   input,
        in EntityVariant                                         variant,
        nint                                                     activator,
        nint                                                     caller);

    public static event DelegateOnEntityCreated?     OnEntityCreated;
    public static event DelegateOnEntityDeleted?     OnEntityDeleted;
    public static event DelegateOnEntityDeleted?     OnEntityDeletedPost;
    public static event DelegateOnEntitySpawned?     OnEntitySpawned;
    public static event DelegateOnEntityFollowed?    OnEntityFollowed;
    public static event DelegateOnEntityFireOutput?  OnEntityFireOutput;
    public static event DelegateOnEntityAcceptInput? OnEntityAcceptInput;

    // 这里拿的都是指针
    // 需要直接转成CBaseHandle

    [UnmanagedCallersOnly]
    public static void OnEntityCreatedExport(nint entity)
        => OnEntityCreated?.Invoke(entity);

    [UnmanagedCallersOnly]
    public static void OnEntityDeletedExport(nint entity)
    {
        OnEntityDeleted?.Invoke(entity);
        OnEntityDeletedPost?.Invoke(entity);
    }

    [UnmanagedCallersOnly]
    public static void OnEntitySpawnedExport(nint entity)
        => OnEntitySpawned?.Invoke(entity);

    [UnmanagedCallersOnly]
    public static void OnEntityFollowedExport(nint entity, nint owner /* maybe be null */)
        => OnEntityFollowed?.Invoke(entity, owner);

    [UnmanagedCallersOnly]
    public static unsafe EHookAction OnEntityFireOutputExport(nint ptrCaller, sbyte* pOutput, nint ptrActivator, float delay)
    {
        if (OnEntityFireOutput is null)
        {
            return EHookAction.Ignored;
        }

        var output = Utils.ReadString(pOutput);

        return OnEntityFireOutput(ptrCaller, output, ptrActivator, delay);
    }

    [UnmanagedCallersOnly]
    public static unsafe EHookAction OnEntityAcceptInputExport(nint ptrEntity,
        sbyte*                                                      pInput,
        EntityVariant*                                              ptrVariant,
        nint                                                        ptrActivator,
        nint                                                        ptrCaller)
    {
        if (OnEntityAcceptInput is null)
        {
            return EHookAction.Ignored;
        }

        var input = Utils.ReadString(pInput);

        var variant = *ptrVariant;

        return OnEntityAcceptInput(ptrEntity, input, in variant, ptrActivator, ptrCaller);
    }
}
