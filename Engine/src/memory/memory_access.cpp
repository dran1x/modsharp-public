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

#include "memory_access.h"

#include <safetyhook.hpp>

bool SetMemoryAccess(uint8_t* address, size_t size, uint8_t access)
{
    const bool read    = (access & MemoryAccess_Read) != 0;
    const bool write   = (access & MemoryAccess_Write) != 0;
    const bool execute = (access & MemoryAccess_Execute) != 0;

    const auto result = safetyhook::vm_protect(address, size, safetyhook::VmAccess{.read = read, .write = write, .execute = execute});

    return result.has_value();
}