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

using System.Runtime.InteropServices;
using Sharp.Core.CStrike;
using Sharp.Shared.GameObjects;
using Sharp.Shared.Utilities;

namespace Sharp.Core.GameObjects;

internal partial class PaintKit : NativeObject, IPaintKit
{
    private unsafe PaintKit(nint pointer) : base(pointer)
    {
        var kit = (CPaintKit*) pointer;

        Id                = kit->Index;
        Name              = Utils.ReadString(kit->m_pszPaintKitName);
        DescriptionString = Utils.ReadString(kit->m_pszPaintKitDescriptionTag);
        DescriptionTag    = Utils.ReadString(kit->m_pszPaintKitNameTag);
        Rarity            = kit->m_nRarity;
        IsUseNormalModel  = kit->m_bUseNormalModel;
        IsLegacyModel     = kit->m_bLegacyModel;
    }

    public uint   Id                { get; }
    public string Name              { get; }
    public string DescriptionString { get; }
    public string DescriptionTag    { get; }
    public uint   Rarity            { get; }
    public bool   IsUseNormalModel  { get; }
    public bool   IsLegacyModel     { get; }
}

[StructLayout(LayoutKind.Explicit)]
file unsafe struct CPaintKit
{
    [FieldOffset(0)]
    public uint Index;

    [FieldOffset(8)]
    public byte* m_pszPaintKitName;

    [FieldOffset(0x10)]
    public byte* m_pszPaintKitDescriptionTag;

    [FieldOffset(0x18)]
    public byte* m_pszPaintKitNameTag;

    [FieldOffset(0x44)]
    public uint m_nRarity;

    [FieldOffset(0xAD)]
    public bool m_bUseNormalModel;

    [FieldOffset(0xAE)]
    public bool m_bLegacyModel;
}
