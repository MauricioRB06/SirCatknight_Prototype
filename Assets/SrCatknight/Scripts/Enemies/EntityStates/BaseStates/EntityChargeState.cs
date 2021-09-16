
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The purpose of this script is:
//
//  Generate a behavior for the entity, that allows it to charge at the player.
//  
//  Documentation and References:
//  
//  C# Read only: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/readonly
//  C# Polymorphism: https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/polymorphism

using Enemies;
using Enemies.Data;
using SrCatknight.Scripts.StateMachine;
using UnityEngine;

namespace SrCatknight.Scripts.Enemies.EntityStates.BaseStates
{
    public class EntityChargeState : EntityState
    {
        // Reference to charge state data.
        private readonly DataEntityChargeState _dataEntityChargeState;
        
        // To verify if the player is within the minimum aggro range.
        protected bool IsPlayerInMinAgroRange;
        
        // To verify if the entity is detecting a ledge.
        protected bool IsDetectingLedge;
        
        // To verify if the entity has touched a wall.
        protected bool IsDetectingWall;
        
        // To check if the charging time has expired.
        protected bool IsChargeTimeOver;
        
        // To know when to perform a close-range action.
        protected bool PerformCloseRangeAction;
        
        // Class constructor.
        protected EntityChargeState(EntityController entityController, EntityStateMachine entityStateMachine,
            string animationBoolName, DataEntityChargeState dataEntityChargeState)
            : base(entityController, entityStateMachine, animationBoolName)
        {
            this._dataEntityChargeState = dataEntityChargeState;
        }
        
        // Checks of this state.
        protected override void DoChecks()
        {
            base.DoChecks();

            IsPlayerInMinAgroRange = EntityController.CheckPlayerInMinAggroRange();
            IsDetectingLedge = Core.CollisionSenses.LedgeVertical;
            IsDetectingWall = Core.CollisionSenses.WallFront;

            PerformCloseRangeAction = EntityController.CheckPlayerInCloseRangeAction();
        }
        
        // When this state is entered, movement is initiated against the player based on the state data.
        public override void Enter()
        {
            base.Enter();

            IsChargeTimeOver = false;
            Core.Movement.SetVelocityX(_dataEntityChargeState.EntityChargeSpeed * Core.Movement.FacingDirection);
        }
        
        // As long as it remains in this state and the time has not expired, the entity will continue to move.
        public override void LogicUpdate()
        {
            base.LogicUpdate();

            Core.Movement.SetVelocityX(_dataEntityChargeState.EntityChargeSpeed * Core.Movement.FacingDirection);

            if (Time.time >= StartTime + _dataEntityChargeState.EntityChargeTime)
            {
                IsChargeTimeOver = true;
            }
        }
    }
}
