
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The purpose of this script is:
//
//  Set the actions that EnemyA will take,
//  It generates a behavior for the entity, when it's stunned.
// 
//  Documentation and References:
//
//  C# Read only: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/readonly
//  C# Polymorphism: https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/polymorphism

using Enemies;
using Enemies.Data;
using Enemies.EntityStates.BaseStates;
using StateMachine;

namespace _Development.Scripts.Mauricio.Enemies.EnemyA
{
    public class EnemyAEntityStunState : EntityStunState
    {
        // Reference to the entity to which we are going to associate this script.
        private readonly EnemyA enemyA;
        
        // Class constructor.
        public EnemyAEntityStunState(EntityController entityController, EntityStateMachine entityStateMachine,
            string animationBoolName, DataEntityStunState dataEntityStunState, EnemyA enemyA)
            : base(entityController, entityStateMachine, animationBoolName, dataEntityStunState)
        {
            this.enemyA = enemyA;
        }
        
        // While in this state, depending on the conditions it will change to another state.
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            // 
            if (IsStunTimeOver)
            {
                // 
                if (PerformCloseRangeAction)
                {
                    EntityStateMachine.ChangeState(enemyA.MeleeAttackState);
                }
                // 
                else if (IsPlayerInMinAgroRange)
                {
                    EntityStateMachine.ChangeState(enemyA.ChargeState);
                }
                // 
                else
                {
                    enemyA.LookForPlayerState.SetTurnImmediately(true);
                    EntityStateMachine.ChangeState(enemyA.LookForPlayerState);
                }
            }
        }
    }
}
