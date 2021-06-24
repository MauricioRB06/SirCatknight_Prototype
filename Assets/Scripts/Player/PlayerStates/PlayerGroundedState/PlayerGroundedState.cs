using Player.Data;
using Player.StateMachine;

namespace Player.PlayerStates.PlayerGroundedState
{
    public class PlayerGroundedState : PlayerState
    {
        // We use it to determine the motion in the X axis
        protected int XInput;
        
        // We use it to determine if the player is crouching or not
        protected int YInput;
        
        // We use them to check if the player is sleeping
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

            _isSleeping = Player.CheckIfPlayerSleep();
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

            if (_jumpInput && Player.JumpState.CanJump() && !IsTouchingCeiling && !_isSleeping)
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
            else if (_dashInput && Player.DashState.CheckIfCanDash() && !IsTouchingCeiling && !_isSleeping)
            {
                StateMachine.ChangeState(Player.DashState);
            }
        }
    }
}
