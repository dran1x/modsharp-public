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
using Sharp.Core.Helpers;
using Sharp.Shared.Calls;
using Sharp.Shared.GameEntities;

namespace Sharp.Core.VirtualCalls;

internal unsafe interface IBaseEntityVirtualCall : IVirtualCall<IBaseEntity>
{
    void* GetDynamicBinding(nint instance, void** result);
}

internal partial class BaseEntityVirtualCall
{
    private static IBaseEntityVirtualCall? _instance;
    private static IBaseEntityVirtualCall  Instance => _instance ??= new BaseEntityVirtualCall(CoreGameData.Core);

    public static unsafe void* GetDynamicBinding(nint instance)
    {
        void* temp;

        if (OperatingSystem.IsWindows())
        {
            Instance.GetDynamicBinding(instance, &temp);

            return temp;
        }

        return Instance.GetDynamicBinding(instance, &temp);
    }
}
