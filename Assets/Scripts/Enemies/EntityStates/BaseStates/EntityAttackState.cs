
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The purpose of this script is:
//
//  Generate a behavior for the entity, that allows it to attack the player.
//  
//  Documentation and References:
//  
//  C# Read only: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/readonly
//  C# Polymorphism: https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/polymorphism

using StateMachine;
using UnityEngine;

namespace Enemies.EntityStates.BaseStates
{
    public class EntityAttackState : EntityState
    {
        // Position from which the attack is to be made.
        protected readonly Transform AttackPosition;
        
        // It will indicate when the animation is finished.
        protected bool IsAnimationFinished;
        
        // To check if the player is in the minimum aggro range.
        protected bool IsPlayerInMinimumAgroRange;
        
        // Class constructor.
        protected EntityAttackState(EntityController entityController, EntityStateMachine entityStateMachine,
            string animationBoolName, Transform attackPosition)
            : base(entityController, entityStateMachine, animationBoolName)
        {
            AttackPosition = attackPosition;
        }
        
        // Checks for this state.
        protected override void DoChecks()
        {
            base.DoChecks();

            IsPlayerInMinimumAgroRange = EntityController.CheckPlayerInMinAggroRange();
        }
        
        // When entering this state, the entity is stopped and the initial settings are made.
        public override void Enter()
        {
            base.Enter();
            
            Core.Movement.SetVelocityX(0f);
            
            IsAnimationFinished = false;
            
            // Send this state to the intermediary to allow it to execute the functions from the animator.
            EntityController.AnimationToEntityStateMachine.AttackState = this;
        }
        
        // To keep the entity detained while performing the attack.
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            Core.Movement.SetVelocityX(0f);
        }
        
        // (It is used from the animator) To perform the attack.
        public virtual void TriggerAttack()
        {
        }
        
        // (It is used from the animator) To tell the attack that it is finished and can move to another state.
        public virtual void FinishAttack()
        {
            IsAnimationFinished = true;
        }
    }
}
