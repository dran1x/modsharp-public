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

using Sharp.Shared.CStrike;
using Sharp.Shared.Utilities;

namespace Sharp.Core.CStrike;

internal abstract class EntitySubclassVData : SchemaObject, IEntitySubclassVData
{
    protected EntitySubclassVData(nint ptr) : base(ptr)
    {
    }

    public string Key => _this.Add(0x10).Dereference().GetString(0);

    public string Classname => _this.Add(0x18).Dereference().GetString(0);
}
