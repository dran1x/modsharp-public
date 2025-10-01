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

namespace Sharp.Shared.Enums;

public enum HitGroupType : uint
{
    /// <summary>
    ///     无效
    /// </summary>
    Invalid = uint.MaxValue,

    /// <summary>
    ///     通用
    /// </summary>
    Generic = 0,

    /// <summary>
    ///     脑瓜子
    /// </summary>
    Head,

    /// <summary>
    ///     胸
    /// </summary>
    Chest,

    /// <summary>
    ///     肚子
    /// </summary>
    Stomach,

    /// <summary>
    ///     左手
    /// </summary>
    LeftArm,

    /// <summary>
    ///     右手
    /// </summary>
    RightArm,

    /// <summary>
    ///     左腿
    /// </summary>
    LeftLeg,

    /// <summary>
    ///     右腿
    /// </summary>
    RightLeg,

    /// <summary>
    ///     脖子
    /// </summary>
    Neck,

    /// <summary>
    ///     不知道是哪里
    /// </summary>
    Unknown9,

    /// <summary>
    ///     不知道什么J8地方
    /// </summary>
    Gear,

    /// <summary>
    ///     特殊部位
    /// </summary>
    Special,
}
