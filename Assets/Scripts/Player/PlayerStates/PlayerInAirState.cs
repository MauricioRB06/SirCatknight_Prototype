using Player.Data;
using Player.StateMachine;
using UnityEngine;

/* Documentation:
 * 
 * Avoiding string parameters: https://blog.jetbrains.com/dotnet/2019/02/28/performance-inspections-unity-code-rider/
 * Coyote time: https://developer.amazon.com/blogs/appstore/post/9d2094ed-53cb-4a3a-a5cf-c7f34bca6cd3/coding-imprecise-controls-to-make-them-feel-more-precise
 * Switch: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/switch
 * StringToHash: https://docs.unity3d.com/ScriptReference/Animator.StringToHash.html
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
        
        // We use them to verify controls for skills
        private bool _grabInput;
        private bool _dashInput;

        // We use them to verify possible status changes
        private bool _isGrounded;
        private bool _isTouchingWall;
        private bool _isTouchingWallBack;
        private bool _isTouchingLedge;
        
        // We use it to check if in the previous frame we were touching the wall
        private bool _oldIsTouchingWall;
        private bool _oldIsTouchingWallBack;
        
        // We use them to enable the CoyoteTime after leaving the ground or accidentally moving the player between wall jumps
        private bool _coyoteTime;
        private bool _wallJumpCoyoteTime;
        
        // We use it to verify the CoyoteTime in the wall jump
        private float _startWallJumpCoyoteTime;
        
        // Generate id parameters for the animator
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
            
            // We save the values that were in the last frame before updating them
            _oldIsTouchingWall = _isTouchingWall;
            _oldIsTouchingWallBack = _isTouchingWallBack;

            _isGrounded = Player.CheckIfGrounded();
            _isTouchingWall = Player.CheckIfTouchingWall();
            _isTouchingWallBack = Player.CheckIfTouchingWallBack();
            _isTouchingLedge = Player.CheckIfTouchingLedge();
  
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
            else switch (_jumpInput)
            {
                case true when _isTouchingWall || _isTouchingWallBack || _wallJumpCoyoteTime:
                    
                    StopWallJumpCoyoteTime();
                    _isTouchingWall = Player.CheckIfTouchingWall();
                    Player.WallJumpState.DetermineWallJumpDirection(_isTouchingWall);
                    StateMachine.ChangeState(Player.WallJumpState);
                    break;
                
                case true when Player.JumpState.CanJump():
                    StateMachine.ChangeState(Player.JumpState);
                    break;
                
                default:
                {
                    switch (_isTouchingWall)
                    {
                        case true when _grabInput && _isTouchingLedge:
                            
                            StateMachine.ChangeState(Player.WallGrabState);
                            break;
                        
                        case true when _xInput == Player.FacingDirection && Player.CurrentVelocity.y <= 0:
                            
                            StateMachine.ChangeState(Player.WallSlideState);
                            break;
                        
                        default:
                        {
                            if(_dashInput && Player.DashState.CheckIfCanDash())
                            {
                                StateMachine.ChangeState(Player.DashState);
                            }
                            else
                            {
                                Player.CheckIfShouldFlip(_xInput);
                                Player.SetVelocityX(PlayerData.runVelocity * _xInput);
                
                                // We pass the motion parameters to the animator, so that it plays the correct animation
                                Player.PlayerAnimator.SetFloat(YVelocity, Player.CurrentVelocity.y);
                                Player.PlayerAnimator.SetFloat(XVelocity, Mathf.Abs(Player.CurrentVelocity.x));
                            }

                            break;
                        }
                    }

                    break;
                }
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
        
        // We use it to check if we have CoyoteTime remaining
        private void CheckCoyoteTime()
        {
            if (!_coyoteTime || !(Time.time > StartTime + PlayerData.coyoteTime)) return;
            
            _coyoteTime = false;
            Player.JumpState.DecreaseAmountOfJumpsLeft();
        }
        
        // We use it to check if we have WallCoyoteTime remaining
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
