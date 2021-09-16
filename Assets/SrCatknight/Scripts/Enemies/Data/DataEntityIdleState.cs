
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The Purpose Of This Script Is:
//
//  Allows to generate data assets of the entity's idle state.
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
    [CreateAssetMenu(fileName = "newEntityIdleStateData",
        menuName = "Sr.Catknight Data/Entity Data/2. Idle State", order = 2)]
    
    public class DataEntityIdleState : ScriptableObject
    {
        [Header("Entity Idle State Settings")] [Space(5)]
        [Range(0.5f, 2.0f)][Tooltip("Minimum idle state time")]
        [SerializeField] private float minIdleTime = 1f;
        [Range(2.0f, 4.0f)][Tooltip("Maximum idle state time")]
        [SerializeField] private float maxIdleTime = 2f;
        
        // Expression body properties
        public float MinIdleTime => minIdleTime;
        public float MaxIdleTime => maxIdleTime;
    }
}
