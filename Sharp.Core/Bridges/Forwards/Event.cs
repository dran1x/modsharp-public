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

namespace Sharp.Core.Bridges.Forwards;

internal static class Event
{
    /// <summary>
    ///     Hook eventManager->FireEvent
    /// </summary>
    /// <param name="event">Event指针</param>
    /// <param name="serverOnly">True = 不发送给客户端</param>
    /// <returns>False = Block Event</returns>
    public delegate bool DelegateHookFireEvent(nint @event, ref bool serverOnly);

    public delegate void DelegateFireGameEvent(nint @event);

    public static event DelegateHookFireEvent? HookFireEvent;
    public static event DelegateFireGameEvent? FireGameEvent;

    [UnmanagedCallersOnly]
    public static unsafe bool HookFireEventExport(nint @event, bool* serverOnly)
    {
        if (HookFireEvent is null)
        {
            return true;
        }

        ref var svOnly = ref Unsafe.AsRef<bool>(serverOnly);
        var     allow  = HookFireEvent.Invoke(@event, ref svOnly);

        return allow;
    }

    [UnmanagedCallersOnly]
    public static void FireGameEventExport(nint @event)
        => FireGameEvent?.Invoke(@event);
}
