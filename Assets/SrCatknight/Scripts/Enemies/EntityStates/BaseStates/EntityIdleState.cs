
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The purpose of this script is:
//
//  Generate a behavior for the entity, that allows it to be idle.
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
    public class EntityIdleState : EntityState
    {
        // Reference to the idle state data.
        private readonly DataEntityIdleState _dataEntityIdleState;
        
        // To know when the idle time is over and you should exit the state.
        protected bool IsIdleTimeOver;
        
        // To establish how long it should be in idle.
        private float _idleTime;
        
        // To know if the entity should rotate, after exiting the Idle state.
        private bool _flipAfterIdleState;
        
        // To verify if the player is within the minimum aggro range.
        protected bool IsPlayerInMinAggroRange;
        
        // Class constructor
        protected EntityIdleState(EntityController entityController, EntityStateMachine entityStateMachine,
            string animationBoolName, DataEntityIdleState dataEntityIdleState)
            : base(entityController, entityStateMachine, animationBoolName)
        {
            this._dataEntityIdleState = dataEntityIdleState;
        }
        
        // Checks of this status are performed.
        protected override void DoChecks()
        {
            base.DoChecks();
            IsPlayerInMinAggroRange = EntityController.CheckPlayerInMinAggroRange();
        }
        
        // When the state is entered, the velocity of the entity is stopped and an idle state time is generated.
        public override void Enter()
        {
            base.Enter();

            Core.Movement.SetVelocityX(0f);
            IsIdleTimeOver = false;        
            SetRandomIdleTime();
        }
        
        // When leaving the state, a check is made to see if the entity should be rotated.
        public override void Exit()
        {
            base.Exit();

            if (_flipAfterIdleState)
            {
                Core.Movement.Flip();
            }
        }
        
        // As long as it remains in this state, the velocity will remain at 0.
        public override void LogicUpdate()
        {
            base.LogicUpdate();

            Core.Movement.SetVelocityX(0f);

            if (Time.time >= StartTime + _idleTime)
            {
                IsIdleTimeOver = true;
            }
        }
        
        // Establishes when the entity is to be turned.
        public void SetFlipAfterIdleState(bool flip)
        {
            _flipAfterIdleState = flip;
        }
        
        // Selects a random idle time, in the range set in the idle state data.
        private void SetRandomIdleTime()
        {
            _idleTime = Random.Range(_dataEntityIdleState.MinIdleTime, _dataEntityIdleState.MaxIdleTime);
        }
    }
}
