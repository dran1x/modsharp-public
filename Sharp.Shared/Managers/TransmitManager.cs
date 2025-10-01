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

using Sharp.Shared.Enums;
using Sharp.Shared.GameEntities;
using Sharp.Shared.Units;

namespace Sharp.Shared.Managers;

public interface ITransmitManager
{
    /// <summary>
    ///     Hook 实体
    /// </summary>
    /// <param name="entity">实体Instance</param>
    /// <param name="defaultTransmit">默认全部Channel状态</param>
    bool AddEntityHooks(IBaseEntity entity, bool defaultTransmit);

    /// <summary>
    ///     手动移除Hook
    ///     <remarks>Hook会在实体删除时自动销毁</remarks>
    /// </summary>
    bool RemoveEntityHooks(IBaseEntity entity);

    /// <summary>
    ///     实体是否已经被Hook
    /// </summary>
    /// <param name="entity">实体Instance</param>
    bool IsEntityHooked(IBaseEntity entity);

    /// <summary>
    ///     获取实体的Controller可见状态
    /// </summary>
    /// <param name="entity">实体Index</param>
    /// <param name="controllerIndex">ControllerIndex</param>
    /// <param name="channel">Channel, -1为读取全局状态</param>
    bool GetEntityState(EntityIndex entity, EntityIndex controllerIndex, int channel = -1);

    /// <summary>
    ///     设置实体的Controller可见状态
    /// </summary>
    /// <param name="entity">实体Index</param>
    /// <param name="controllerIndex">ControllerIndex</param>
    /// <param name="transmit">是否可见</param>
    /// <param name="channel">Channel</param>
    bool SetEntityState(EntityIndex entity, EntityIndex controllerIndex, bool transmit, int channel);

    /// <summary>
    ///     获取实体是否被Block
    /// </summary>
    bool GetEntityBlock(EntityIndex entity);

    /// <summary>
    ///     设置实体Block State
    /// </summary>
    bool SetEntityBlock(EntityIndex entity, bool state);

    /// <summary>
    ///     获取Hook中的实体Owner
    /// </summary>
    /// <returns>-2 = NoHook | -1 = Null | other = Entity Index</returns>
    int GetEntityOwner(EntityIndex entity);

    /// <summary>
    ///     设置Hook中的实体Owner
    /// </summary>
    /// <param name="entity">实体Index</param>
    /// <param name="owner">Owner实体的Index</param>
    bool SetEntityOwner(EntityIndex entity, EntityIndex owner);

    /// <summary>
    ///     TempEnt的状态
    /// </summary>
    /// <param name="type">TE类型</param>
    /// <param name="slot">IGameClient Slot</param>
    bool GetTempEntState(BlockTempEntType type, PlayerSlot slot);

    /// <summary>
    ///     设置TempEnt的状态
    /// </summary>
    /// <param name="type">TE类型</param>
    /// <param name="slot">IGameClient Slot</param>
    /// <param name="state">可见状态</param>
    void SetTempEntState(BlockTempEntType type, PlayerSlot slot, bool state);

    /// <summary>
    ///     重置接受者的所有实体状态
    /// </summary>
    /// <param name="receiverIndex">receiver controller index</param>
    void ClearReceiverState(EntityIndex receiverIndex);

    /// <summary>
    ///     检查武器屏蔽FireBullet
    /// </summary>
    TransmitFireBulletState GetWeaponFireBulletState(IBaseWeapon weapon);

    /// <summary>
    ///     设置武器FireBullet状态
    /// </summary>
    void SetWeaponFireBulletState(IBaseWeapon weapon, TransmitFireBulletState state);
}
