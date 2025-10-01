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

#ifndef MS_ADAPTER_H
#define MS_ADAPTER_H

template <typename TReturn = void, typename... TArgs>
class ForwardItem
{
public:
    TReturn Invoke(TArgs... args)
    {
        return m_Func(args...);
    }

public:
    char m_Name[128];
    TReturn (*m_Func)(TArgs...) = nullptr;
    int m_InstallCount;
};

class NativeItem
{
public:
    char  m_Name[128];
    void* m_Func;
};

namespace bridge
{

void CreateNative(const char* name, void* func);

ForwardItem<void>* CreateForward(const char* name);
template <typename TReturn, typename... TArgs>
ForwardItem<TReturn, TArgs...>* CreateForward(const char* name)
{
    return reinterpret_cast<ForwardItem<TReturn, TArgs...>*>(CreateForward(name));
}

void* GetNatives();
void* GetForwards();

void InitNatives();

} // namespace bridge
#endif // MS_ADAPTER_H