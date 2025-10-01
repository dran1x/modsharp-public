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

#ifndef __CORECLR_DELEGATES_H__
#define __CORECLR_DELEGATES_H__

#include <stdint.h>

#if defined(_WIN32) || defined(PLATFORM_WINDOWS)
#    define CORECLR_DELEGATE_CALLTYPE __stdcall
#    ifdef _WCHAR_T_DEFINED
using char_t = wchar_t;
#    else
typedef unsigned short char_t;
#    endif
#else
#    define CORECLR_DELEGATE_CALLTYPE
typedef char char_t;
#endif

#define UNMANAGEDCALLERSONLY_METHOD ((const char_t*)-1)

// Signature of delegate returned by coreclr_delegate_type::load_assembly_and_get_function_pointer
using load_assembly_and_get_function_pointer_fn = int(CORECLR_DELEGATE_CALLTYPE*)(
    const char_t* assembly_path /* Fully qualified path to assembly */,
    const char_t* type_name /* Assembly qualified type name */,
    const char_t* method_name /* Public static method name compatible with delegateType */,
    const char_t* delegate_type_name /* Assembly qualified delegate type name or null
                                        or UNMANAGEDCALLERSONLY_METHOD if the method is marked with
                                        the UnmanagedCallersOnlyAttribute. */
    ,
    void*          reserved /* Extensibility parameter (currently unused and must be 0) */,
    /*out*/ void** delegate /* Pointer where to store the function pointer result */);

// Signature of delegate returned by load_assembly_and_get_function_pointer_fn when delegate_type_name == null (default)
using component_entry_point_fn = int(CORECLR_DELEGATE_CALLTYPE*)(void* arg, int32_t arg_size_in_bytes);

using get_function_pointer_fn = int(CORECLR_DELEGATE_CALLTYPE*)(
    const char_t* type_name /* Assembly qualified type name */,
    const char_t* method_name /* Public static method name compatible with delegateType */,
    const char_t* delegate_type_name /* Assembly qualified delegate type name or null,
                                        or UNMANAGEDCALLERSONLY_METHOD if the method is marked with
                                        the UnmanagedCallersOnlyAttribute. */
    ,
    void*          load_context /* Extensibility parameter (currently unused and must be 0) */,
    void*          reserved /* Extensibility parameter (currently unused and must be 0) */,
    /*out*/ void** delegate /* Pointer where to store the function pointer result */);

#endif // __CORECLR_DELEGATES_H__
