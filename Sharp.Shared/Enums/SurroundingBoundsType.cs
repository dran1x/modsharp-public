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

public enum SurroundingBoundsType : byte
{
    UseObbCollisionBounds             = 0x0,
    UseBestCollisionBounds            = 0x1,
    UseHitboxes                       = 0x2,
    UseSpecifiedBounds                = 0x3,
    UseGameCode                       = 0x4,
    UseRotationExpandedBounds         = 0x5,
    UseRotationExpandedOrientedBounds = 0x6,
    UseCollisionBoundsNeverVPhysics   = 0x7,
    UseRotationExpandedSequenceBounds = 0x8,
    SurroundingTypeBitCount           = 0x3,
}
