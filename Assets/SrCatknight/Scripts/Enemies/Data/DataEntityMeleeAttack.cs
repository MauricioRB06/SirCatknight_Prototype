
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The Purpose Of This Script Is:
//
//  Allows to generate data assets of the entity's melee attack state.
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
    [CreateAssetMenu(fileName = "newEntityMeleeAttackStateData",
        menuName = "Sr.Catknight Data/Entity Data/8. Melee Attack State", order = 8)]
    
    public class DataEntityMeleeAttack : ScriptableObject
    {
        [Header("Entity Melee Attack Settings")] [Space(5)]
        [Tooltip("Sets the attack radius of the entity")]
        [Range(0.5f, 10.0f)][SerializeField] private float attackRadius = 0.5f;
        [Tooltip("Damage to be caused by the entity at each stroke")]
        [Range(5f, 80.0f)][SerializeField] private float attackDamage = 10f;
        [Tooltip("x")]
        [SerializeField] private Vector2 knockbackAngle = Vector2.one;
        [Tooltip("x")]
        [SerializeField] private float knockbackStrength = 10f;
        [Tooltip("Layer where the player will be searched")]
        [SerializeField] private LayerMask layerPlayer;
        
        // Expression body properties.
        public float AttackRadius => attackRadius;
        public float AttackDamage => attackDamage;
        public Vector2 KnockbackAngle => knockbackAngle;
        public float KnockbackStrength => knockbackStrength;
        public LayerMask LayerPlayer => layerPlayer;
    }
}
