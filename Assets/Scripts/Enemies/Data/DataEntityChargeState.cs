
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The Purpose Of This Script Is:
//
//  Allows to generate data assets of the entity's charge state.
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

namespace Enemies.Data
{
    [CreateAssetMenu(fileName = "newEntityChargeStateData",
        menuName = "Sr.Catknight Data/Entity Data/9. Charge State", order = 9)]
    
    public class DataEntityChargeState : ScriptableObject
    {
        [Header("Entity Charge State Settings")] [Space(5)]
        [Tooltip("Speed at which the entity will pounce on the player")]
        [Range(3.0f, 10.0f)][SerializeField] private float entityChargeSpeed = 6f;
        [Range(1.0f, 5.0f)][Tooltip("The duration of the entity's charge on the player")]
        [SerializeField] private float entityChargeTime = 2f;
        
        // Expression body properties.
        public float EntityChargeSpeed => entityChargeSpeed;
        public float EntityChargeTime => entityChargeTime;
    }
}
