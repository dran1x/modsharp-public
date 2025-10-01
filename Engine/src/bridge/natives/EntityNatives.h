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

#ifndef MS_NATIVE_ENTITY_H
#define MS_NATIVE_ENTITY_H

#include "definitions.h"

class CBaseEntity;
class CEntityIOOutput;
class CEntityIdentity;
class Variant_t;

namespace natives::entity
{
void        Init();
EHookAction OnEntityFireOutput(CBaseEntity* pCaller, const CEntityIOOutput* pIO, CBaseEntity* pActivator, float flDelay);
EHookAction OnEntityAcceptInput(CEntityIdentity* pInstance, const char* pInput, CBaseEntity* pActivator, CBaseEntity* pCaller, Variant_t* pValue, int outputId);
} // namespace natives::entity

#endif
