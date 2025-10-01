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

using Sharp.Shared.Attributes;
using Sharp.Shared.CStrike;

namespace Sharp.Shared.GameObjects;

[NetClass("CAttributeContainer")]
public interface IAttributeContainer : ISchemaObject
{
    /// <summary>
    ///     Econ物品
    /// </summary>
    IEconItemView Item { get; }
}

[NetClass("CEconItemView")]
public interface IEconItemView : ISchemaObject
{
    /// <summary>
    ///     ItemDefIndex
    /// </summary>
    ushort ItemDefinitionIndex { get; }

    /// <summary>
    ///     价值等级
    /// </summary>
    int Quality { get; }

    /// <summary>
    ///     物品等级
    /// </summary>
    uint Level { get; }

    /// <summary>
    ///     物品Id (对应Steam库存中的Id)
    /// </summary>
    ulong ItemId { get; }

    /// <summary>
    ///     物品Id (高位)
    /// </summary>
    uint ItemIdHigh { get; }

    /// <summary>
    ///     物品Id (低位)
    /// </summary>
    uint ItemIdLow { get; }

    /// <summary>
    ///     拥有者的AccountId
    /// </summary>
    uint AccountId { get; }

    string CustomName { get; set; }

    bool Initialized { get; }

    void SetItemDefinitionIndexLocal(ushort value);

    void SetItemIdLowLocal(uint value);

    void SetItemIdHighLocal(uint value);

    void SetAccountIdLocal(uint value);

    void SetQualityLocal(int value);

    void SetCustomNameLocal(string value);

    void SetCustomNameOverrideLocal(string value);

    void SetInitializedLocal(bool value);
}
