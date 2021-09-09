using Player.Data;
using StateMachine;
using UnityEngine;

// The purpose of this script is:
/* Insert Here */

/* Documentation and References:
 * 
 * 
 * 
 */

namespace Player.PlayerStates.BaseStates
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
        protected bool controllerCanCrouch;
        protected bool IsTouchingCeiling;
        
        // We use them to verify controls for skills
        private bool _jumpInput;
        private bool _grabInput;
        private bool _dashInput;
        private bool _dodgeRollInput;
        private bool _interactInput;

        // Class constructor
        protected PlayerGroundedState(PlayerController playerController, PlayerStateMachine playerStateMachine, 
            DataPlayerController dataPlayerController, string animationBoolName)
            : base(playerController, playerStateMachine, dataPlayerController, animationBoolName)
        {
        }

        protected override void DoChecks()
        {
            base.DoChecks();

            _isGrounded = Core.CollisionSenses.Ground;
            _isTouchingWall = Core.CollisionSenses.WallFront;
            _isTouchingLedge = Core.CollisionSenses.LedgeHorizontal;
            controllerCanCrouch = PlayerController.InputHandler.ControllerCanCrouch;
            IsTouchingCeiling = Core.CollisionSenses.Ceiling;
        }

        public override void Enter()
        {
            base.Enter();

            _isSleeping = PlayerController.CheckIfPlayerSleep();
            PlayerController.JumpState.ResetAmountOfJumpsLeft();
            PlayerController.DashState.ResetCanDash();
            PlayerController.DodgeRollState.ResetCanDodgeRoll();
            Core.Movement.RestoreGravityScale(DataPlayerController.restoreGravityScale);
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
            _interactInput = PlayerController.InputHandler.InteractInput;

            if (PlayerController.InputHandler.AttackInputs[(int) CombatInputs.PrimaryAttackInput]
                && !IsTouchingCeiling && !_isSleeping)
            {
                PlayerStateMachine.ChangeState((PlayerController.PrimaryAttackState));
            }
            else if (_jumpInput && PlayerController.JumpState.CanJump() && !IsTouchingCeiling && !_isSleeping)
            {
                PlayerStateMachine.ChangeState(PlayerController.JumpState);
            }
            else if (!_isGrounded)
            {
                PlayerController.InAirState.StartCoyoteTime();
                PlayerStateMachine.ChangeState(PlayerController.InAirState);
            }
            else if(_isTouchingWall && _grabInput && _isTouchingLedge && !_isSleeping)
            {
                PlayerStateMachine.ChangeState(PlayerController.WallGrabState);
            }
            else if (_dashInput && PlayerController.DashState.CheckIfCanDash() && !IsTouchingCeiling && !_isSleeping)
            {
                PlayerStateMachine.ChangeState(PlayerController.DashState);
            }
            else if (_dodgeRollInput && PlayerController.DodgeRollState.CheckIfCanDodgeRoll()
                                     && !IsTouchingCeiling && !_isSleeping && XInput != 0)
            {
                PlayerStateMachine.ChangeState(PlayerController.DodgeRollState);
            }
            else if (_interactInput && !_isSleeping)
            {
                PlayerStateMachine.ChangeState(PlayerController.InteractState);
            }
        }
    }
}
