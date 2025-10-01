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

namespace Sharp.Core.Utilities;

internal static class ListExtension
{
    [Obsolete("Bad GC performance")]
    public static void ForEachCatchException<T>(this IReadOnlyCollection<T> list, Action<T> action, Action<Exception> ex)
    {
        foreach (var item in list)
        {
            try
            {
                action(item);
            }
            catch (Exception e)
            {
                ex(e);
            }
        }
    }

    [Obsolete("Bad GC performance")]
    public static void ForEachIgnore<T>(this IReadOnlyCollection<T> list, Action<T> action)
    {
        foreach (var item in list)
        {
            action(item);
        }
    }

    [Obsolete("Bad GC performance")]
    public static void ForEachCallback<T>(this List<T> list, Action<T> action, Action<Exception> ex)
    {
        // ReSharper disable once ForCanBeConvertedToForeach
        for (var i = 0; i < list.Count; i++)
        {
            try
            {
                action(list[i]);
            }
            catch (Exception e)
            {
                ex(e);
            }
        }
    }
}
