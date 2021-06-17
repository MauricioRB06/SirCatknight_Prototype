using Player.Data;
using Player.StateMachine;

namespace Player.PlayerStates.PlayerGroundedState
{
    public class PlayerGroundedState : PlayerState
    {
        protected int XInput;
        protected int YInput;

        protected bool IsTouchingCeiling;

        private bool _jumpInput;
        private bool _grabInput;
        private bool _isGrounded;
        private bool _isTouchingWall;
        private bool _isTouchingLedge;
        private bool _dashInput;
        
        // Class constructor
        protected PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData,
            string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }

        protected override void DoChecks()
        {
            base.DoChecks();

            _isGrounded = Player.CheckIfGrounded();
            _isTouchingWall = Player.CheckIfTouchingWall();
            _isTouchingLedge = Player.CheckIfTouchingLedge();
            IsTouchingCeiling = Player.CheckForCeiling();
        }

        public override void Enter()
        {
            base.Enter();

            Player.JumpState.ResetAmountOfJumpsLeft();
            Player.DashState.ResetCanDash();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            XInput = Player.InputHandler.NormInputX;
            YInput = Player.InputHandler.NormInputY;
            _jumpInput = Player.InputHandler.JumpInput;
            _grabInput = Player.InputHandler.GrabInput;
            _dashInput = Player.InputHandler.DashInput;

            if (_jumpInput && Player.JumpState.CanJump())
            {
                StateMachine.ChangeState(Player.JumpState);
            }
            else if (!_isGrounded)
            {
                Player.InAirState.StartCoyoteTime();
                StateMachine.ChangeState(Player.InAirState);
            }
            else if(_isTouchingWall && _grabInput && _isTouchingLedge)
            {
                StateMachine.ChangeState(Player.WallGrabState);
            }
            else if (_dashInput && Player.DashState.CheckIfCanDash() && !IsTouchingCeiling)
            {
                StateMachine.ChangeState(Player.DashState);
            }
        }
    }
}
