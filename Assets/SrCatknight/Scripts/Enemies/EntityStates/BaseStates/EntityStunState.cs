
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The Purpose Of This Script Is:
//
//  Generate a behavior for the entity, that allows it to be stunned.
// 
//  Documentation and References:
//
//  C# Read only: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/readonly
//  C# Polymorphism: https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/polymorphism

using Enemies;
using SrCatknight.Scripts.Enemies.Data;
using SrCatknight.Scripts.StateMachine;
using UnityEngine;

namespace SrCatknight.Scripts.Enemies.EntityStates.BaseStates
{
    public class EntityStunState : EntityState
    {
        // Reference to stun state data.
        private readonly DataEntityStunState _dataEntityStunState;
        
        // To know if the stunning time is over.
        protected bool IsStunTimeOver;
        
        // To detect whether the entity has touched the ground.
        protected bool IsGrounded;
        
        // To be able to KnockBack the enemy when he is stunned and to know,
        // if he is still moving since the last KnockBack.
        protected bool IsMovementStopped;
        
        // To know if the player is too close.
        protected bool PerformCloseRangeAction;
        
        // To find out if the player is in the minimum aggro range.
        protected bool IsPlayerInMinAgroRange;
        
        // Class constructor.
        protected EntityStunState(EntityController entityController, EntityStateMachine entityStateMachine,
            string animationBoolName, DataEntityStunState dataEntityStunState)
            : base(entityController, entityStateMachine, animationBoolName)
        {
            this._dataEntityStunState = dataEntityStunState;
        }
        
        // 
        protected override void DoChecks()
        {
            base.DoChecks();
            
            IsGrounded = Core.CollisionSenses.Ground;
            PerformCloseRangeAction = EntityController.CheckPlayerInCloseRangeAction();
            IsPlayerInMinAgroRange = EntityController.CheckPlayerInMinAggroRange();
        }
        
        // 
        public override void Enter()
        {
            base.Enter();
            
            IsStunTimeOver = false;
            IsMovementStopped = false;
            Core.Movement.SetVelocity(_dataEntityStunState.StunKnockBackSpeed, _dataEntityStunState.StunKnockBackAngle,
                EntityController.LastDamageDirection);
        }
        
        // 
        public override void Exit()
        {
            base.Exit();
            
            EntityController.ResetStunResistance();
        }
        
        // 
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            // If the stun time is over.
            if (Time.time >= StartTime + _dataEntityStunState.StunTime)
            {
                IsStunTimeOver = true;
            }
            
            // If ground is detected and the KnockBack movement has not been stopped.
            if (IsGrounded && Time.time >= StartTime + _dataEntityStunState.StunKnockBackTime && !IsMovementStopped)
            {
                IsMovementStopped = true;
                Core.Movement.SetVelocityX(0f);
            }
        }
    }
}
