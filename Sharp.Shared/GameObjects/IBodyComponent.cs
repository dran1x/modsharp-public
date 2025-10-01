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

using Sharp.Shared.Attributes;
using Sharp.Shared.CStrike;
using Sharp.Shared.GameEntities;
using Sharp.Shared.Types;

namespace Sharp.Shared.GameObjects;

[NetClass("CBodyComponent")]
public interface IBodyComponent : IEntityComponent
{
    IGameSceneNode? GetSceneNode();

    /// <summary>
    ///     强制转换为 <see cref="IBodyComponentSkeletonInstance" /> 实例, 不验证任何信息
    /// </summary>
    IBodyComponentSkeletonInstance AsBodyComponentSkeletonInstance { get; }
}

[NetClass("CBodyComponentSkeletonInstance")]
public interface IBodyComponentSkeletonInstance : IBodyComponent
{
    ISkeletonInstance? GetSkeletonInstance();
}

[NetClass("CGameSceneNode")]
public interface IGameSceneNode : ISchemaObject
{
    IGameSceneNode? GetParent();

    IGameSceneNode? GetChild();

    IGameSceneNode? GetNextSibling();

    IBaseEntity? GetOwner();

    Vector AbsOrigin   { get; }
    Vector AbsRotation { get; }
    float  Scale       { get; set; }
    float  AbsScale    { get; set; }

    /// <summary>
    ///     转换为 <see cref="ISkeletonInstance" />
    /// </summary>
    ISkeletonInstance? AsSkeletonInstance { get; }
}

[NetClass("CSkeletonInstance")]
public interface ISkeletonInstance : IGameSceneNode
{
    IModelState GetModelState();

    bool IsAnimationEnabled { get; set; }

    bool DisableSolidCollisionsForHierarchy { get; set; }

    uint MaterialGroup { get; set; }
}

[NetClass("CModelState")]
public interface IModelState : INativeObject
{
    string ModelName { get; }

    ulong MeshGroupMask { get; }
}
