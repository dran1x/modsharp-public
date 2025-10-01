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
using Sharp.Shared.Enums;
using Sharp.Shared.GameObjects;
using Sharp.Shared.Utilities;

namespace Sharp.Core.GameObjects;

internal partial class EconItemDefinition : NativeObject, IEconItemDefinition
{
    private unsafe EconItemDefinition(nint pointer) : base(pointer)
    {
        var item = (CEconItemDefinition*) pointer;

        Index              = item->m_nDefIndex;
        Rarity             = item->m_nItemRarity;
        Quality            = item->m_nItemQuality;
        ItemBaseName       = Utils.ReadString(item->m_pszItemBaseName);
        ItemTypeName       = Utils.ReadString(item->m_pszItemTypeName);
        BaseDisplayModel   = Utils.ReadString(item->m_pszBaseDisplayModel);
        WorldDisplayModel  = Utils.ReadString(item->m_pszWorldDisplayModel);
        ItemClassName      = Utils.ReadString(item->m_pszItemClassname);
        DefinitionName     = Utils.ReadString(item->m_pszDefinitionName);
        StickerSlots       = item->m_iStickerSlots;
        ItemType           = (EconItemType) item->m_iItemType;
        DefaultLoadoutSlot = item->m_iDefaultLoadoutSlot;
    }

    public ushort       Index              { get; }
    public byte         Rarity             { get; }
    public byte         Quality            { get; }
    public string       ItemBaseName       { get; }
    public string       ItemTypeName       { get; }
    public string       BaseDisplayModel   { get; }
    public string       WorldDisplayModel  { get; }
    public string       ItemClassName      { get; }
    public string       DefinitionName     { get; }
    public uint         StickerSlots       { get; }
    public EconItemType ItemType           { get; }
    public int          DefaultLoadoutSlot { get; }
}

[StructLayout(LayoutKind.Explicit, Size = 0x3C8)]
file unsafe struct CEconItemDefinition
{
    [FieldOffset(0x10)]
    public ushort m_nDefIndex;

    [FieldOffset(0x42)]
    public byte m_nItemRarity;

    [FieldOffset(0x43)]
    public byte m_nItemQuality;

    [FieldOffset(0x70)]
    public byte* m_pszItemBaseName;

    [FieldOffset(0x80)]
    public byte* m_pszItemTypeName;

    [FieldOffset(0x148)]
    public byte* m_pszBaseDisplayModel;

    [FieldOffset(0x158)]
    public byte* m_pszWorldDisplayModel;

    [FieldOffset(0x168)]
    public uint m_iStickerSlots;

    [FieldOffset(0x1B8)]
    public uint m_iItemType;

    [FieldOffset(0x248)]
    public byte* m_pszItemClassname;

    [FieldOffset(0x260)]
    public byte* m_pszDefinitionName;

    [FieldOffset(0x338)]
    public int m_iDefaultLoadoutSlot;
}
