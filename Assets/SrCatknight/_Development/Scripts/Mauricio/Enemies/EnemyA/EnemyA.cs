
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The Purpose Of This Script Is:
//
//  To be the basis for generating the different states that the entities can take.
// 
//  Documentation and References:
//
//  C# Protected: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/protected
//  C# Read only: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/readonly
//  C# Polymorphism: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/virtual
//  C# Polymorphism: https://www.youtube.com/watch?v=XzKL94OMDV4&list=PLU8oAlHdN5BmpIQGDSHo5e1r4ZYWQ8m4B&index=46 [ Spanish ]

using Enemies;
using Enemies.Data;
using SrCatknight.Scripts.Enemies.Data;
using UnityEngine;

namespace _Development.Scripts.Mauricio.Enemies.EnemyA
{
    public class EnemyA : EntityController
    {
        [Space(15)]
        [Header("Entity Settings Data")] [Space(5)]
        [Tooltip("Insert an entity idle state data asset here")]
        [SerializeField] private DataEntityIdleState idleStateData;
        [Tooltip("Insert an entity move state data asset here")]
        [SerializeField] private DataEntityMoveState moveStateData;
        [Tooltip("Insert an entity player detection state data asset here")]
        [SerializeField] private DataEntityPlayerDetection playerDetectionData;
        [Tooltip("Insert an entity look for payer state data asset here")]
        [SerializeField] private DataEntityLookForPlayer lookForPlayerStateData;
        [Tooltip("Insert an entity charge state state data asset here")]
        [SerializeField] private DataEntityChargeState chargeStateData;
        [Tooltip("Insert an entity stun state data asset here")]
        [SerializeField] private DataEntityStunState stunStateData;
        [Tooltip("Insert an entity dead state data asset here")]
        [SerializeField] private DataEntityDeadState deadStateData;
        [Space(15)]
        
        [Header("Entity Melee Attack Data")] [Space(5)]
        [Tooltip("Set the position in which the attack is to be made")]
        [SerializeField] private Transform meleeAttackPosition;
        [Tooltip("Insert an entity melee attack state data asset here")]
        [SerializeField] private DataEntityMeleeAttack meleeAttackStateData;
        
        // These are the different states that this entity will have.
        public EnemyAIdleState IdleState { get; private set; }
        public EnemyAMoveState MoveState { get; private set; }
        public EnemyAPlayerDetectionState PlayerDetectionState { get; private set; }
        public EnemyALookForPlayerState LookForPlayerState { get; private set; }
        public EnemyAChargeState ChargeState { get; private set; }
        public EnemyAEntityStunState StunState { get; private set; }
        public EnemyADeadState DeadState { get; private set; }
        public EnemyAMeleeAttackState MeleeAttackState { get; private set; }
        
        // Instantiate the objects that will contain the states of enemyA.
        public override void Awake()
        {
            base.Awake();

            MoveState = new EnemyAMoveState(this, EntityStateMachine,
                "Move", moveStateData, this);
            
            IdleState = new EnemyAIdleState(this, EntityStateMachine,
                "Idle", idleStateData, this);
            
            PlayerDetectionState = new EnemyAPlayerDetectionState(this, EntityStateMachine,
                "PlayerDetected", playerDetectionData, this);
            
            ChargeState = new EnemyAChargeState(this, EntityStateMachine,
                "Charge", chargeStateData, this);
            
            LookForPlayerState = new EnemyALookForPlayerState(this, EntityStateMachine,
                "LookForPlayer", lookForPlayerStateData, this);
            
            MeleeAttackState = new EnemyAMeleeAttackState(this, EntityStateMachine,
                "MeleeAttack", meleeAttackPosition, meleeAttackStateData, this);
            
            StunState = new EnemyAEntityStunState(this, EntityStateMachine,
                "Stun", stunStateData, this);
            
            DeadState = new EnemyADeadState(this, EntityStateMachine,
                "Dead", deadStateData, this);
        }
        
        // After being created, this enemyA initializes it's state machine.
        private void Start()
        {
            EntityStateMachine.Initialize(MoveState);        
        }
        
        // (Delete this in the final build) Used to see the range of the enemy's attackA.
        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();

            Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.AttackRadius);
        }
    }
}
