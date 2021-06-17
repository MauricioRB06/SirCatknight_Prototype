using Player.Data;
using Player.StateMachine;

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
        protected PlayerTouchingWallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData,
            string animBoolName): base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void AnimationFinishTrigger()
        {
            base.AnimationFinishTrigger();
        }

        public override void AnimationTrigger()
        {
            base.AnimationTrigger();
        }

        protected override void DoChecks()
        {
            base.DoChecks();

            _isGrounded = Player.CheckIfGrounded();
            _isTouchingWall = Player.CheckIfTouchingWall();
            _isTouchingLedge = Player.CheckIfTouchingLedge();

            if(_isTouchingWall && !_isTouchingLedge)
            {
                Player.LedgeClimbState.SetDetectedPosition(Player.transform.position);
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            _xInput = Player.InputHandler.NormInputX;
            YInput = Player.InputHandler.NormInputY;
            GrabInput = Player.InputHandler.GrabInput;
            _jumpInput = Player.InputHandler.JumpInput;

            if (_jumpInput)
            {            
                Player.WallJumpState.DetermineWallJumpDirection(_isTouchingWall);
                StateMachine.ChangeState(Player.WallJumpState);
            }
            else if (_isGrounded && !GrabInput)
            {
                StateMachine.ChangeState(Player.IdleState);
            }
            else if(!_isTouchingWall || (_xInput != Player.FacingDirection && !GrabInput))
            {
                StateMachine.ChangeState(Player.InAirState);
            }
            else if(_isTouchingWall && !_isTouchingLedge)
            {
                StateMachine.ChangeState(Player.LedgeClimbState);
            }
        }
    }
}
