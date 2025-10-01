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

using Microsoft.Extensions.Logging;
using Sharp.Shared.HookParams;
using Sharp.Shared.Types;
using Sharp.Shared.Units;

namespace Sharp.Core.Managers.Forwards;

internal class EmitMusicForward : ForwardType<EmitMusicHookParams, EmitMusicForward>
{
    public EmitMusicForward(ILoggerFactory factory) : base(factory)
        => Bridges.Forwards.Game.OnEmitMusicPost += SoundOpGameSystem_OnEmitMusicPost;

    private void SoundOpGameSystem_OnEmitMusicPost(int entity,
        string                                         sound,
        float                                          duration,
        ulong                                          receivers,
        SoundOpEventGuid                               guid)
    {
        if (!IsForwardInvokeRequired())
        {
            return;
        }

        var param = new EmitMusicHookParams(entity, sound, duration, guid, receivers);

        InvokeForward(param);
        param.MarkAsDisposed();
    }
}

internal sealed class EmitMusicHookParams : FunctionParams, IEmitMusicForwardParams
{
    public EmitMusicHookParams(EntityIndex index,
        string                             soundName,
        float                              duration,
        SoundOpEventGuid                   guid,
        ulong                              receivers) : base(true)
    {
        EntityIndex = index;
        SoundName   = soundName;
        Duration    = duration;
        EventGuid   = guid;
        Receivers   = receivers;
    }

    public EntityIndex      EntityIndex { get; }
    public string           SoundName   { get; }
    public float            Duration    { get; }
    public SoundOpEventGuid EventGuid   { get; }

    public NetworkReceiver Receivers { get; }
}
