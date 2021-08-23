using Player.Data;
using StateMachine;

namespace Player.PlayerStates.BaseStates
{
    public class PlayerTouchingWallState : PlayerState
    {
        // We use them to verify possible status changes
        private bool _isGrounded;
        private bool _isTouchingWall;
        private bool _isTouchingLedge;
        
        // We use them to verify controls for skills
        protected bool GrabInput;
        private bool _jumpInput;
        
        // We use it to know whether to switch between WallSlide or AirState
        private int _xInput;
        
        // We use it to switch between GrabState and ClimbState
        protected int YInput;
        
        // Class constructor
        protected PlayerTouchingWallState(PlayerController playerController, PlayerStateMachine playerStateMachine, DataPlayerController dataPlayerController,
            string animBoolName): base(playerController, playerStateMachine, dataPlayerController, animBoolName)
        {
        }

        protected override void DoChecks()
        {
            base.DoChecks();

            _isGrounded = Core.CollisionSenses.Ground;
            _isTouchingWall = Core.CollisionSenses.WallFront;
            _isTouchingLedge = Core.CollisionSenses.LedgeHorizontal;
        }

        public override void Enter()
        {
            base.Enter();
            Core.Movement.RestoreGravityScale(DataPlayerController.restoreGravityScale);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            _xInput = PlayerController.InputHandler.NormInputX;
            YInput = PlayerController.InputHandler.NormInputY;
            GrabInput = PlayerController.InputHandler.GrabInput;
            _jumpInput = PlayerController.InputHandler.JumpInput;

            if (_jumpInput)
            {            
                PlayerController.WallJumpState.DetermineWallJumpDirection(_isTouchingWall);
                PlayerStateMachine.ChangeState(PlayerController.WallJumpState);
            }
            else if (_isGrounded && !GrabInput)
            {
                PlayerStateMachine.ChangeState(PlayerController.IdleState);
            }
            else if(!_isTouchingWall || (_xInput != Core.Movement.FacingDirection && !GrabInput))
            {
                PlayerStateMachine.ChangeState(PlayerController.InAirState);
            }
            else if(_isTouchingWall && !_isTouchingLedge)
            {
                PlayerStateMachine.ChangeState(PlayerController.LedgeClimbState);
            }
        }
    }
}
