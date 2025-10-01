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

using System;
using System.Collections.Generic;
using Sharp.Shared.CStrike;

namespace Sharp.Shared.Managers;

public interface IFileManager
{
    nint GetValveFileSystem();

    bool FileExists(string filename, string pathId = "GAME");

    IValveFile? OpenFile(string filename, string pathId = "GAME");

    IValveDirectory? OpenDirectory(string path, string pathId = "GAME");

    void AddSearchPath(string path, string pathId, int addType = 1, int priority = 0, int unknown = 0);

    void RemoveSearchPath(string path, string pathId);
}

public interface IValveFile : INativeObject, IDisposable
{
    void Read(Span<byte> output);

    void Write(ReadOnlySpan<byte> input);

    int Size();
}

public interface IValveDirectory : IDisposable
{
    IEnumerator<string> GetEnumerator();
}
