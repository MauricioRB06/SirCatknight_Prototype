
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The Purpose Of This Script Is:
//
//  Generate a behavior for the entity, that allows it to detect the player.
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
    public class EntityPlayerDetectionState : EntityState
    {
        // Reference to player detected state data.
        private readonly DataEntityPlayerDetection _dataEntityPlayerDetection;
        
        // To know if the player is in the minimum aggro range.
        protected bool IsPlayerInMinAgroRange;
        
        // To know if the player is in the maximum aggro range.
        protected bool IsPlayerInMaxAgroRange;
        
        // To know when to perform a long-range action.
        protected bool PerformLongRangeAction;
        
        // To know when to perform a close-range action.
        protected bool PerformCloseRangeAction;
        
        // To know if the entity detects a ledge when it is detecting the player.
        protected bool IsDetectingLedge;
        
        // Class constructor.
        protected EntityPlayerDetectionState(EntityController entityController, EntityStateMachine entityStateMachine,
            string animationBoolName, DataEntityPlayerDetection dataEntityPlayerDetection)
            : base(entityController, entityStateMachine, animationBoolName)
        {
            this._dataEntityPlayerDetection = dataEntityPlayerDetection;
        }
        
        // Checks of this state are performed.
        protected override void DoChecks()
        {
            base.DoChecks();
            
            IsPlayerInMinAgroRange = EntityController.CheckPlayerInMinAggroRange();
            IsPlayerInMaxAgroRange = EntityController.CheckPlayerInMaxAggroRange();
            IsDetectingLedge = Core.CollisionSenses.LedgeVertical;
            PerformCloseRangeAction = EntityController.CheckPlayerInCloseRangeAction();
        }
        
        // When this state is entered, the entity stops and sets as false to perform a long-range action.
        public override void Enter()
        {
            base.Enter();
            
            PerformLongRangeAction = false;
            Core.Movement.SetVelocityX(0f);     
        }
        
        // The entity stands still and waits for the established time to pass before performing a long-range action.
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            Core.Movement.SetVelocityX(0f);
            
            if (Time.time >= StartTime + _dataEntityPlayerDetection.LongRangeActionTime)
            {
                PerformLongRangeAction = true;
            }
        }
    }
}
