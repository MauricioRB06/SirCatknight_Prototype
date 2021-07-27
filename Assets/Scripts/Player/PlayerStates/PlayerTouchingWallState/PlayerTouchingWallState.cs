using Player.Data;

namespace Player.PlayerStates.PlayerTouchingWallState
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
        protected PlayerTouchingWallState(PlayerController playerController, StateMachine.StateMachine stateMachine, PlayerData playerData,
            string animBoolName): base(playerController, stateMachine, playerData, animBoolName)
        {
        }

        protected override void DoChecks()
        {
            base.DoChecks();

            _isGrounded = Core.CollisionSenses.Ground;
            _isTouchingWall = Core.CollisionSenses.WallFront;
            _isTouchingLedge = Core.CollisionSenses.Ledge;
        }

        public override void Enter()
        {
            base.Enter();
            Core.Movement.RestoreGravityScale(PlayerData.restoreGravityScale);
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
                StateMachine.ChangeState(PlayerController.WallJumpState);
            }
            else if (_isGrounded && !GrabInput)
            {
                StateMachine.ChangeState(PlayerController.IdleState);
            }
            else if(!_isTouchingWall || (_xInput != Core.Movement.FacingDirection && !GrabInput))
            {
                StateMachine.ChangeState(PlayerController.InAirState);
            }
            else if(_isTouchingWall && !_isTouchingLedge)
            {
                StateMachine.ChangeState(PlayerController.LedgeClimbState);
            }
        }
    }
}
