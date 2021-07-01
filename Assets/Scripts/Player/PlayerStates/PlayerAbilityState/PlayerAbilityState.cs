﻿using Player.Data;
using StateMachine;

namespace Player.PlayerStates.PlayerAbilityState
{
    public class EntityAbilityState : EntityState
    {
        // We use it to determine the motion in the X axis
        protected int XInput;
        
        // We use it to know if the skill has already been performed
        protected bool IsAbilityDone;
        
        // To check if the character is on the ground 
        private bool _isGrounded;
        
        // Class constructor
        protected EntityAbilityState(Player entity, global::StateMachine.StateMachine stateMachine, PlayerData entityData,
            string animBoolName) : base(entity, stateMachine, entityData, animBoolName)
        {
        }

        protected override void DoChecks()
        {
            base.DoChecks();
            
            _isGrounded = Core.CollisionSenses.Ground;
        }

        public override void Enter()
        {
            base.Enter();
            
            IsAbilityDone = false;
        }
        
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            XInput = Entity.InputHandler.NormInputX;

            if (!IsAbilityDone) return;
            
            if (_isGrounded && Core.Movement.CurrentVelocity.y < 0.01f)
            {
                StateMachine.ChangeState(Entity.IdleState);
            }
            else
            {
                StateMachine.ChangeState(Entity.InAirState);
            }
        }
    }
}
