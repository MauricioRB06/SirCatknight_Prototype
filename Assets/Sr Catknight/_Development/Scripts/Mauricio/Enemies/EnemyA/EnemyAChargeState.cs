
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The purpose of this script is:
//
//  Set the actions that EnemyA will take, when it's charging at the player.
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
    public class EnemyAChargeState : EntityChargeState
    {
        // Reference to the entity to which we are going to associate this script.
        private readonly EnemyA enemyA;
        
        // Class constructor
        public EnemyAChargeState(EntityController entityController, EntityStateMachine entityStateMachine,
            string animationBoolName, DataEntityChargeState dataEntityChargeState, EnemyA enemyA)
            : base(entityController, entityStateMachine, animationBoolName, dataEntityChargeState)
        {
            this.enemyA = enemyA;
        }
        
        // While in this state, depending on the conditions, it will change to different states.
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            // If during the charge it is detected that the player is too close, the entity attacks him.
            if (PerformCloseRangeAction)
            {
                EntityStateMachine.ChangeState(enemyA.MeleeAttackState);
            }
            // If a wall is detected during charging or a ledge is undetected, it will start looking for the player.
            else if (IsDetectingWall || !IsDetectingLedge)
            {
                EntityStateMachine.ChangeState(enemyA.LookForPlayerState);
            }
            // If, during charging, the charging time runs out.
            else if (IsChargeTimeOver)
            {
                // If the player is still in the minimum aggro range.
                if (IsPlayerInMinAgroRange)
                {
                    EntityStateMachine.ChangeState(enemyA.PlayerDetectionState);
                }
                // If the player is no longer there, they try to look for him.
                else
                {
                    EntityStateMachine.ChangeState(enemyA.LookForPlayerState);
                }
            }
        }
    }
}
