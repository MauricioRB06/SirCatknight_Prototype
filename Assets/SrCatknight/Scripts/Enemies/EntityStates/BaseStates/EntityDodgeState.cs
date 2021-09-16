
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The purpose of this script is:
//
//   Generate a behavior for the entity, that allows it to dodge at the player..
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
    public class EntityDodgeState : EntityState
    {
        // Reference to the dodge state data.
        private readonly DataEntityDodgeState _dataEntityDodgeState;
        
        // To detect if the player is too close.
        protected bool PerformCloseRangeAction;
        
        // To detect if the player is in the maximum aggro range.
        protected bool IsPlayerInMaxAgroRange;
        
        // To find out if the entity has touched the ground.
        protected bool IsGrounded;
        
        // To find out if the dodge has been completed.
        protected bool IsDodgeOver;
        
        // Class constructor.
        protected EntityDodgeState(EntityController entityController, EntityStateMachine entityStateMachine,
            string animationBoolName, DataEntityDodgeState dataEntityDodgeState)
            : base(entityController, entityStateMachine, animationBoolName)
        {
            this._dataEntityDodgeState = dataEntityDodgeState;
        }
        
        // 
        protected override void DoChecks()
        {
            base.DoChecks();

            PerformCloseRangeAction = EntityController.CheckPlayerInCloseRangeAction();
            IsPlayerInMaxAgroRange = EntityController.CheckPlayerInMaxAggroRange();
            IsGrounded = Core.CollisionSenses.Ground;
        }
        
        // 
        public override void Enter()
        {
            base.Enter();

            IsDodgeOver = false;

            Core.Movement.SetVelocity(_dataEntityDodgeState.DodgeSpeed, _dataEntityDodgeState.DodgeAngle,
                -Core.Movement.FacingDirection);
        }
        
        // 
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            // 
            if (Time.time >= StartTime + _dataEntityDodgeState.DodgeTime && IsGrounded)
            {
                IsDodgeOver = true;
            }
        }
    }
}
