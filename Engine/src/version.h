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

#ifndef MS_ROOT_VERSION_H
#define MS_ROOT_VERSION_H

// 不能删掉!!!
// ReSharper disable once CppUnusedIncludeDirective
#include "version_auto.h"

#ifndef GIT_VERSION
#    define GIT_VERSION 0
#endif

#ifndef GIT_VERSION_STR
#    define GIT_VERSION_STR "Local"
#endif

#ifndef BUILD_DATE
#    define BUILD_DATE __DATE__ " " __TIME__
#endif

#ifndef COMMIT_HASH
#    define COMMIT_HASH "1234567"
#endif

#define STR_HELPER(x) #x
#define STRINGIFY(x) STR_HELPER(x)

#define COPYRIGHT_YEAR 2025
#define MAJOR_VERSION 2
#define MINOR_VERSION 0
#define VERSION_STRING STRINGIFY(MAJOR_VERSION) "." STRINGIFY(MINOR_VERSION)

#define VERSION_STRING_BUILD VERSION_STRING "." GIT_VERSION_STR
#define FULL_VERSION_STRING VERSION_STRING "+dev" GIT_VERSION_STR

#define AUTHORS_STRING "Kxnrl, BoneTM, laper32, Nukoooo"
#define YEAR_STRING STRINGIFY(COPYRIGHT_YEAR) ""

#define COPYRIGHT_STRING "© " YEAR_STRING " Kxnrl, BoneTM, laper32, Nukoooo"

#endif
