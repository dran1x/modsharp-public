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

using Sharp.Shared.Listeners;
using Sharp.Shared.Objects;
using Sharp.Shared.Units;

namespace Sharp.Shared.Managers;

public interface IEventManager
{
    /// <summary>
    ///     添加EventListener以监听事件
    /// </summary>
    void InstallEventListener(IEventListener listener);

    /// <summary>
    ///     移除EventListener
    /// </summary>
    void RemoveEventListener(IEventListener listener);

    /// <summary>
    ///     创建Game Event
    /// </summary>
    IGameEvent? CreateEvent(string name, bool force);

    /// <summary>
    ///     创建Game Event
    /// </summary>
    T? CreateEvent<T>(bool force) where T : class, IGameEvent;

    /// <summary>
    ///     克隆事件 (可编辑)  <br />
    ///     <remarks>Clone一个新的事件, 需要后期手动释放或发射</remarks>
    /// </summary>
    T? CloneEvent<T>(T @event) where T : class, IGameEvent;

    /// <summary>
    ///     添加某个事件名称到Hook队列
    /// </summary>
    void HookEvent(string name);

    /// <summary>
    ///     检查客户端订阅的事件
    /// </summary>
    bool FindListener(PlayerSlot slot, string name);
}
