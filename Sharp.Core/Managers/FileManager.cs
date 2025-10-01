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
using Microsoft.Extensions.Logging;
using Sharp.Core.Bridges;
using Sharp.Core.Bridges.Interfaces;
using Sharp.Core.CStrike;
using Sharp.Shared.Managers;
using Sharp.Shared.Utilities;

namespace Sharp.Core.Managers;

internal interface ICoreFileManager : IFileManager;

internal class FileManager : ICoreFileManager
{
    private readonly ILogger<FileManager> _logger;

    public FileManager(ILogger<FileManager> logger)
        => _logger = logger;

    public nint GetValveFileSystem()
        => CoreBridge.ValveFileSystem;

    public bool FileExists(string filename, string pathId = "GAME")
        => CoreBridge.FullFileSystemInstance.FileExists(filename, pathId);

    public IValveFile? OpenFile(string filename, string pathId = "GAME")
    {
        var fileHandle = CoreBridge.FullFileSystemInstance.Open(filename, "rb", pathId);

        if (!fileHandle.IsValid())
        {
            _logger.LogError("Failed to open file {Filename} in {PathId}", filename, pathId);

            return null;
        }

        return ValveFile.Create(new IntPtr(fileHandle));
    }

    public unsafe IValveDirectory? OpenDirectory(string path, string pathId = "GAME")
    {
        FileFindHandle_t handle;

        string temp;

        if (!path.EndsWith("/") && !path.EndsWith("\\"))
        {
            temp = $"{path}/";
        }
        else
        {
            temp = path;
        }

        var currentFile = CoreBridge.FullFileSystemInstance.FindFirstEx($"{temp}*", pathId, &handle);

        if (currentFile is null)
        {
            return null;
        }

        return new ValveDirectory(handle, NativeString.ReadString(currentFile));
    }

    public void AddSearchPath(string path, string pathId, int addType = 1, int priority = 0, int unknown = 0)
        => CoreBridge.FullFileSystemInstance.AddSearchPath(path, pathId, addType, priority, unknown);

    public void RemoveSearchPath(string path, string pathId)
        => CoreBridge.FullFileSystemInstance.RemoveSearchPath(path, pathId);
}

internal partial class ValveFile : NativeObject, IValveFile
{
    public void Dispose()
        => CoreBridge.FullFileSystemInstance.Close(_this);

    public unsafe void Read(Span<byte> output)
    {
        fixed (byte* outputPtr = output)
        {
            CoreBridge.FullFileSystemInstance.Read(outputPtr, output.Length, _this);
        }
    }

    public unsafe void Write(ReadOnlySpan<byte> input)
    {
        fixed (byte* inputPtr = input)
        {
            CoreBridge.FullFileSystemInstance.Write(inputPtr, input.Length, _this);
        }
    }

    public int Size()
        => CoreBridge.FullFileSystemInstance.Size(_this);
}

internal class ValveDirectory : IValveDirectory
{
    private readonly FileFindHandle_t _handle;
    public           string           CurrentFile { get; private set; }

    public ValveDirectory(FileFindHandle_t handle, string currentFile)
    {
        _handle     = handle;
        CurrentFile = currentFile;
    }

    public IEnumerator<string> GetEnumerator()
    {
        do
        {
            yield return CurrentFile;
        }
        while (FindNext());
    }

    private unsafe bool FindNext()
    {
        var handle = _handle;
        var result = CoreBridge.FullFileSystemInstance.FindNext(&handle);

        if (result is null)
        {
            return false;
        }

        CurrentFile = NativeString.ReadString(result);

        return true;
    }

    public unsafe void Dispose()
    {
        var handle = _handle;

        CoreBridge.FullFileSystemInstance.FindClose(&handle);
    }
}
