
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The purpose of this script is:
//
//  Generate a behavior for the entity, that allows it to move.
//  
//  Documentation and References:
//  
//  C# Read only: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/readonly
//  C# Polymorphism: https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/polymorphism

using Enemies.Data;
using StateMachine;

namespace Enemies.EntityStates.BaseStates
{
    public class EntityMoveState : EntityState
    {
        // Reference to the move state data.
        private readonly DataEntityMoveState dataEntityMoveState;
        
        // To verify if the entity has touched a wall.
        protected bool IsDetectingWall;
        
        // To verify if the entity is detecting a ledge.
        protected bool IsDetectingLedge;
        
        // To verify if the player is within the minimum aggro range.
        protected bool IsPlayerInMinAggroRange;
        
        // Class constructor.
        protected EntityMoveState(EntityController entityController, EntityStateMachine entityStateMachine,
            string animationBoolName, DataEntityMoveState dataEntityMoveState)
            : base(entityController, entityStateMachine, animationBoolName)
        {
            this.dataEntityMoveState = dataEntityMoveState;
        }
        
        // Checks of this state are performed.
        protected override void DoChecks()
        {
            base.DoChecks();
            
            IsDetectingLedge = Core.CollisionSenses.LedgeVertical;
            IsDetectingWall = Core.CollisionSenses.WallFront;
            IsPlayerInMinAggroRange = EntityController.CheckPlayerInMinAggroRange();
        }
        
        // When we start the state, a velocity is set for the entity.
        public override void Enter()
        {
            base.Enter();
            
            Core.Movement.SetVelocityX(dataEntityMoveState.MovementSpeed * Core.Movement.FacingDirection);
        }
        
        // As long as it remains in the state, a speed will continue to be applied to the entity.
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            Core.Movement.SetVelocityX(dataEntityMoveState.MovementSpeed * Core.Movement.FacingDirection);
        }
    }
}
