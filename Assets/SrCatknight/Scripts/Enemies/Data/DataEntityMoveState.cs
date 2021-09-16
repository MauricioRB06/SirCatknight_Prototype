
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The Purpose Of This Script Is:
//
//  Allows to generates data assets of the entity's move state.
//
//  Documentation and References:
//
//  Unity ScriptableObject: https://docs.unity3d.com/ScriptReference/ScriptableObject.html
//  Unity Header: https://docs.unity3d.com/ScriptReference/HeaderAttribute.html
//  Unity Tooltip: https://docs.unity3d.com/ScriptReference/TooltipAttribute.html
//  Unity Space: https://docs.unity3d.com/ScriptReference/SpaceAttribute.html
//  Unity SerializeField: https://docs.unity3d.com/2021.1/Documentation/ScriptReference/SerializeField.html
//  Unity Range: https://docs.unity3d.com/ScriptReference/RangeAttribute.html
//  Unity CreateAssetMenu: https://docs.unity3d.com/2021.1/Documentation/ScriptReference/CreateAssetMenuAttribute.html
// 
//  C# Properties: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/properties

using UnityEngine;

namespace SrCatknight.Scripts.Enemies.Data
{
    [CreateAssetMenu(fileName = "newEntityMoveStateData",
        menuName = "Sr.Catknight Data/Entity Data/3. Move State", order = 3)]
    
    public class DataEntityMoveState : ScriptableObject
    {
        [Header("Entity Move State Settings")] [Space(5)]
        [Tooltip("Speed at which the entity will normally move")]
        [SerializeField] private float movementSpeed = 3f;
        
        // Expression body properties
        public float MovementSpeed => movementSpeed;
    }
}
