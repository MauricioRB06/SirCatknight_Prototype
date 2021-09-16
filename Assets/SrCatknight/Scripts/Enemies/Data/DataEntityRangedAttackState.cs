
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The Purpose Of This Script Is:
//
//  Allows to generate data assets of the entity's ranged attack state.
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
    [CreateAssetMenu(fileName = "newEntityRangedAttackStateData",
        menuName = "Sr.Catknight Data/Entity Data/10. Ranged Attack State", order = 10)]
    
    public class DataEntityRangedAttackState : ScriptableObject
    {
        [Header("Entity Ranged Attack Settings")] [Space(5)]
        [Tooltip("Insert an ProjectilePrefab here")]
        [SerializeField] private GameObject projectile;
        [Tooltip("The damage to be caused by the projectile")]
        [SerializeField] private float projectileDamage = 10f;
        [Tooltip("The velocity that the projectile will have")]
        [SerializeField] private float projectileSpeed = 12f;
        [Tooltip("Distance traveled by the projectile before it is affected by gravity")]
        [SerializeField] private float projectileTravelDistance;
        
        // Expression body properties.
        public GameObject Projectile => projectile;
        public float ProjectileDamage => projectileDamage;
        public float ProjectileSpeed => projectileSpeed;
        public float ProjectileTravelDistance => projectileTravelDistance;
    }
}
