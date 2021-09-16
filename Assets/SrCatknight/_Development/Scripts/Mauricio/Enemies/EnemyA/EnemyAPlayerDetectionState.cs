
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The purpose of this script is:
//
//  Set the actions that EnemyA will take, when it's detecting at the player.
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
    public class EnemyAPlayerDetectionState : EntityPlayerDetectionState
    {
        // Reference to the entity to which we are going to associate this script.
        private readonly EnemyA enemyA;
        
        // Class constructor.
        public EnemyAPlayerDetectionState(EntityController entityController, EntityStateMachine entityStateMachine,
            string animationBoolName, DataEntityPlayerDetection dataEntityPlayerDetection, EnemyA enemyA)
            : base(entityController, entityStateMachine, animationBoolName, dataEntityPlayerDetection)
        {
            this.enemyA = enemyA;
        }
        
        // While in this state, depending on the conditions it will change to another state.
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            // If the player is detected in the minimum aggro range.
            if (PerformCloseRangeAction)
            {
                EntityStateMachine.ChangeState(enemyA.MeleeAttackState);
            }
            // If the player is detected in the maximum aggro range.
            else if (PerformLongRangeAction)
            {            
                EntityStateMachine.ChangeState(enemyA.ChargeState);
            }
            // If the player is not detected in the maximum aggro range, the entity will initiate a look.
            else if (!IsPlayerInMaxAgroRange)
            {
                EntityStateMachine.ChangeState(enemyA.LookForPlayerState);
            }
            // If a ledge is no longer detected.
            else if (!IsDetectingLedge)
            {
                Core.Movement.Flip();
                EntityStateMachine.ChangeState(enemyA.MoveState);
            }
        }
    }
}
