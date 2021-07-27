﻿using Player.Data;

// The purpose of this script is:
/* Insert Here */

/* Documentation and References:
 * 
 * 
 * 
 */

namespace Player.PlayerStates.PlayerGroundedState
{
    public class PlayerGroundedState : PlayerState
    {
        // We use it to determine the motion in the X axis
        protected int XInput;
        
        // We use it to determine if the entity is crouching or not
        protected int YInput;
        
        // We use them to check if the entity is sleeping
        private bool _isSleeping;
        
        // We use them to verify possible status changes
        private bool _isGrounded;
        private bool _isTouchingWall;
        private bool _isTouchingLedge;
        protected bool IsTouchingCeiling;
        
        // We use them to verify controls for skills
        private bool _jumpInput;
        private bool _grabInput;
        private bool _dashInput;
        private bool _dodgeRollInput;
        
        // Class constructor
        protected PlayerGroundedState(PlayerController playerController, StateMachine.StateMachine stateMachine, PlayerData playerData,
            string animBoolName) : base(playerController, stateMachine, playerData, animBoolName)
        {
        }

        protected override void DoChecks()
        {
            base.DoChecks();

            _isGrounded = Core.CollisionSenses.Ground;
            _isTouchingWall = Core.CollisionSenses.WallFront;
            _isTouchingLedge = Core.CollisionSenses.Ledge;
            IsTouchingCeiling = Core.CollisionSenses.Ceiling;
        }

        public override void Enter()
        {
            base.Enter();

            _isSleeping = PlayerController.CheckIfPlayerSleep();
            PlayerController.JumpState.ResetAmountOfJumpsLeft();
            PlayerController.DashState.ResetCanDash();
            Core.Movement.RestoreGravityScale(PlayerData.restoreGravityScale);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            XInput = PlayerController.InputHandler.NormInputX;
            YInput = PlayerController.InputHandler.NormInputY;
            _jumpInput = PlayerController.InputHandler.JumpInput;
            _grabInput = PlayerController.InputHandler.GrabInput;
            _dashInput = PlayerController.InputHandler.DashInput;
            _dodgeRollInput = PlayerController.InputHandler.DodgeRollInput;

            if (PlayerController.InputHandler.AttackInputs[(int) CombatInputs.PrimaryAttackInput] && !IsTouchingCeiling
                && !_isSleeping)
            {
                StateMachine.ChangeState((PlayerController.PrimaryAttackState));
            }
            else if (PlayerController.InputHandler.AttackInputs[(int) CombatInputs.SecondaryAttackInput] && !IsTouchingCeiling
                && !_isSleeping)
            {
                StateMachine.ChangeState((PlayerController.SecondaryAttackState));
            }
            else if (_jumpInput && PlayerController.JumpState.CanJump() && !IsTouchingCeiling && !_isSleeping)
            {
                StateMachine.ChangeState(PlayerController.JumpState);
            }
            else if (!_isGrounded)
            {
                PlayerController.InAirState.StartCoyoteTime();
                StateMachine.ChangeState(PlayerController.InAirState);
            }
            else if(_isTouchingWall && _grabInput && _isTouchingLedge && !_isSleeping)
            {
                StateMachine.ChangeState(PlayerController.WallGrabState);
            }
            else if (_dashInput && PlayerController.DashState.CheckIfCanDash() && !IsTouchingCeiling && !_isSleeping)
            {
                StateMachine.ChangeState(PlayerController.DashState);
            }
            else if (_dodgeRollInput && !_isSleeping)
            {
                StateMachine.ChangeState(PlayerController.DodgeRoll);
            }
        }
    }
}
