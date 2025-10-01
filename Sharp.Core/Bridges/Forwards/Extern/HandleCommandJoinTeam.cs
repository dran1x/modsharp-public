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
using Sharp.Shared.Types;

namespace Sharp.Core.Bridges.Forwards.Extern;

public static class HandleCommandJoinTeam
{
    public delegate HookReturnValue<bool> DelegateHandleCommandJoinTeamPre(nint ptrClient,
        nint                                                                    ptrController,
        ref int                                                                 team,
        ref bool                                                                queue,
        ref int                                                                 coachTeam);

    public delegate void DelegateHandleCommandJoinTeamPost(nint ptrClient,
        nint                                                    pController,
        int                                                     team,
        bool                                                    queue,
        int                                                     coachTeam,
        EHookAction                                             hookAction,
        bool                                                    returnValue);

    public static event DelegateHandleCommandJoinTeamPre? OnHandleCommandJoinTeamPre;

    public static event DelegateHandleCommandJoinTeamPost? OnHandleCommandJoinTeamPost;

    [UnmanagedCallersOnly]
    public static unsafe EHookAction OnHandleCommandJoinTeamPreExport(nint ptrClient,
        nint                                                               ptrController,
        int*                                                               pTeam,
        bool*                                                              pQueue,
        int*                                                               pCoachTeam,
        bool*                                                              returnValue)
    {
        ref var team      = ref Unsafe.AsRef<int>(pTeam);
        ref var queue     = ref Unsafe.AsRef<bool>(pQueue);
        ref var coachTeam = ref Unsafe.AsRef<int>(pCoachTeam);

        if (OnHandleCommandJoinTeamPre is null)
        {
            return EHookAction.Ignored;
        }

        var action = OnHandleCommandJoinTeamPre.Invoke(ptrClient, ptrController, ref team, ref queue, ref coachTeam);

        if (action.Action == EHookAction.SkipCallReturnOverride)
        {
            *returnValue = action.ReturnValue;

            return EHookAction.SkipCallReturnOverride;
        }

        if (action.Action == EHookAction.Ignored)
        {
            return EHookAction.Ignored;
        }

        return action.Action;
    }

    [UnmanagedCallersOnly]
    public static void OnHandleCommandJoinTeamPostExport(nint ptrClient,
        nint                                                  ptrController,
        int                                                   team,
        byte                                                  queue,
        int                                                   coachTeam,
        EHookAction                                           hookAction,
        byte                                                  returnValue)
    {
        OnHandleCommandJoinTeamPost?.Invoke(ptrClient,
                                            ptrController,
                                            team,
                                            queue != 0,
                                            coachTeam,
                                            hookAction,
                                            returnValue != 0);
    }
}
