
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The Purpose Of This Script Is:
//
//  To be the basis for generating the different states that the player can take.
// 
//  Documentation and References:
//
//  C# Protected: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/protected
//  C# Read only: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/readonly
//  C# Polymorphism: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/virtual

using Enemies.Data;
using SrCatknight.Scripts.Enemies.Data;
using SrCatknight.Scripts.Intermediaries;
using SrCatknight.Scripts.StateMachine;
using UnityEngine;

namespace Enemies
{
    public class EntityController : MonoBehaviour
    {
        [Header("Entity Data Controller")] [Space(5)]
        [Tooltip("Insert an entity controller data asset here")]
        [SerializeField] private DataEntityController entityControllerData;
        [Space(15)]
        
        [Header("Entity Checkers")] [Space(5)]
        [Tooltip("Insert here the verification axis for the entity's wall detection")]
        [SerializeField] private Transform wallCheck;
        [Tooltip("Insert here the verification axis for the entity's ledge detection")]
        [SerializeField] private Transform ledgeCheck;
        [Tooltip("Insert here the verification axis for the entity's ground detection")]
        // 
        [SerializeField] private Transform groundCheck;
        [Tooltip("Insert here the verification axis for the entity's player detection")]
        [SerializeField] private Transform playerCheck;
        
        // To stored the state machine for the entity.
        protected EntityStateMachine EntityStateMachine;
        
        // Intermediary that will be in charge of connecting the functions to be executed from the animator.
        public AnimationToEntityStateMachine AnimationToEntityStateMachine { get; private set; }
        
        // Stores the address of the last stroke, to be available if you decide to use it.
        public int LastDamageDirection { get; private set; }
        
        // Reference to the entity's core.
        public SrCatknight.Scripts.Core.Core Core { get; private set; }
        
        // Reference to the entity's animator.
        public Animator EntityAnimator { get; private set; }   
        
        // To know the current health of the entity.
        private float currentHealth;
        
        // To find out how much is the current stun resistance of the entity.
        private float currentStunResistance;
        
        // To know the last instant in which the entity received damage.
        private float lastDamageTime;
        
        // It is used each time you want to create a vector, so instead of creating a vector 2 each time,
        // only the values of this vector will be changed.
        private Vector2 velocityWorkspace;
        
        // To know if the entity has been stunned.
        protected bool IsStunned;
        
        // To find out if the entity is dead.
        protected bool IsDead;
        
        // ID parameters for the animator.
        private static readonly int YVelocity = Animator.StringToHash("yVelocity");
        
        // 
        public virtual void Awake()
        {
            Core = GetComponentInChildren<SrCatknight.Scripts.Core.Core>();
            EntityAnimator = GetComponent<Animator>();
            AnimationToEntityStateMachine = GetComponent<AnimationToEntityStateMachine>();
            
            EntityStateMachine = new EntityStateMachine();
            
            currentHealth = entityControllerData.MaxHealth;
            currentStunResistance = entityControllerData.StunResistance;   
        }
        
        // 
        public virtual void Update()
        {
            // Each time it is updated, it will call the update of the current state of the entity.
            EntityStateMachine.CurrentState.LogicUpdate();
            
            // Sends the entity's animator the Y velocity, in case the dodge mechanic is used.
            EntityAnimator.SetFloat(YVelocity, Core.Movement.RigidBody2D.velocity.y);
            
            // Restores the resistance of the entity to be stunned if its cooldown time has passed.
            if (Time.time >= lastDamageTime + entityControllerData.StunRecoveryTime)
            {
                ResetStunResistance();
            }
        }
        
        // In each FixedUpdate, it calls the respective PhysicsUpdate of the current state of the entity.
        public virtual void FixedUpdate()
        {
            // 
            Core.PhysicsUpdate();
            
            EntityStateMachine.CurrentState.PhysicsUpdate();
        }
        
        // Performs the smallest distance verification.
        public virtual bool CheckPlayerInMinAggroRange()
        {
            return Physics2D.Raycast(playerCheck.position, transform.right, 
                entityControllerData.MINAggroDistance, entityControllerData.LayerPlayer);
        }
        
        // Performs the verification of the largest distance.
        public virtual bool CheckPlayerInMaxAggroRange()
        {
            return Physics2D.Raycast(playerCheck.position, transform.right,
                entityControllerData.MAXAggroDistance, entityControllerData.LayerPlayer);
        }
        
        // Check if the player is very close to the entity.
        public virtual bool CheckPlayerInCloseRangeAction()
        {
            return Physics2D.Raycast(playerCheck.position, transform.right,
                entityControllerData.CloseRangeActionDistance, entityControllerData.LayerPlayer);
        }
        
        // Causes the entity to jump when it receives damage.
        public virtual void DamageHop(float velocity)
        {
            velocityWorkspace.Set(Core.Movement.RigidBody2D.velocity.x, velocity);
            Core.Movement.RigidBody2D.velocity = velocityWorkspace;
        }
        
        // When the reset time has elapsed, the entity's resistance to being stunned is restored.
        public virtual void ResetStunResistance()
        {
            IsStunned = false;
            currentStunResistance = entityControllerData.StunResistance;
        }
        
        // (Delete this in the final build) Reference gizmos to see the parameters of the entity.
        public virtual void OnDrawGizmos()
        {
            if (Core == null) return;
            
            // Draw the verification distance of the walls.
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(Vector2.right * 
                            Core.Movement.FacingDirection * entityControllerData.WallCheckDistance));
            
            // Draw the verification distance of the ledges.
            Gizmos.DrawLine(ledgeCheck.position,
                ledgeCheck.position + (Vector3)(Vector2.down * entityControllerData.LedgeCheckDistance));
            
            // Draw the area in which the player is considered to be very close.
            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right
                                  * entityControllerData.CloseRangeActionDistance), 0.2f);
            
            // Draw the zone where the minimum aggro range is.
            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right
                                  * entityControllerData.MINAggroDistance), 0.2f);
            
            // Draw the zone where the maximum aggro range is.
            Gizmos.DrawWireSphere(playerCheck.position + (Vector3)(Vector2.right
                                  * entityControllerData.MAXAggroDistance), 0.2f);
        }
    }
}
