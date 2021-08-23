
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The Purpose Of This Script Is:
//
//  Allows to generate data assets of the entity's dodge state.
// 
//  Documentation and References:
//
//  Unity ScriptableObject: https://docs.unity3d.com/ScriptReference/ScriptableObject.html
//  Unity Header: https://docs.unity3d.com/ScriptReference/HeaderAttribute.html
//  Unity Tooltip: https://docs.unity3d.com/ScriptReference/TooltipAttribute.html
//  Unity Space: https://docs.unity3d.com/ScriptReference/SpaceAttribute.html
//  Unity SerializeField: https://docs.unity3d.com/2021.1/Documentation/ScriptReference/SerializeField.html
//  Unity CreateAssetMenu: https://docs.unity3d.com/2021.1/Documentation/ScriptReference/CreateAssetMenuAttribute.html
// 
//  C# Properties: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/properties

using UnityEngine;

namespace Enemies.Data
{
    [CreateAssetMenu(fileName = "newEntityDodgeStateData",
        menuName = "Sr.Catknight Data/Entity Data/11. Dodge State", order = 11)]
    
    public class DataEntityDodgeState : ScriptableObject
    {
        [Header("Entity Dodge State Settings")] [Space(5)]
        [Tooltip("Speed with which the dodge will be performed")]
        [SerializeField] private float dodgeSpeed = 10f;
        [Tooltip("Dodge duration time")]
        [SerializeField] private float dodgeTime = 0.2f;
        [Tooltip("Waiting time between each dodge")]
        [SerializeField] private float dodgeCooldown = 2f;
        [Tooltip("Angle at which the dodge will be performed")]
        [SerializeField] private Vector2 dodgeAngle;
        
        // Expression body properties.
        public float DodgeSpeed => dodgeSpeed;
        public float DodgeTime => dodgeTime;
        public float DodgeCooldown => dodgeCooldown;
        public Vector2 DodgeAngle => dodgeAngle;
    }
}
