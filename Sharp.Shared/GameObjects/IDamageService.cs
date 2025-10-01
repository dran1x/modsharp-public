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
using Sharp.Shared.Units;

namespace Sharp.Shared.GameObjects;

public enum KillTypes : byte
{
    None     = 0x0,
    Default  = 0x1,
    Headshot = 0x2,
    Blast    = 0x3,
    Burn     = 0x4,
    Slash    = 0x5,
    Shock    = 0x6,
}

[NetClass("CDamageRecord")]
public interface IDamageRecord : INativeObject
{
    /// <summary>
    ///     Killer SteamId
    /// </summary>
    SteamID DamagerXuid { get; }

    /// <summary>
    ///     Victim SteamId
    /// </summary>
    SteamID RecipientXuid { get; }

    /// <summary>
    ///     子弹上海
    /// </summary>
    float BulletsDamage { get; }

    /// <summary>
    ///     伤害
    /// </summary>
    float Damage { get; }

    /// <summary>
    ///     实际掉血
    /// </summary>
    float ActualHealthRemoved { get; }

    /// <summary>
    ///     命中数
    /// </summary>
    int NumHits { get; }

    /// <summary>
    ///     击杀类型
    /// </summary>
    KillTypes KillType { get; }
}

[NetClass("CCSPlayerController_DamageServices")]
public interface IDamageService : IPlayerControllerComponent
{
    /// <summary>
    ///     伤害统计列表
    /// </summary>
    ISchemaEmbeddedList<IDamageRecord> GetDamageList();
}
