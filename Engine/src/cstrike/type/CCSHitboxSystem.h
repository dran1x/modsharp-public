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

#ifndef CSTRIKE_TYPE_HITBOXSYSTEM_H
#define CSTRIKE_TYPE_HITBOXSYSTEM_H

#include <cstdint>

class CBaseEntity;
class CBodyComponentSkeletonInstance;

class CCSHitboxSystem
{
    virtual bool TraceLineShouldHitEntity(void*, const CBodyComponentSkeletonInstance*, void*, const Vector*, const Vector*)           = 0;
    virtual bool Unknown001(void*, void*, void*, int32_t, uint32_t, void*)                                                             = 0;
    virtual bool TraceShapeShouldHitEntity(void*, const CBodyComponentSkeletonInstance*, void*, const Vector*, const Vector*, int32_t) = 0;
};

#endif