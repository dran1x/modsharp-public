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

namespace Sharp.Core.Bridges.Natives;

public static partial class Econ
{
    public static partial nint GetItemDefinitions();

    public static partial nint GetPaintKits();

    public static partial bool UpdateItemAttributes(nint entity,
        uint                                             accountId,
        string                                           nameTag,
        int                                              paint,
        int                                              pattern,
        float                                            wear,
        int                                              nSticker1,
        float                                            flSticker1,
        int                                              nSticker2,
        float                                            flSticker2,
        int                                              nSticker3,
        float                                            flSticker3,
        int                                              nSticker4,
        float                                            flSticker4);
}
