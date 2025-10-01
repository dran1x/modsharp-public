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

using Sharp.Core.Objects;
using Sharp.Shared.Enums;
using Sharp.Shared.GameEntities;
using Sharp.Shared.GameEvents;
using Sharp.Shared.Units;

namespace Sharp.Core.GameEvents;

[GameEventName("player_hurt")]
internal partial class EventPlayerHurt : GameEvent, IEventPlayerHurt
{
    [GameEventProperty("userid", true)]
    private IPlayerController? _victimController;

    [GameEventProperty("userid", true)]
    private IPlayerPawn? _victimPawn;

    [GameEventProperty("userid")]
    private UserID? _victimUserId;

    [GameEventProperty("attacker", true)]
    private IPlayerController? _killerController;

    [GameEventProperty("attacker", true)]
    private IPlayerPawn? _killerPawn;

    [GameEventProperty("attacker")]
    private UserID? _killerUserId;

    [GameEventProperty("health")]
    private int? _health;

    [GameEventProperty("armor")]
    private int? _armor;

    [GameEventProperty("weapon")]
    private string? _weapon;

    [GameEventProperty("dmg_health")]
    private int? _damage;

    [GameEventProperty("dmg_armor")]
    private int? _armorDamage;

    [GameEventProperty("hitgroup")]
    private HitGroupType? _hitGroup;
}
