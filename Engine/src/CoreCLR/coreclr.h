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

#ifndef CORECLR_H
#define CORECLR_H

namespace coreclr
{
const char* GetDotNetVersion();
int  Bootstrap(void* natives, void* forwards);
void Shutdown();
bool Init(const char* baseDir);
// bool  CreateNative(const char* name, void* func);
void* GetManagedFunction(const char* name);
template <typename T>
T GetManagedFunction(const char* name)
{
    return reinterpret_cast<T>(GetManagedFunction(name));
}

} // namespace coreclr

#endif
