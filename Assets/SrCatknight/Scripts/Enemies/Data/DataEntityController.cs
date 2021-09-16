
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The Purpose Of This Script Is:
//
//  Allows to generate data assets of the entity's controller.
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
    [CreateAssetMenu(fileName = "newEntityControllerData",
        menuName = "Sr.Catknight Data/Entity Data/1. Controller Data", order = 1)]
    
    public class DataEntityController : ScriptableObject
    {
        [Header("Health Settings")][Space(5)]
        [Tooltip("Maximum health of the entity")]
        [Range(5.0f, 1000.0f)][SerializeField] private float maxHealth = 20f;
        [Space(15)]
        
        [Header("Damage Hop Settings")][Space(5)]
        [Tooltip("Force with which the entity will jump when it receives damage")]
        [Range(5.0f, 20.0f)][SerializeField] private float damageHopSpeed = 10f;
        [Space(15)]
        
        [Header("Verification distances of the environment")][Space(5)]
        [Tooltip("Verification radius in which the entity will try to detect the walls")]
        [SerializeField] private float wallCheckDistance = 0.2f;
        [Tooltip("Verification radius in which the entity will try to detect the ledges")]
        [SerializeField] private float ledgeCheckDistance = 0.4f;
        [Tooltip("Verification radius in which the entity will try to detect the ground")]
        [SerializeField] private float groundCheckRadius = 0.3f;
        [Space(15)]
        
        [Header("Ranges for player detection")][Space(5)]
        [Tooltip("x")]
        [SerializeField] private float minAggroDistance = 3f;
        [Tooltip("x")]
        [SerializeField] private float maxAggroDistance = 4f;
        [Space(15)]
        
        [Header("Parameters to be stunned")][Space(5)]
        [Tooltip("How much damage the entity must receive in order to be stunned")]
        [SerializeField] private float stunResistance = 3f;
        [Tooltip("x")]
        [SerializeField] private float stunRecoveryTime = 2f;
        [Space(15)]
        
        [Header("Distance that is considered very close to the player")][Space(5)]
        [Tooltip("Set the distance at which the entity will consider the player to be too close")]
        [SerializeField] private float closeRangeActionDistance = 1f;
        [Space(15)]
        
        [Header("Particles when damage is received")][Space(5)]
        [Tooltip("x")]
        [SerializeField] private GameObject hitParticles;
        [Space(15)]
        
        [Header("Layers on which detection is performed")][Space(5)]
        [Tooltip("x")]
        [SerializeField] private LayerMask layerGroundAndWalls;
        [Tooltip("Layer in which the player will be searched")]
        [SerializeField] private LayerMask layerPlayer;
        
        // Expression body properties.
        public float MaxHealth => maxHealth;
        public float DamageHopSpeed => damageHopSpeed;
        public float WallCheckDistance => wallCheckDistance;
        public float LedgeCheckDistance => ledgeCheckDistance;
        public float GroundCheckRadius => groundCheckRadius;
        public float MINAggroDistance => minAggroDistance;
        public float MAXAggroDistance => maxAggroDistance;
        public float StunResistance => stunResistance;
        public float StunRecoveryTime => stunRecoveryTime;
        public float CloseRangeActionDistance => closeRangeActionDistance;
        public GameObject HitParticles => hitParticles;
        public LayerMask LayerGroundAndWalls => layerGroundAndWalls;
        public LayerMask LayerPlayer => layerPlayer;
    }
}
