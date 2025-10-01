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
using System.Text;

namespace Sharp.Generator.Sdk.Utils;

public class CodeWriter
{
    public CodeWriter()
        => m_ScopeTracker = new ScopeTracker(this); //We only need one. It can be reused.

    private StringBuilder Content        { get; } = new ();
    private int           IndentLevel    { get; set; }
    private ScopeTracker  m_ScopeTracker { get; } //We only need one. It can be reused.

    public void Append(string line)
        => Content.Append(line);

    public void AppendLine(string line)
        => Content.Append(new string('\t', IndentLevel)).AppendLine(line);

    public void AppendLine()
        => Content.AppendLine();

    public IDisposable BeginScope(string line)
    {
        AppendLine(line);

        return BeginScope();
    }

    public IDisposable BeginScope()
    {
        Content.Append(new string('\t', IndentLevel)).AppendLine("{");
        IndentLevel += 1;

        return m_ScopeTracker;
    }

    public void EndLine()
        => Content.AppendLine();

    public void EndScope()
    {
        IndentLevel -= 1;
        Content.Append(new string('\t', IndentLevel)).AppendLine("}");
    }

    public void StartLine()
        => Content.Append(new string('\t', IndentLevel));

    public override string ToString()
        => Content.ToString();

    private string EscapeString(string text)
        => text.Replace("\"", "\"\"");

    private class ScopeTracker : IDisposable
    {
        public ScopeTracker(CodeWriter parent)
            => Parent = parent;

        public CodeWriter Parent { get; }

        public void Dispose()
        {
            Parent.EndScope();
        }
    }
}
