
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The purpose of this script is:
//
//  Set the actions that EnemyA will take, when it's attacking at the player.
// 
//  Documentation and References:
//
//  C# Read only: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/readonly
//  C# Polymorphism: https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/polymorphism

using Enemies;
using Enemies.Data;
using SrCatknight.Scripts.Enemies.Data;
using SrCatknight.Scripts.Enemies.EntityStates.EntityAttackState;
using SrCatknight.Scripts.StateMachine;
using UnityEngine;

namespace _Development.Scripts.Mauricio.Enemies.EnemyA
{
    public class EnemyAMeleeAttackState : EntityMeleeAttackState
    {
        // Reference to the entity to which this script will be associated.
        private readonly EnemyA enemyA;
        
        // Class constructor.
        public EnemyAMeleeAttackState(EntityController entityController, EntityStateMachine entityStateMachine,
            string animationBoolName, Transform attackPosition, DataEntityMeleeAttack dataEntityMeleeAttack,
            EnemyA enemyA)
            : base(entityController, entityStateMachine, animationBoolName, attackPosition, dataEntityMeleeAttack)
        {
            this.enemyA = enemyA;
        }
        
        // While in this state, depending on the conditions it will change to another state.
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            // If the attack animation is finished.
            if (IsAnimationFinished)
            {
                // If the player is in the minimum aggro range.
                if (IsPlayerInMinimumAgroRange)
                {
                    EntityStateMachine.ChangeState(enemyA.PlayerDetectionState);
                }
                // If the player has not been detected.
                else
                {
                    EntityStateMachine.ChangeState(enemyA.LookForPlayerState);
                }
            }
        }
    }
}
