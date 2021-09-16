
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The purpose of this script is:
//
//  Set the actions that EnemyA will take, when it's moving.
// 
//  Documentation and References:
//
//  C# Read only: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/readonly
//  C# Polymorphism: https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/polymorphism

using Enemies;
using Enemies.Data;
using SrCatknight.Scripts.Enemies.Data;
using SrCatknight.Scripts.Enemies.EntityStates.BaseStates;
using SrCatknight.Scripts.StateMachine;

namespace _Development.Scripts.Mauricio.Enemies.EnemyA
{
    public class EnemyAMoveState : EntityMoveState
    {
        // Reference to the entity to which we are going to associate this script.
        private readonly EnemyA enemyA;
        
        // Class constructor.
        public EnemyAMoveState(EntityController entityController, EntityStateMachine entityStateMachine,
            string animationBoolName, DataEntityMoveState dataEntityMoveState, EnemyA enemyA)
            : base(entityController, entityStateMachine, animationBoolName, dataEntityMoveState)
        {
            this.enemyA = enemyA;
        }
        
        // The state changes, depending on the condition.
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            // If the player is within the minimum detection range.
            if (IsPlayerInMinAggroRange)
            {
                EntityStateMachine.ChangeState(enemyA.PlayerDetectionState);
            }
            // If a wall is detected during moving or a ledge is undetected.
            else if(IsDetectingWall || !IsDetectingLedge)
            {
                enemyA.IdleState.SetFlipAfterIdleState(true);
                EntityStateMachine.ChangeState(enemyA.IdleState);
            }
        }
    }
}
