
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The Purpose Of This Script Is:
//
//  Allows to generate data assets of the entity's stun state.
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

namespace SrCatknight.Scripts.Enemies.Data
{
    [CreateAssetMenu(fileName = "newEntityStunStateData",
        menuName = "Sr.Catknight Data/Entity Data/7. Stun State", order = 7)]
    
    public class DataEntityStunState : ScriptableObject
    {
        [Header("Entity Stun Settings")] [Space(5)]
        [Tooltip("Time that the entity will be stunned")]
        [Range(0.5f, 3.0f)][SerializeField] private float stunTime = 2f;
        [Tooltip("KnockBack duration when the entity is stunned")]
        [SerializeField] private float stunKnockBackTime = 0.2f;
        [Tooltip("KnockBack speed when the entity is stunned")]
        [SerializeField] private float stunKnockBackSpeed = 20f;
        [Tooltip("KnockBack angle when the entity is stunned")]
        [SerializeField] private Vector2 stunKnockBackAngle;
        
        // Expression body properties.
        public float StunTime => stunTime;
        public float StunKnockBackTime => stunKnockBackTime;
        public float StunKnockBackSpeed => stunKnockBackSpeed;
        public Vector2 StunKnockBackAngle => stunKnockBackAngle;
    }
}
