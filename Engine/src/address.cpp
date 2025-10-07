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

#include "address.h"
#include "gamedata.h"
#include "global.h"
#include "logging.h"
#include "module.h"
#include "types.h"

#include <array>

#define RESOLVE_GAMEDATA_ADDRESS(name, variable) \
    (variable) = g_pGameData->GetAddress<decltype(variable)>(name)

#ifdef PLATFORM_WINDOWS
#    define RELATE_SERVER_LIB_FILE_PATH "../../csgo/bin/win64/"
#else
#    define RELATE_SERVER_LIB_FILE_PATH "../../csgo/bin/linuxsteamrt64/"
#endif

GameData* g_pGameData;

bool address::Initialize()
{
    modules::server          = new CModule(RELATE_SERVER_LIB_FILE_PATH LIB_FILE_PREFIX "server");
    modules::engine          = new CModule(LIB_FILE_PREFIX "engine2");
    modules::tier0           = new CModule(LIB_FILE_PREFIX "tier0");
    modules::schemas         = new CModule(LIB_FILE_PREFIX "schemasystem");
    modules::resource        = new CModule(LIB_FILE_PREFIX "resourcesystem");
    modules::vscript         = new CModule(LIB_FILE_PREFIX "vscript");
    modules::vphysics2       = new CModule(LIB_FILE_PREFIX "vphysics2");
    modules::sound           = new CModule(LIB_FILE_PREFIX "soundsystem");
    modules::network         = new CModule(LIB_FILE_PREFIX "networksystem");
    modules::worldrenderer   = new CModule(LIB_FILE_PREFIX "worldrenderer");
    modules::matchmaking     = new CModule(LIB_FILE_PREFIX "matchmaking");
    modules::filesystem      = new CModule(LIB_FILE_PREFIX "filesystem_stdio");
    modules::steamsockets    = new CModule(LIB_FILE_PREFIX "steamnetworkingsockets");
    modules::materialsystem2 = new CModule(LIB_FILE_PREFIX "materialsystem2");
    modules::animationsystem = new CModule(LIB_FILE_PREFIX "animationsystem");

    g_pGameData = new GameData();

    constexpr std::array gamedataFiles = {
        "core.games.jsonc",
        "tier0.games.jsonc",
        "engine.games.jsonc",
        "server.games.jsonc",
        "log.games.jsonc",
    };

    bool all_succeeded = true;

    for (const auto& path : gamedataFiles)
    {
        if (!g_pGameData->Register(path))
        {
            all_succeeded = false;
        }
    }

    if (!all_succeeded)
    {
        FatalError("Failed to load one or more gamedata files. See log for details.");
    }

    RESOLVE_GAMEDATA_ADDRESS("Source2_Init", address::engine::Source2_Init);

    // cvar
    RESOLVE_GAMEDATA_ADDRESS("ScriptSetConVarString", address::server::ScriptSetConVarString);
    RESOLVE_GAMEDATA_ADDRESS("ScriptSetConVarNumber", address::server::ScriptSetConVarNumber);
    RESOLVE_GAMEDATA_ADDRESS("ScriptSetConVarDouble", address::server::ScriptSetConVarDouble);

    RESOLVE_GAMEDATA_ADDRESS("NetworkStateChanged", address::server::NetworkStateChanged);
    RESOLVE_GAMEDATA_ADDRESS("StateChanged", address::server::StateChanged);

    // CEntityIdentity
    RESOLVE_GAMEDATA_ADDRESS("CEntityIdentity::SetEntityName", address::server::CEntityIdentity_SetEntityName);

    // CBaseEntity
    RESOLVE_GAMEDATA_ADDRESS("CreateEntityByName", address::server::CreateEntityByName);
    RESOLVE_GAMEDATA_ADDRESS("CBaseEntity::AbsOrigin", address::server::CBaseEntity_AbsOrigin);
    RESOLVE_GAMEDATA_ADDRESS("CBaseEntity::SetAbsOrigin", address::server::CBaseEntity_SetAbsOrigin);
    RESOLVE_GAMEDATA_ADDRESS("CBaseEntity::AbsAngles", address::server::CBaseEntity_AbsAngles);
    RESOLVE_GAMEDATA_ADDRESS("CBaseEntity::SetAbsAngles", address::server::CBaseEntity_SetAbsAngles);
    RESOLVE_GAMEDATA_ADDRESS("CBaseEntity::LocalVelocity", address::server::CBaseEntity_LocalVelocity);
    RESOLVE_GAMEDATA_ADDRESS("CBaseEntity::AbsVelocity", address::server::CBaseEntity_AbsVelocity);
    RESOLVE_GAMEDATA_ADDRESS("CBaseEntity::SetAbsVelocity", address::server::CBaseEntity_SetAbsVelocity);
    RESOLVE_GAMEDATA_ADDRESS("CBaseEntity::AcceptInput", address::server::CBaseEntity_AcceptInput);
    RESOLVE_GAMEDATA_ADDRESS("CBaseEntity::SetMoveType", address::server::CBaseEntity_SetMoveType);
    RESOLVE_GAMEDATA_ADDRESS("CBaseEntity::SetGravityScale", address::server::CBaseEntity_SetGravityScale);
    RESOLVE_GAMEDATA_ADDRESS("CBaseEntity::ApplyAbsVelocityImpulse", address::server::CBaseEntity_ApplyAbsVelocityImpulse);
    RESOLVE_GAMEDATA_ADDRESS("CBaseEntity::DispatchSpawn", address::server::CBaseEntity_DispatchSpawn);
    RESOLVE_GAMEDATA_ADDRESS("CBaseEntity::SetGroundEntity", address::server::CBaseEntity_SetGroundEntity);
    RESOLVE_GAMEDATA_ADDRESS("CBaseEntity::EmitSoundFilter", address::server::CBaseEntity_EmitSoundFilter);
    RESOLVE_GAMEDATA_ADDRESS("CBaseEntity::StopSound", address::server::CBaseEntity_StopSound);
    RESOLVE_GAMEDATA_ADDRESS("CBaseEntity::DispatchTraceAttack", address::server::CBaseEntity_DispatchTraceAttack);

    // PlayerController
    RESOLVE_GAMEDATA_ADDRESS("CBasePlayerController::SwitchSteam", address::server::CBasePlayerController_SwitchSteam);
    RESOLVE_GAMEDATA_ADDRESS("CBasePlayerController::SetPawn", address::server::CBasePlayerController_SetPawn);
    RESOLVE_GAMEDATA_ADDRESS("CBasePlayerController::CheckPawn", address::server::CBasePlayerController_CheckPawn);

    // services
    RESOLVE_GAMEDATA_ADDRESS("CCSPlayer_ItemServices::GiveGlove", address::server::ServiceGiveGlove);
    RESOLVE_GAMEDATA_ADDRESS("CCSPlayer_ItemServices::GiveNamedItem", address::server::PlayerPawnItemServices_GiveNamedItem);
    RESOLVE_GAMEDATA_ADDRESS("CCSPlayer_ItemServices::RemovePlayerItem", address::server::PlayerPawnWeaponServices_RemovePlayerItem);
    RESOLVE_GAMEDATA_ADDRESS("CCSPlayer_WeaponServices::GetWeaponBySlot", address::server::PlayerPawnWeaponServices_GetWeaponBySlot);
    RESOLVE_GAMEDATA_ADDRESS("CCSPlayer_WeaponServices::DetachWeapon", address::server::PlayerPawnWeaponServices_DetachWeapon);

    // entity list
    RESOLVE_GAMEDATA_ADDRESS("CGameEntitySystem::FindEntityByIndex", address::server::CGameEntitySystem_FindEntityByIndex);
    RESOLVE_GAMEDATA_ADDRESS("CGameEntitySystem::FindByClassname", address::server::CGameEntitySystem_FindByClassname);
    RESOLVE_GAMEDATA_ADDRESS("CGameEntitySystem::FindByName", address::server::CGameEntitySystem_FindByName);
    RESOLVE_GAMEDATA_ADDRESS("CGameEntitySystem::FindInSphere", address::server::CGameEntitySystem_FindInSphere);
    RESOLVE_GAMEDATA_ADDRESS("CGameEntitySystem::SpawnEntityFromKeyValuesSync", address::server::CGameEntitySystem_SpawnEntityFromKeyValuesSync);
    RESOLVE_GAMEDATA_ADDRESS("CGameEntitySystem::AddListenerEntity", address::server::CGameEntitySystem_AddListenerEntity);
    RESOLVE_GAMEDATA_ADDRESS("CGameEntitySystem::RemoveListenerEntity", address::server::CGameEntitySystem_RemoveListenerEntity);
    RESOLVE_GAMEDATA_ADDRESS("CGameEntitySystem::AllocPooledString", address::server::CGameEntitySystem_AllocPooledString);
    RESOLVE_GAMEDATA_ADDRESS("CGameEntitySystem::AddEntityIOEvent", address::server::CGameEntitySystem_AddEntityIOEvent);

    // PlayerPawn
    RESOLVE_GAMEDATA_ADDRESS("CBasePlayerPawn::FindMatchingWeaponsForTeamLoadout", address::server::CBasePlayerPawn_FindMatchingWeaponsForTeamLoadout);

    // GameRules
    RESOLVE_GAMEDATA_ADDRESS("CCSGameRules::PlayerCanHearChat", address::server::CCSGameRules_PlayerCanHearChat);
    RESOLVE_GAMEDATA_ADDRESS("CCSGameRules::TerminateRound", address::server::CCSGameRules_TerminateRound);

    // Utils
    RESOLVE_GAMEDATA_ADDRESS("UTIL_SetModel", address::server::UTIL_SetModel);
    RESOLVE_GAMEDATA_ADDRESS("UTIL_GetEconItemSchema", address::server::GetEconItemSchema);
    RESOLVE_GAMEDATA_ADDRESS("UTIL_RadioMessage", address::server::UTIL_RadioMessage);
    RESOLVE_GAMEDATA_ADDRESS("UTIL_DispatchEffect", address::server::UTIL_DispatchEffect);
    RESOLVE_GAMEDATA_ADDRESS("UTIL_DispatchEffectFilter", address::server::UTIL_DispatchEffectFilter);
    RESOLVE_GAMEDATA_ADDRESS("UTIL_DispatchParticleEffectFilter_Position", address::server::UTIL_DispatchParticleEffectFilterPosition);
    RESOLVE_GAMEDATA_ADDRESS("UTIL_DispatchParticleEffectFilter_Attachment", address::server::UTIL_DispatchParticleEffectFilterAttachment);

    // Misc
    RESOLVE_GAMEDATA_ADDRESS("CAttributeList::SetOrAddAttributeValueByName", address::server::CAttributeList_SetOrAddAttributeValueByName);

    // CEntityInstance
    RESOLVE_GAMEDATA_ADDRESS("CEntityInstance::GetEntityIndex", address::server::CEntityInstance_GetEntityIndex);
    RESOLVE_GAMEDATA_ADDRESS("CEntityInstance::Kill", address::server::CEntityInstance_Kill);
    RESOLVE_GAMEDATA_ADDRESS("CEntityInstance::GetRefEHandle", address::server::CEntityInstance_GetRefEHandle);
    RESOLVE_GAMEDATA_ADDRESS("CEntityInstance::GetOrCreatePublicScriptScope", address::server::CEntityInstance_GetOrCreatePublicScriptScope);
    RESOLVE_GAMEDATA_ADDRESS("CEntityInstance::GetOrCreatePrivateScriptScope", address::server::CEntityInstance_GetOrCreatePrivateScriptScope);

    // CBaseModelEntity
    RESOLVE_GAMEDATA_ADDRESS("CBaseModelEntity::SetBodyGroupByName", address::server::CBaseModelEntity_SetBodyGroupByName);
    RESOLVE_GAMEDATA_ADDRESS("CBaseModelEntity::SetMaterialGroupMask", address::server::CBaseModelEntity_SetMaterialGroupMask);
    RESOLVE_GAMEDATA_ADDRESS("CBaseModelEntity::LookupBone", address::server::CBaseModelEntity_LookupBone);
    RESOLVE_GAMEDATA_ADDRESS("CBaseModelEntity::GetBoneTransform", address::server::CBaseModelEntity_GetBoneTransform);
    RESOLVE_GAMEDATA_ADDRESS("CBaseModelEntity::SetModelScale", address::server::CBaseModelEntity_SetModelScale);
    RESOLVE_GAMEDATA_ADDRESS("CBaseModelEntity::SetCollisionBounds", address::server::CBaseModelEntity_SetCollisionBounds);

    // CPaintKit
    // RESOLVE_GAMEDATA_ADDRESS("CPaintKit::UsesLegacyModel", address::server::CPaintKit_UsesLegacyModel);

    // CGamePhysicsQueryInterface
    RESOLVE_GAMEDATA_ADDRESS("CGamePhysicsQueryInterface::TraceShape", address::server::CGamePhysicsQueryInterface_TraceShape);

    // Studio Model
    RESOLVE_GAMEDATA_ADDRESS("StudioModel::LookupAttachment", address::server::StudioModel_LookupAttachment);
    RESOLVE_GAMEDATA_ADDRESS("StudioModel::GetAttachment", address::server::StudioModel_GetAttachment);

    // Sound OP
    RESOLVE_GAMEDATA_ADDRESS("SoundOpGameSystem::SetSoundEventParamString", address::server::SoundOpGameSystem_SetSoundEventParamString);
    RESOLVE_GAMEDATA_ADDRESS("SoundOpGameSystem::StopSoundEvent", address::server::SoundOpGameSystem_StopSoundEvent);
    RESOLVE_GAMEDATA_ADDRESS("SoundOpGameSystem::StopSoundEventFilter", address::server::SoundOpGameSystem_StopSoundEventFilter);

    // Movement service
    RESOLVE_GAMEDATA_ADDRESS("CCSPlayer_MovementServices::TracePlayerBBox", address::server::CCSPlayer_MovementServices_TracePlayerBBox);

    // Tracefilter
    // RESOLVE_GAMEDATA_ADDRESS("CTraceFilterPlayerMovementCS::ctor", address::server::CTraceFilterPlayerMovementCS_ctor);

    // CCollisionProperty
    RESOLVE_GAMEDATA_ADDRESS("CCollisionProperty::SetSolid", address::server::CCollisionProperty_SetSolid);

    RESOLVE_GAMEDATA_ADDRESS("FindWeaponVDataByName", address::server::FindWeaponVDataByName);
    RESOLVE_GAMEDATA_ADDRESS("GetLegacyGameEventListener", address::server::GetLegacyGameEventListener);
    RESOLVE_GAMEDATA_ADDRESS("CGameEntitySystem::GetSpawnOriginOffset", address::server::CGameEntitySystem_GetSpawnOriginOffset);

    // ResourceSystem
    RESOLVE_GAMEDATA_ADDRESS("CResourceNameTyped::ResolveResourceName", address::resource::CResourceNameTyped_ResolveResourceName);

    return true;
}
