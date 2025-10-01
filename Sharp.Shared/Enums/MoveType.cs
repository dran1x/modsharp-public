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

public enum MoveType : byte
{
    /// <summary>
    ///     静止
    /// </summary>
    None,

    /// <summary>
    ///     Isometric
    /// </summary>
    Isometric,

    /// <summary>
    ///     行走状态 (玩家默认)
    /// </summary>
    Walk,

    /// <summary>
    ///     Fly
    /// </summary>
    Fly,

    /// <summary>
    ///     FlyGravity
    /// </summary>
    FlyGravity,

    /// <summary>
    ///     物理 (物理实体默认)
    /// </summary>
    VPhysics,

    /// <summary>
    ///     Push
    /// </summary>
    Push,

    /// <summary>
    ///     穿墙
    /// </summary>
    NoClip,

    /// <summary>
    ///     观察者
    /// </summary>
    Observer,

    /// <summary>
    ///     挂在梯子上
    /// </summary>
    Ladder,

    /// <summary>
    ///     自定义
    /// </summary>
    Custom,
}
