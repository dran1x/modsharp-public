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

#ifndef MS_ROOT_PROTOBUF_PROXY_H
#define MS_ROOT_PROTOBUF_PROXY_H

#include <cstdint>
#include <string>

namespace google::protobuf
{
class Message;
}

bool ProtobufHasField(const google::protobuf::Message* message, const char* field);
int  ProtobufGetFieldRepeatedCount(const google::protobuf::Message* message, const char* field);

bool ProtobufReadFieldBool(const google::protobuf::Message* message, const char* field, bool* out, int repeatedIndex = -1);
bool ProtobufReadFieldEnum(const google::protobuf::Message* message, const char* field, int32_t* out, int repeatedIndex = -1);
bool ProtobufReadFieldInt32(const google::protobuf::Message* message, const char* field, int32_t* out, int repeatedIndex = -1);
bool ProtobufReadFieldUInt32(const google::protobuf::Message* message, const char* field, uint32_t* out, int repeatedIndex = -1);
bool ProtobufReadFieldFloat(const google::protobuf::Message* message, const char* field, float* out, int repeatedIndex = -1);
bool ProtobufReadFieldDouble(const google::protobuf::Message* message, const char* field, double* out, int repeatedIndex = -1);
bool ProtobufReadFieldInt64(const google::protobuf::Message* message, const char* field, int64_t* out, int repeatedIndex = -1);
bool ProtobufReadFieldUInt64(const google::protobuf::Message* message, const char* field, uint64_t* out, int repeatedIndex = -1);
bool ProtobufReadFieldString(const google::protobuf::Message* message, const char* field, std::string* out, int repeatedIndex = -1);
bool ProtobufReadFieldBytes(const google::protobuf::Message* message, const char* field, std::string* out, int repeatedIndex = -1);

bool ProtobufSetFieldBool(google::protobuf::Message* message, const char* field, bool value, int repeatedIndex = -1);
bool ProtobufSetFieldEnum(google::protobuf::Message* message, const char* field, int32_t value, int repeatedIndex = -1);
bool ProtobufSetFieldInt32(google::protobuf::Message* message, const char* field, int32_t value, int repeatedIndex = -1);
bool ProtobufSetFieldUInt32(google::protobuf::Message* message, const char* field, uint32_t value, int repeatedIndex = -1);
bool ProtobufSetFieldFloat(google::protobuf::Message* message, const char* field, float value, int repeatedIndex = -1);
bool ProtobufSetFieldDouble(google::protobuf::Message* message, const char* field, double value, int repeatedIndex = -1);
bool ProtobufSetFieldInt64(google::protobuf::Message* message, const char* field, int64_t value, int repeatedIndex = -1);
bool ProtobufSetFieldUInt64(google::protobuf::Message* message, const char* field, uint64_t value, int repeatedIndex = -1);
bool ProtobufSetFieldString(google::protobuf::Message* message, const char* field, const char* value, int repeatedIndex = -1);
bool ProtobufSetFieldBytes(google::protobuf::Message* message, const char* field, const std::string& value, int repeatedIndex = -1);

int ProtobufAddFieldBool(google::protobuf::Message* message, const char* field, bool value);
int ProtobufAddFieldEnum(google::protobuf::Message* message, const char* field, int32_t value);
int ProtobufAddFieldInt32(google::protobuf::Message* message, const char* field, int32_t value);
int ProtobufAddFieldUInt32(google::protobuf::Message* message, const char* field, uint32_t value);
int ProtobufAddFieldFloat(google::protobuf::Message* message, const char* field, float value);
int ProtobufAddFieldDouble(google::protobuf::Message* message, const char* field, double value);
int ProtobufAddFieldInt64(google::protobuf::Message* message, const char* field, int64_t value);
int ProtobufAddFieldUInt64(google::protobuf::Message* message, const char* field, uint64_t value);
int ProtobufAddFieldString(google::protobuf::Message* message, const char* field, const char* value);
int ProtobufAddFieldBytes(google::protobuf::Message* message, const char* field, const std::string& value);

google::protobuf::Message* ProtobufReadFieldMessage(google::protobuf::Message* message, const char* field, int repeatedIndex = -1);
google::protobuf::Message* ProtobufAddRepeatedFieldMessage(google::protobuf::Message* message, const char* field);
bool                       ProtobufRemoveNetMessageRepeatedField(google::protobuf::Message* message, const char* field, int repeatedIndex);
bool                       ProtobufCopyFromOtherMessage(google::protobuf::Message* message, uint8_t* data, int32_t size);

#endif // MS_ROOT_PROTOBUF_PROXY_H
