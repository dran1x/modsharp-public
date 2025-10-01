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
using Microsoft.Extensions.Logging;

namespace Sharp.Core.Helpers;

internal class ExceptionHandler
{
    private readonly ILogger<ExceptionHandler> _logger;

    public ExceptionHandler(ILogger<ExceptionHandler> logger)
        => _logger = logger;

    public void Start()
        => AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

    private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        try
        {
            if (e.ExceptionObject is Exception ex)
            {
                string errorText;

                if (ex is AccessViolationException)
                {
                    errorText = "哦豁, 野指针了, 大兄弟";
                }
                else if (ex is NullReferenceException)
                {
                    errorText = "哦豁, 空指针了, 大兄弟";
                }
                else
                {
                    errorText = "哦豁, 抛异常了, 大兄弟";
                }

                var exception = new Exception(errorText, ex);
                _logger.LogCritical(exception, "An unhandled error occurred");

                Bridges.Natives.Core.FatalError(ex.ToString());

                // Quit
                Environment.Exit(1);
            }
        }
        catch
        {
            // empty
        }
    }
}
