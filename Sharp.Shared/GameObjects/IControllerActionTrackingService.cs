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

[NetClass("CSPerRoundStats_t")]
public interface IPerRoundStats : ISchemaObject
{
    int Kills { get; set; }

    int Deaths { get; set; }

    int Assists { get; set; }

    int Damage { get; set; }

    int EquipmentValue { get; set; }

    int MoneySaved { get; set; }

    int KillReward { get; set; }

    int LiveTime { get; set; }

    int HeadShotKills { get; set; }

    int Objective { get; set; }

    int CashEarned { get; set; }

    int UtilityDamage { get; set; }

    int EnemiesFlashed { get; set; }
}

[NetClass("CSMatchStats_t")]
public interface IMatchStats : IPerRoundStats
{
    int Enemy5Ks { get; set; }

    int Enemy4Ks { get; set; }

    int Enemy3Ks { get; set; }

    int Enemy2Ks { get; set; }

    int EnemyKnifeKills { get; set; }

    int EnemyTaserKills { get; set; }

    int UtilityCount { get; set; }

    int UtilitySuccesses { get; set; }

    int UtilityEnemies { get; set; }

    int FlashCount { get; set; }

    int FlashSuccesses { get; set; }

    float HealthPointsRemovedTotal { get; set; }

    float HealthPointsDealtTotal { get; set; }

    int ShotsFiredTotal { get; set; }

    int ShotsOnTargetTotal { get; set; }

    int R1V1Count { get; set; }

    int R1V1Wins { get; set; }

    int R1V2Count { get; set; }

    int R1V2Wins { get; set; }

    int EntryCount { get; set; }
    int EntryWins  { get; set; }
}

[NetClass("CCSPlayerController_ActionTrackingServices")]
public interface IControllerActionTrackingService : IPlayerControllerComponent
{
    /// <summary>
    ///     当前回合击杀数
    /// </summary>
    int NumRoundKills { get; set; }

    /// <summary>
    ///     当前回合爆头数
    /// </summary>
    int NumRoundKillsHeadshots { get; set; }

    /// <summary>
    ///     当前回合造成的总伤害
    /// </summary>
    float TotalRoundDamageDealt { get; set; }

    /// <summary>
    ///     MatchStats实例
    /// </summary>
    IMatchStats GetMatchStats();
}
