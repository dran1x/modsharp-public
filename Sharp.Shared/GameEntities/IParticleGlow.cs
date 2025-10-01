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
using Sharp.Shared.Types;

namespace Sharp.Shared.GameEntities;

[NetClass("CEnvParticleGlow")]
public interface IParticleGlow : IBaseParticle
{
    /// <summary>
    ///     m_flAlphaScale
    /// </summary>
    float AlphaScale { get; set; }

    /// <summary>
    ///     m_flRadiusScale
    /// </summary>
    float RadiusScale { get; set; }

    /// <summary>
    ///     m_flSelfIllumScale
    /// </summary>
    float SelfIllumScale { get; set; }

    /// <summary>
    ///     m_ColorTint
    /// </summary>
    Color32 ColorTint { get; set; }
}
