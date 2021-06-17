using Player.Data;
using Player.StateMachine;
using UnityEngine;

/* Documentation:
* 
* Avoiding string parameters: https://blog.jetbrains.com/dotnet/2019/02/28/performance-inspections-unity-code-rider/
* Coyote time: https://developer.amazon.com/blogs/appstore/post/9d2094ed-53cb-4a3a-a5cf-c7f34bca6cd3/coding-imprecise-controls-to-make-them-feel-more-precise
* 
*/

namespace Player.PlayerStates
{
    public class PlayerInAirState : PlayerState
    {
        // We use it to move in the air
        private int _xInput;
        
        // To be able to use the jump in the air
        private bool _jumpInput;
        private bool _jumpInputStop;
        private bool _isJumping;
        
        private bool _grabInput;
        private bool _dashInput;

        //Checks
        private bool _isGrounded;
        private bool _isTouchingWall;
        private bool _isTouchingWallBack;
        private bool _isTouchingLedge;
        
        private bool _oldIsTouchingWall;
        private bool _oldIsTouchingWallBack;

        private bool _coyoteTime;
        private bool _wallJumpCoyoteTime;
        

        private float _startWallJumpCoyoteTime;
        private static readonly int YVelocity = Animator.StringToHash("yVelocity");
        private static readonly int XVelocity = Animator.StringToHash("xVelocity");

        // Class constructor
        public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData,
            string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }

        protected override void DoChecks()
        {
            base.DoChecks();

            _oldIsTouchingWall = _isTouchingWall;
            _oldIsTouchingWallBack = _isTouchingWallBack;

            _isGrounded = Player.CheckIfGrounded();
            _isTouchingWall = Player.CheckIfTouchingWall();
            _isTouchingWallBack = Player.CheckIfTouchingWallBack();
            _isTouchingLedge = Player.CheckIfTouchingLedge();

            if(_isTouchingWall && !_isTouchingLedge)
            {
                Player.LedgeClimbState.SetDetectedPosition(Player.transform.position);
            }

            if(!_wallJumpCoyoteTime && !_isTouchingWall && !_isTouchingWallBack && (_oldIsTouchingWall || _oldIsTouchingWallBack))
            {
                StartWallJumpCoyoteTime();
            }
        }

        public override void Exit()
        {
            base.Exit();

            _oldIsTouchingWall = false;
            _oldIsTouchingWallBack = false;
            _isTouchingWall = false;
            _isTouchingWallBack = false;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            CheckCoyoteTime();
            CheckWallJumpCoyoteTime();

            _xInput = Player.InputHandler.NormInputX;
            _jumpInput = Player.InputHandler.JumpInput;
            _jumpInputStop = Player.InputHandler.JumpInputStop;
            _grabInput = Player.InputHandler.GrabInput;
            _dashInput = Player.InputHandler.DashInput;

            CheckJumpLimiter();

            if (_isGrounded && Player.CurrentVelocity.y < 0.01f)
            {            
                StateMachine.ChangeState(Player.LandState);
            }
            else if(_isTouchingWall && !_isTouchingLedge && !_isGrounded)
            {
                StateMachine.ChangeState(Player.LedgeClimbState);
            }
            else if(_jumpInput && (_isTouchingWall || _isTouchingWallBack || _wallJumpCoyoteTime))
            {
                StopWallJumpCoyoteTime();
                _isTouchingWall = Player.CheckIfTouchingWall();
                Player.WallJumpState.DetermineWallJumpDirection(_isTouchingWall);
                StateMachine.ChangeState(Player.WallJumpState);
            }
            else if(_jumpInput && Player.JumpState.CanJump())
            {
                StateMachine.ChangeState(Player.JumpState);
            }
            else if(_isTouchingWall && _grabInput && _isTouchingLedge)
            {
                StateMachine.ChangeState(Player.WallGrabState);
            }
            else if(_isTouchingWall && _xInput == Player.FacingDirection && Player.CurrentVelocity.y <= 0)
            {
                StateMachine.ChangeState(Player.WallSlideState);
            }
            else if(_dashInput && Player.DashState.CheckIfCanDash())
            {
                StateMachine.ChangeState(Player.DashState);
            }
            else
            {
                Player.CheckIfShouldFlip(_xInput);
                Player.SetVelocityX(PlayerData.movementVelocity * _xInput);
                
                // We pass the motion parameters to the animator, so that it plays the correct animation
                Player.PlayerAnimator.SetFloat(YVelocity, Player.CurrentVelocity.y);
                Player.PlayerAnimator.SetFloat(XVelocity, Mathf.Abs(Player.CurrentVelocity.x));
            }

        }

        // When we stop pressing the jump button, we slow down to start falling
        private void CheckJumpLimiter()
        {
            if (!_isJumping) return;
            
            if (_jumpInputStop)
            {
                Player.SetVelocityY(Player.CurrentVelocity.y * PlayerData.jumpHeightLimiter);
                _isJumping = false;
            }
            else if (Player.CurrentVelocity.y <= 0f)
            {
                _isJumping = false;
            }
        }

        private void CheckCoyoteTime()
        {
            if (!_coyoteTime || !(Time.time > StartTime + PlayerData.coyoteTime)) return;
            
            _coyoteTime = false;
            Player.JumpState.DecreaseAmountOfJumpsLeft();
        }

        private void CheckWallJumpCoyoteTime()
        {
            if(_wallJumpCoyoteTime && Time.time > _startWallJumpCoyoteTime + PlayerData.coyoteTime)
            {
                _wallJumpCoyoteTime = false;
            }
        }

        public void StartCoyoteTime() => _coyoteTime = true;

        private void StartWallJumpCoyoteTime()
        {
            _wallJumpCoyoteTime = true;
            _startWallJumpCoyoteTime = Time.time;
        }

        private void StopWallJumpCoyoteTime() => _wallJumpCoyoteTime = false;

        public void SetIsJumping() => _isJumping = true;
    }
}
