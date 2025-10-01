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
using Sharp.Shared.Types;

namespace Sharp.Shared.GameObjects;

[NetClass("CGlowProperty")]
public interface IGlowProperty : IEntityObject
{
    /// <summary>
    ///     m_iGlowType
    /// </summary>
    int GlowType { get; set; }

    /// <summary>
    ///     m_iGlowTeam
    /// </summary>
    int GlowTeam { get; set; }

    /// <summary>
    ///     m_bEligibleForScreenHighlight
    /// </summary>
    bool EligibleForScreenHighlight { get; set; }

    /// <summary>
    ///     m_flGlowTime
    /// </summary>
    float GlowTime { get; set; }

    /// <summary>
    ///     m_flGlowStartTime
    /// </summary>
    float GlowStartTime { get; set; }

    /// <summary>
    ///     Glowing
    /// </summary>
    bool Glowing { get; set; }

    /// <summary>
    ///     m_bFlashing
    /// </summary>
    bool Flashing { get; set; }

    /// <summary>
    ///     m_nGlowRange
    /// </summary>
    int GlowRangeMax { get; set; }

    /// <summary>
    ///     m_nGlowRangeMin
    /// </summary>
    int GlowRangeMin { get; set; }

    /// <summary>
    ///     m_fGlowColor
    /// </summary>
    Vector GlowColor { get; set; }

    /// <summary>
    ///     m_glowColorOverride
    /// </summary>
    Color32 GlowColorOverride { get; set; }
}
