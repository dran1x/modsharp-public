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

using System;
using Sharp.Core.Bridges.Natives;
using Sharp.Core.CStrike;
using Sharp.Core.GameObjects;
using Sharp.Core.Utilities;
using Sharp.Generator;
using Sharp.Shared;
using Sharp.Shared.CStrike;
using Sharp.Shared.Enums;
using Sharp.Shared.GameEntities;
using Sharp.Shared.Types;

namespace Sharp.Core.GameEntities;

internal partial class PlayerPawn : BaseCombatCharacter, IPlayerPawn
{
    public void Print(HudPrintChannel channel,
        string                        message,
        string?                       param1 = null,
        string?                       param2 = null,
        string?                       param3 = null,
        string?                       param4 = null)
        => Player.PawnPrint(_this,
                            channel,
                            channel is HudPrintChannel.Chat && message[0] != '#' ? $" {message}" : message,
                            param1,
                            param2,
                            param3,
                            param4);

    public bool IsPlayer()
        => Player.PawnIsPlayer(_this);

    public void Slay(bool explode = false)
        => Player.PawnSlay(_this, explode);

    public IBaseWeapon? GiveNamedItem(string weapon)
        => BaseWeapon.Create(Player.PawnGiveNamedItem(_this, weapon));

    public IBaseWeapon? GiveNamedItem(EconItemId id)
    {
        if (!Enum.IsDefined(id) || !SharedGameObject.EconItemDefinitionsById.TryGetValue((ushort) id, out var value))
        {
            throw new InvalidOperationException($"Invalid ItemId {id}");
        }

        return GiveNamedItem(value.DefinitionName);
    }

    public IPlayerController? GetController()
        => PlayerController.Create(Player.PawnGetController(_this));

    public IPlayerController? GetOriginalController()
        => PlayerController.Create(OriginalControllerHandle.GetEntityPtr());

    public IPlayerController? GetControllerAuto()
        => IsAlive ? GetController() : GetOriginalController();

    public IBaseWeapon? GetActiveWeapon()
        => BaseWeapon.Create(Player.PawnGetActiveWeapon(_this));

    public IBaseWeapon? GetWeaponBySlot(GearSlot slot, int position = -1)
        => BaseWeapon.Create(Player.PawnGetWeaponBySlot(_this, slot, position));

    public void RemovePlayerItem(IBaseWeapon item)
        => Player.PawnRemovePlayerItem(_this, item.GetAbsPtr());

    public void RemoveAllItems(bool removeSuit = false)
        => Player.PawnRemoveAllItems(_this, removeSuit);

    public void DropWeapon(IBaseWeapon item)
        => Player.PawnDropWeapon(_this, item.GetAbsPtr());

    public bool SelectItem(IBaseWeapon item)
        => Player.PawnSelectItem(_this, item.GetAbsPtr());

    public bool DetachWeapon(IBaseWeapon item)
        => Player.PawnDetachWeapon(_this, item.GetAbsPtr());

    public void SwitchWeapon(IBaseWeapon? weapon)
        => Player.PawnSwitchWeapon(_this, weapon?.GetAbsPtr() ?? nint.Zero);

    public void GiveGloves(int itemDefIndex, int prefab, float wear, int seed)
        => Player.PawnGiveGloves(_this, itemDefIndex, prefab, wear, seed);

    public void GiveGloves(EconGlovesId id, int prefab, float wear, int seed)
    {
        if (!Enum.IsDefined(id))
        {
            Bridges.Natives.Core.LogWarning($"[PlayerPawn::GiveGloves] Invalid GlovesId {id}");

            return;
        }

        Player.PawnGiveGloves(_this, (int) id, prefab, wear, seed);
    }

    public void TransientChangeTeam(CStrikeTeam team)
        => SetTeamLocal(team);

    public void TransientChangeVelocityModifier(float velocityModifier)
        => SetVelocityModifierLocal(velocityModifier);

    public unsafe Vector GetEyeAngles()
        => *Entity.GetEyeAngles(_this);

    public unsafe Vector GetEyePosition()
        => *Entity.GetEyePosition(_this);

    public unsafe SoundOpEventGuid EmitSoundClient(string sound, float? volume = null)
        => Player.PawnEmitSoundClient(_this, sound, &volume);

#region Schemas

    [NativeSchemaField("CCSPlayerPawn", "m_ArmorValue", typeof(int))]
    private partial SchemaField GetArmorValueField();

    [NativeSchemaField("CCSPlayerPawnBase", "m_flFlashMaxAlpha", typeof(float))]
    private partial SchemaField GetFlashMaxAlphaField();

    [NativeSchemaField("CCSPlayerPawnBase", "m_flFlashDuration", typeof(float))]
    private partial SchemaField GetFlashDurationField();

    [NativeSchemaField("CCSPlayerPawnBase", "m_iPlayerState", typeof(PlayerState))]
    private partial SchemaField GetStateField();

    [NativeSchemaField("CCSPlayerPawnBase", "m_iNumSpawns", typeof(int))]
    private partial SchemaField GetNumSpawnsField();

    [NativeSchemaField("CCSPlayerPawn", "m_fMolotovDamageTime", typeof(float))]
    private partial SchemaField GetMolotovDamageTimeField();

    [NativeSchemaField("CBasePlayerPawn", "m_pItemServices", typeof(ItemService))]
    private partial SchemaField GetItemServiceField();

    [NativeSchemaField("CBasePlayerPawn", "m_pMovementServices", typeof(MovementService))]
    private partial SchemaField GetMovementServiceField();

    [NativeSchemaField("CBasePlayerPawn", "m_pWeaponServices", typeof(WeaponService))]
    private partial SchemaField GetWeaponServiceField();

    [NativeSchemaField("CBasePlayerPawn", "m_pCameraServices", typeof(CameraService))]
    private partial SchemaField GetCameraServiceField();

    [NativeSchemaField("CBasePlayerPawn", "m_pObserverServices", typeof(ObserverService))]
    private partial SchemaField GetObserverServiceField();

    [NativeSchemaField("CCSPlayerPawnBase", "m_hOriginalController", typeof(CEntityHandle<IPlayerController>))]
    private partial SchemaField GetOriginalControllerHandleField();

    [NativeSchemaField("CCSPlayerPawn", "m_flHealthShotBoostExpirationTime", typeof(float))]
    private partial SchemaField GetHealthShotBoostExpirationTimeField();

    [NativeSchemaField("CCSPlayerPawn", "m_EconGloves", typeof(EconItemView), InlineObject = true)]
    private partial SchemaField GetEconGlovesField();

    [NativeSchemaField("CCSPlayerPawn", "m_flVelocityModifier", typeof(float))]
    private partial SchemaField GetVelocityModifierField();

    [NativeSchemaField("CCSPlayerPawn", "m_iShotsFired", typeof(int))]
    private partial SchemaField GetShotsFiredField();

    [NativeSchemaField("CCSPlayerPawn", "m_flFlinchStack", typeof(float))]
    private partial SchemaField GetFlinchStackField();

    [NativeSchemaField("CCSPlayerPawnBase", "m_flProgressBarStartTime", typeof(float))]
    private partial SchemaField GetProgressBarStartTimeField();

    [NativeSchemaField("CCSPlayerPawnBase", "m_iProgressBarDuration", typeof(int))]
    private partial SchemaField GetProgressBarDurationField();

    [NativeSchemaField("CBasePlayerPawn", "m_iHideHUD", typeof(uint))]
    private partial SchemaField GetHideHudField();

    [NativeSchemaField("CBasePlayerPawn", "m_fTimeLastHurt", typeof(float))]
    private partial SchemaField GetTimeLastHurtField();

    [NativeSchemaField("CBasePlayerPawn", "m_flDeathTime", typeof(float))]
    private partial SchemaField GetDeathTimeField();

    [NativeSchemaField("CBasePlayerPawn", "m_fNextSuicideTime", typeof(float))]
    private partial SchemaField GetNextSuicideTimeField();

    [NativeSchemaField("CCSPlayerPawnBase", "m_bRespawning", typeof(bool))]
    private partial SchemaField GetRespawningField();

    [NativeSchemaField("CCSPlayerPawnBase", "m_fNextRadarUpdateTime", typeof(float))]
    private partial SchemaField GetNextRadarUpdateTimeField();

    [NativeSchemaField("CCSPlayerPawn", "m_bInBuyZone", typeof(bool))]
    private partial SchemaField GetInBuyZoneField();

    [NativeSchemaField("CCSPlayerPawn", "m_bInHostageRescueZone", typeof(bool))]
    private partial SchemaField GetInHostageRescueZoneField();

    [NativeSchemaField("CCSPlayerPawn", "m_bInBombZone", typeof(bool))]
    private partial SchemaField GetInBombZoneField();

    [NativeSchemaField("CCSPlayerPawn", "m_bIsBuyMenuOpen", typeof(bool))]
    private partial SchemaField GetIsBuyMenuOpenField();

    [NativeSchemaField("CCSPlayerPawn", "m_bLeftHanded", typeof(bool))]
    private partial SchemaField GetLeftHandedField();

    [NativeSchemaField("CCSPlayerPawn", "m_fSwitchedHandednessTime", typeof(float))]
    private partial SchemaField GetSwitchedHandednessTimeField();

    [NativeSchemaField("CCSPlayerPawn", "m_vecTotalBulletForce", typeof(Vector))]
    private partial SchemaField GetTotalBulletForceField();

    [NativeSchemaField("CCSPlayerPawn", "m_iDeathFlags", typeof(int))]
    private partial SchemaField GetDeathFlagsField();

    [NativeSchemaField("CCSPlayerPawn", "m_bWaitForNoAttack", typeof(bool))]
    private partial SchemaField GetWaitForNoAttackField();

    [NativeSchemaField("CCSPlayerPawn", "m_aimPunchAngle", typeof(Vector))]
    private partial SchemaField GetAimPunchAngleField();

    [NativeSchemaField("CCSPlayerPawn", "m_aimPunchAngleVel", typeof(Vector))]
    private partial SchemaField GetAimPunchAngleVelocityField();

    [NativeSchemaField("CCSPlayerPawn", "m_aimPunchTickBase", typeof(int))]
    private partial SchemaField GetAimPunchTickBaseField();

    [NativeSchemaField("CCSPlayerPawn", "m_aimPunchTickFraction", typeof(float))]
    private partial SchemaField GetAimPunchTickFractionField();

    [NativeSchemaField("CCSPlayerPawn",
                       "m_aimPunchCache",
                       typeof(SchemaUnmanagedVector<Vector>),
                       InlineObject = true,
                       ReturnType = typeof(ISchemaList<Vector>))]
    private partial SchemaField GetAimPunchCacheField();

#endregion
}
