
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The purpose of this script is:
//
//  Set the actions that EnemyA will take, when it's idle.
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
    public class EnemyAIdleState : EntityIdleState
    {
        // Reference to the entity to which we are going to associate this script.
        private readonly EnemyA enemyA;
        
        // Class constructor.
        public EnemyAIdleState(EntityController entityController, EntityStateMachine entityStateMachine,
            string animationBoolName, DataEntityIdleState dataEntityIdleState, EnemyA enemyA)
            : base(entityController, entityStateMachine, animationBoolName, dataEntityIdleState)
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
            // If the idle status time is over.
            else if (IsIdleTimeOver)
            {
                EntityStateMachine.ChangeState(enemyA.MoveState);
            }
        }
    }
}
