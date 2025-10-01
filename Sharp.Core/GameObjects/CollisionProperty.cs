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

using Sharp.Core.Attributes;
using Sharp.Core.CStrike;
using Sharp.Generator;
using Sharp.Shared;
using Sharp.Shared.Enums;
using Sharp.Shared.GameObjects;
using Sharp.Shared.Types;

namespace Sharp.Core.GameObjects;

[EntityObjectInfo(8)] // m_pOuter  0x8
internal partial class CollisionProperty : EntityObject, ICollisionProperty
{
    private readonly VPhysicsCollisionAttribute _attribute;

    private CollisionProperty(nint ptr) : base(ptr)
        => _attribute = (VPhysicsCollisionAttribute) GetCollisionAttribute();

    protected override void OnDisposed()
    {
        _attribute.MarkAsDisposed();
        base.OnDisposed();
    }

    public IVPhysicsCollisionAttribute CollisionAttribute
    {
        get
        {
            CheckDisposed();

            return _attribute;
        }
    }

#region Schemas

    [NativeSchemaField("CCollisionProperty", "m_nSolidType", typeof(SolidType), IsStruct = true)]
    private partial SchemaField GetSolidTypeField();

    [NativeSchemaField("CCollisionProperty", "m_nEnablePhysics", typeof(byte), IsStruct = true)]
    private partial SchemaField GetEnablePhysicsField();

    [NativeSchemaField("CCollisionProperty", "m_usSolidFlags", typeof(byte), IsStruct = true)]
    private partial SchemaField GetSolidFlagsField();

    [NativeSchemaField("CCollisionProperty", "m_CollisionGroup", typeof(CollisionGroupType), IsStruct = true)]
    private partial SchemaField GetCollisionGroupField();

    [NativeSchemaField("CCollisionProperty",
                       "m_collisionAttribute",
                       typeof(VPhysicsCollisionAttribute),
                       InlineObject = true)]
    private partial SchemaField GetCollisionAttributeField();

    [NativeSchemaField("CCollisionProperty", "m_vecMins", typeof(Vector), IsStruct = true)]
    private partial SchemaField GetMinsField();

    [NativeSchemaField("CCollisionProperty", "m_vecMaxs", typeof(Vector), IsStruct = true)]
    private partial SchemaField GetMaxsField();

    [NativeSchemaField("CCollisionProperty", "m_flBoundingRadius", typeof(float), IsStruct = true)]
    private partial SchemaField GetBoundingRadiusField();

    [NativeSchemaField("CCollisionProperty", "m_nSurroundType", typeof(SurroundingBoundsType), IsStruct = true)]
    private partial SchemaField GetSurroundTypeField();

#endregion

    public override string GetSchemaClassname()
        => "CCollisionProperty";
}

internal partial class VPhysicsCollisionAttribute : SchemaObject, IVPhysicsCollisionAttribute
{
#region Schemas

    [NativeSchemaField("VPhysicsCollisionAttribute_t",
                       "m_nCollisionGroup",
                       typeof(CollisionGroupType),
                       IsStruct = true)]
    private partial SchemaField GetCollisionGroupField();

    [NativeSchemaField("VPhysicsCollisionAttribute_t",
                       "m_nEntityId",
                       typeof(uint),
                       IsStruct = true)]
    private partial SchemaField GetEntityIdField();

    [NativeSchemaField("VPhysicsCollisionAttribute_t",
                       "m_nOwnerId",
                       typeof(uint),
                       IsStruct = true)]
    private partial SchemaField GetOwnerIdField();

    [NativeSchemaField("VPhysicsCollisionAttribute_t",
                       "m_nHierarchyId",
                       typeof(ushort),
                       IsStruct = true)]
    private partial SchemaField GetHierarchyIdField();

    [NativeSchemaField("VPhysicsCollisionAttribute_t",
                       "m_nInteractsAs",
                       typeof(InteractionLayers),
                       IsStruct = true)]
    private partial SchemaField GetInteractsAsField();

    [NativeSchemaField("VPhysicsCollisionAttribute_t",
                       "m_nInteractsWith",
                       typeof(InteractionLayers),
                       IsStruct = true)]
    private partial SchemaField GetInteractsWithField();

    [NativeSchemaField("VPhysicsCollisionAttribute_t",
                       "m_nInteractsExclude",
                       typeof(InteractionLayers),
                       IsStruct = true)]
    private partial SchemaField GetInteractsExcludeField();

#endregion

    public override string GetSchemaClassname()
        => "VPhysicsCollisionAttribute_t";
}
