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

#ifndef MS_HOOKBRIDGE_H
#define MS_HOOKBRIDGE_H

#include <safetyhook.hpp>

class IHook
{
public:
    virtual ~IHook()                                    = default;
    virtual void  Prepare(void* targetFn, void* hookFn) = 0;
    virtual bool  Install()                             = 0;
    virtual void  Uninstall()                           = 0;
    virtual void* GetOriginalFunction()                 = 0;

protected:
    void* m_pTargetFn{};
    void* m_pHookFn{};
};

class InlineHook final : public IHook
{
public:
    ~InlineHook() override;

    void Prepare(void* targetFn, void* hookFn) override;

    bool Install() override;

    void Uninstall() override;

    void* GetOriginalFunction() override;

private:
    SafetyHookInline m_hkInline{};
};

class MidFuncHook final : public IHook
{
public:
    ~MidFuncHook() override;
    void Prepare(void* targetFn, void* hookFn) override;

    bool Install() override;

    void  Uninstall() override;
    void* GetOriginalFunction() override;

private:
    SafetyHookMid m_hkMid{};
};

#endif