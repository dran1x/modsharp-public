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
using Sharp.Shared.Enums;
using Sharp.Shared.GameObjects;
using Sharp.Shared.Types;

namespace Sharp.Shared.GameEntities;

[NetClass("CCSPlayerPawn")]
public interface IPlayerPawn : IBaseCombatCharacter
{
    /// <summary>
    ///     打印对应的消息 (HudMsg)
    /// </summary>
    void Print(HudPrintChannel channel,
        string                 message,
        string?                param1 = null,
        string?                param2 = null,
        string?                param3 = null,
        string?                param4 = null);

    /// <summary>
    ///     是否是玩家或者Observer
    /// </summary>
    bool IsPlayer();

    /// <summary>
    ///     处死
    /// </summary>
    /// <param name="explode">原地爆炸</param>
    void Slay(bool explode = false);

    /// <summary>
    ///     取得当前PlayerPawn对应的Controller
    /// </summary>
    IPlayerController? GetController();

    /// <summary>
    ///     m_hOriginalController
    /// </summary>
    IPlayerController? GetOriginalController();

    /// <summary>
    ///     自动识别存活状态就拿Controller, 否则Original
    /// </summary>
    /// <returns></returns>
    IPlayerController? GetControllerAuto();

    /// <summary>
    ///     发枪
    /// </summary>
    IBaseWeapon? GiveNamedItem(string weapon);

    /// <summary>
    ///     发枪
    /// </summary>
    IBaseWeapon? GiveNamedItem(EconItemId item);

    /// <summary>
    ///     获取当前手上的武器
    /// </summary>
    /// <returns></returns>
    IBaseWeapon? GetActiveWeapon();

    /// <summary>
    ///     通过槽位获取身上的武器
    /// </summary>
    IBaseWeapon? GetWeaponBySlot(GearSlot slot, int position = -1);

    /// <summary>
    ///     移除武器并立即销毁
    /// </summary>
    void RemovePlayerItem(IBaseWeapon item);

    /// <summary>
    ///     移除所有物品
    /// </summary>
    /// <param name="removeSuit">包括护甲</param>
    void RemoveAllItems(bool removeSuit = false);

    /// <summary>
    ///     强制丢掉武器
    /// </summary>
    void DropWeapon(IBaseWeapon item);

    /// <summary>
    ///     切换武器/道具
    /// </summary>
    bool SelectItem(IBaseWeapon item);

    /// <summary>
    ///     分离武器
    /// </summary>
    bool DetachWeapon(IBaseWeapon item);

    /// <summary>
    ///     强制切换武器或空手 (无视一切检测/条件判断)
    /// </summary>
    void SwitchWeapon(IBaseWeapon? weapon);

    /// <summary>
    ///     发手套
    /// </summary>
    void GiveGloves(int itemDefIndex, int prefab, float wear, int seed);

    /// <summary>
    ///     发手套
    /// </summary>
    void GiveGloves(EconGlovesId id, int prefab, float wear, int seed);

    /// <summary>
    ///     👀角度
    /// </summary>
    Vector GetEyeAngles();

    /// <summary>
    ///     👀位置
    /// </summary>
    Vector GetEyePosition();

    /// <summary>
    ///     瞬态更换队伍 <br />
    ///     <remarks>直接修改m_iTeamNum的值并且不发送网络消息</remarks>
    /// </summary>
    void TransientChangeTeam(CStrikeTeam team);

    /// <summary>
    ///     瞬态修改VelocityModifier,不发送网络消息
    /// </summary>
    void TransientChangeVelocityModifier(float velocityModifier);

    /// <summary>
    ///     ItemService实例
    /// </summary>
    IItemService? GetItemService();

    /// <summary>
    ///     MoveService实例
    /// </summary>
    IMovementService? GetMovementService();

    /// <summary>
    ///     WeaponService实例
    /// </summary>
    IWeaponService? GetWeaponService();

    /// <summary>
    ///     CameraService实例
    /// </summary>
    ICameraService? GetCameraService();

    /// <summary>
    ///     ObserverService实例
    /// </summary>
    IObserverService? GetObserverService();

    /// <summary>
    ///     只给当前玩家播放本地音频
    /// </summary>
    SoundOpEventGuid EmitSoundClient(string sound, float? volume = null);

    /// <summary>
    ///     Glove Econ
    /// </summary>
    IEconItemView GetEconGloves();

    /// <summary>
    ///     护甲值
    /// </summary>
    int ArmorValue { get; set; }

    /// <summary>
    ///     闪光Alpha
    /// </summary>
    float FlashMaxAlpha { get; set; }

    /// <summary>
    ///     闪光持续时间
    /// </summary>
    float FlashDuration { get; set; }

    /// <summary>
    ///     PlayerState
    /// </summary>
    PlayerState State { get; set; }

    /// <summary>
    ///     m_iNumSpawns
    /// </summary>
    int NumSpawns { get; }

    /// <summary>
    ///     m_fMolotovDamageTime
    /// </summary>
    float MolotovDamageTime { get; set; }

    /// <summary>
    ///     m_flHealthShotBoostExpirationTime
    /// </summary>
    float HealthShotBoostExpirationTime { get; set; }

    /// <summary>
    ///     m_flVelocityModifier
    /// </summary>
    float VelocityModifier { get; set; }

    /// <summary>
    ///     m_iShotsFired
    /// </summary>
    int ShotsFired { get; set; }

    /// <summary>
    ///     m_flFlinchStack
    /// </summary>
    float FlinchStack { get; set; }

    /// <summary>
    ///     m_flProgressBarStartTime
    /// </summary>
    float ProgressBarStartTime { get; set; }

    /// <summary>
    ///     m_iProgressBarDuration
    /// </summary>
    int ProgressBarDuration { get; set; }

    /// <summary>
    ///     m_iHideHUD
    /// </summary>
    uint HideHud { get; set; }

    /// <summary>
    ///     m_fTimeLastHurt
    /// </summary>
    float TimeLastHurt { get; set; }

    /// <summary>
    ///     m_flDeathTime
    /// </summary>
    float DeathTime { get; set; }

    /// <summary>
    ///     m_fNextSuicideTime
    /// </summary>
    float NextSuicideTime { get; set; }

    /// <summary>
    ///     m_bRespawning
    /// </summary>
    bool Respawning { get; }

    /// <summary>
    ///     m_fNextRadarUpdateTime
    /// </summary>
    float NextRadarUpdateTime { get; set; }

    /// <summary>
    ///     m_bInBuyZone
    /// </summary>
    bool InBuyZone { get; }

    /// <summary>
    ///     m_bInHostageRescueZone
    /// </summary>
    bool InHostageRescueZone { get; }

    /// <summary>
    ///     m_bInBombZone
    /// </summary>
    bool InBombZone { get; }

    /// <summary>
    ///     m_bIsBuyMenuOpen
    /// </summary>
    bool IsBuyMenuOpen { get; }

    /// <summary>
    ///     m_bLeftHanded
    /// </summary>
    bool LeftHanded { get; }

    /// <summary>
    ///     m_fSwitchedHandednessTime
    /// </summary>
    float SwitchedHandednessTime { get; set; }

    /// <summary>
    ///     m_vecTotalBulletForce
    /// </summary>
    Vector TotalBulletForce { get; set; }

    /// <summary>
    ///     m_iDeathFlags
    /// </summary>
    int DeathFlags { get; set; }

    /// <summary>
    ///     m_bWaitForNoAttack
    /// </summary>
    bool WaitForNoAttack { get; set; }

    /// <summary>
    ///     m_aimPunchAngle
    /// </summary>
    Vector AimPunchAngle { get; set; }

    /// <summary>
    ///     m_aimPunchAngleVel
    /// </summary>
    Vector AimPunchAngleVelocity { get; set; }

    /// <summary>
    ///     m_aimPunchTickBase
    /// </summary>
    int AimPunchTickBase { get; set; }

    /// <summary>
    ///     m_aimPunchTickFraction
    /// </summary>
    float AimPunchTickFraction { get; set; }

    /// <summary>
    ///     m_aimPunchCache
    /// </summary>
    ISchemaList<Vector> GetAimPunchCache();
}
