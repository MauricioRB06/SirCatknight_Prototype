
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The purpose of this script is:
//
//  Set the actions that EnemyA will take, when it's looking at the player.
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
    public class EnemyALookForPlayerState : EntityLookForPlayerState
    {
        // Reference to the entity to which we are going to associate this script.
        private readonly EnemyA enemyA;
        
        // Class constructor.
        public EnemyALookForPlayerState(EntityController entityController, EntityStateMachine entityStateMachine,
            string animationBoolName, DataEntityLookForPlayer dataEntityLookForPlayer, EnemyA enemyA)
            : base(entityController, entityStateMachine, animationBoolName, dataEntityLookForPlayer)
        {
            this.enemyA = enemyA;
        }
        
        // While in this state, depending on the conditions it will change to another state.
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            // If the player has been detected within the minimum aggro range.
            if (IsPlayerInMinAgroRange)
            {
                EntityStateMachine.ChangeState(enemyA.PlayerDetectionState);
            }
            // If the player has not been detected and all turns have been completed.
            else if (IsAllTurnsTimeDone)
            {
                EntityStateMachine.ChangeState(enemyA.MoveState);
            }
        }
    }
}
