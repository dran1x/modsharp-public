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

public enum ParticleAttachmentType : uint
{
    Invalid = uint.MaxValue,

    /// <summary>
    ///     Create at AbsOrigin, but don't follow
    /// </summary>
    AbsOrigin = 0,

    /// <summary>
    ///     Create at AbsOrigin, and update to follow the entity
    /// </summary>
    AbsOriginFollow,

    /// <summary>
    ///     Create at a custom origin, but don't follow
    /// </summary>
    CustomOrigin,

    /// <summary>
    ///     Create at a custom origin, follow relative position to specified entity
    /// </summary>
    CustomOriginFollow,

    /// <summary>
    ///     Create on attachment point, but don't follow
    /// </summary>
    Point,

    /// <summary>
    ///     Create on attachment point, and update to follow the entity
    /// </summary>
    PointFollow,

    /// <summary>
    ///     Create on eyes of the attached entity, and update to follow the entity
    /// </summary>
    EyesFollow,

    /// <summary>
    ///     Create at the top of the entity's BBox
    /// </summary>
    OverheadFollow,

    /// <summary>
    ///     Used for control points that don't attach to an entity
    /// </summary>
    WorldOrigin,

    /// <summary>
    ///     Create at the root bone of the entity, and update to follow
    /// </summary>
    RootBoneFollow,

    RenderOriginFollow,

    MainView,

    WaterWake,

    CenterFollow,

    CustomGameState,

    HealthBar,
}
