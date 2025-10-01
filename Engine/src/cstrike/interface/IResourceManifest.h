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

#ifndef CSTRIKE_INTERFACE_RESOURCEMANIFEST_H
#define CSTRIKE_INTERFACE_RESOURCEMANIFEST_H

class IEntityResourceManifest;

class IResourceManifest
{
public:
    IEntityResourceManifest* m_pResourceManifest;
};

class IEntityResourceManifest
{
public:
    virtual void AddResource(const char*)                      = 0;
    virtual void AddResource(const char*, void*)               = 0;
    virtual void AddResource(const char*, void*, void*, void*) = 0;

private:
    virtual void Method_04() = 0;
    virtual void Method_05() = 0;
    virtual void Method_06() = 0;
    virtual void Method_07() = 0;
    virtual void Method_08() = 0;
    virtual void Method_09() = 0;
    virtual void Method_10() = 0;
};

#endif