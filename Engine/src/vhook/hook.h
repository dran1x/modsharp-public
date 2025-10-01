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

#ifndef MS_VHOOK_HOOK_H
#define MS_VHOOK_HOOK_H

#include <cstdint>
#include <safetyhook.hpp>

/*
 * instance: pointer to an instance of a class
 * hook: function to overwrite with
 * offset: 0 = method 1, 1 = method 2 etc...
 * return: original function
 */

inline void* vtablehook_hook(void* instance, void* hook, int offset)
{
    const auto vtable   = *reinterpret_cast<intptr_t*>(instance);
    const auto entry    = vtable + sizeof(intptr_t) * offset;
    const auto original = *reinterpret_cast<intptr_t*>(entry);

    auto protect = safetyhook::unprotect((uint8_t*)entry, sizeof(void*));

    *reinterpret_cast<intptr_t*>(entry) = reinterpret_cast<intptr_t>(hook);

    return reinterpret_cast<void*>(original);
}

inline void* vtablehook_hook_direct(void* vtable, void* hook, int offset)
{
    const auto entry    = (intptr_t)vtable + sizeof(intptr_t) * offset;
    const auto original = *reinterpret_cast<intptr_t*>(entry);

    auto protect                        = safetyhook::unprotect((uint8_t*)entry, sizeof(void*));
    *reinterpret_cast<intptr_t*>(entry) = reinterpret_cast<intptr_t>(hook);

    return reinterpret_cast<void*>(original);
}

#endif