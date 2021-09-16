
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The purpose of this script is:
//
//  Generate a behavior for the entity, that allows it to look for the player.
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
    public class EntityLookForPlayerState : EntityState
    {
        // Reference to the look for player state data.
        private readonly DataEntityLookForPlayer _dataEntityLookForPlayer;
        
        // To verify if the player is within the minimum aggro range.
        protected bool IsPlayerInMinAgroRange;
        
        // To turn to the entity.
        protected bool TurnImmediately;
        
        // To check if all the turns have already been made.
        protected bool IsAllTurnsDone;
        
        // To track the amount of time that has passed.
        protected bool IsAllTurnsTimeDone;
        
        // To check when we made the last turn.
        protected float LastTurnTime;
        
        // To control the number of turns that have been completed.
        protected int AmountOfTurnsDone;
        
        // Class constructor.
        protected EntityLookForPlayerState(EntityController entityController, EntityStateMachine entityStateMachine,
            string animationBoolName, DataEntityLookForPlayer dataEntityLookForPlayer)
            : base(entityController, entityStateMachine, animationBoolName)
        {
            this._dataEntityLookForPlayer = dataEntityLookForPlayer;
        }
        
        // Checks of this state.
        protected override void DoChecks()
        {
            base.DoChecks();

            IsPlayerInMinAgroRange = EntityController.CheckPlayerInMinAggroRange();
        }
        
        // When we enter the state, the entity stops and the initial settings are established.
        public override void Enter()
        {
            base.Enter();
            
            Core.Movement.SetVelocityX(0f);
            
            IsAllTurnsDone = false;
            IsAllTurnsTimeDone = false;
            
            // The time of the last turn is set as the time of entry into this state.
            LastTurnTime = StartTime;
            AmountOfTurnsDone = 0;
        }
        
        // While in this state, it will perform the established turns to try to locate the player.
        public override void LogicUpdate()
        {
            base.LogicUpdate();

            Core.Movement.SetVelocityX(0f);
            
            // It is checked whether it should be turn.
            if (TurnImmediately)
            {
                Core.Movement.Flip();
                
                // The time of this turn, is set as the time of the last turn.
                LastTurnTime = Time.time;
                AmountOfTurnsDone++;
                
                // The variable that initiates the sequence of turns is deactivated.
                TurnImmediately = false;
            }
            // Allows you to keep turning until the set number of turns is completed.
            else if(Time.time >= LastTurnTime + _dataEntityLookForPlayer.TimeBetweenTurns && !IsAllTurnsDone)
            {
                Core.Movement.Flip();
                
                LastTurnTime = Time.time;
                AmountOfTurnsDone++;
            }
            
            // It is checked whether all the established turns have already been completed.
            if(AmountOfTurnsDone >= _dataEntityLookForPlayer.AmountOfTurns)
            {
                IsAllTurnsDone = true;
            }
            
            // It is checked if the time for the last turn is completed.
            if(Time.time >= LastTurnTime + _dataEntityLookForPlayer.TimeBetweenTurns && IsAllTurnsDone)
            {
                IsAllTurnsTimeDone = true;
            }
        }
        
        // Allows to control the turn from other states.
        public void SetTurnImmediately(bool flip)
        {
            TurnImmediately = flip;
        }
    }
}
