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
using Sharp.Core.Bridges.Natives;
using Sharp.Shared.GameEntities;
using Sharp.Shared.Managers;
using Sharp.Shared.Types;

namespace Sharp.Core.Managers;

internal interface ICoreSoundManager : ISoundManager;

internal unsafe class SoundManager : ICoreSoundManager
{
    private readonly ILogger<SoundManager> _logger;

    public SoundManager(ILogger<SoundManager> logger)
        => _logger = logger;

    public float GetSoundDuration(string soundEvent)
        => Sound.GetSoundDuration(soundEvent);

    public SoundOpEventGuid StartSoundEvent(string sound,
        IBaseEntity?                               entity = null,
        float?                                     volume = null,
        RecipientFilter                            filter = default)
        => Sound.StartSoundEvent(entity?.GetAbsPtr() ?? nint.Zero, sound, &volume, &filter);

    public void StopSoundEvent(SoundOpEventGuid guid)
        => Sound.StopSoundEvent(&guid);

    public void StopSoundEvent(SoundOpEventGuid guid, RecipientFilter filter)
        => Sound.StopSoundEventFilter(&guid, &filter);

    public bool SetSoundEventParam(SoundOpEventGuid guid, string param, float value, RecipientFilter filter = default)
        => Sound.SetSoundEventParamFloat(guid, param, value, &filter);

    public bool SetSoundEventParam(SoundOpEventGuid guid, string param, Vector value, RecipientFilter filter = default)
        => Sound.SetSoundEventParamVector(guid, param, &value, &filter);

    public bool SetSoundEventParam(SoundOpEventGuid guid, string param, int value, RecipientFilter filter = default)
        => Sound.SetSoundEventParamInt32(guid, param, value, &filter);

    public bool SetSoundEventParam(SoundOpEventGuid guid, string param, uint value, RecipientFilter filter = default)
        => Sound.SetSoundEventParamUInt32(guid, param, value, &filter);

    public bool SetSoundEventParam(SoundOpEventGuid guid, string param, ulong value, RecipientFilter filter = default)
        => Sound.SetSoundEventParamUInt64(guid, param, value, &filter);

    public bool IsSoundEventValid(string soundEvent)
        => Sound.IsSoundEventValid(soundEvent);
}
