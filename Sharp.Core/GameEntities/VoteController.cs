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

using Sharp.Generator;
using Sharp.Shared;
using Sharp.Shared.GameEntities;

namespace Sharp.Core.GameEntities;

internal partial class VoteController : BaseEntity, IVoteController
{
#region Schemas

    [NativeSchemaField("CVoteController", "m_iOnlyTeamToVote", typeof(int))]
    private partial SchemaField GetOnlyTeamToVoteField();

    [NativeSchemaField("CVoteController", "m_nVoteOptionCount", typeof(int[]))]
    private partial SchemaField GetVoteOptionCountField();

    [NativeSchemaField("CVoteController", "m_nPotentialVotes", typeof(int))]
    private partial SchemaField GetPotentialVotesField();

    [NativeSchemaField("CVoteController", "m_bIsYesNoVote", typeof(bool))]
    private partial SchemaField GetIsYesNoVoteField();

    [NativeSchemaField("CVoteController", "m_nVotesCast", typeof(int[]))]
    private partial SchemaField GetVotesCastField();

#endregion
}
