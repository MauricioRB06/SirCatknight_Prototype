using Player.Data;
using Player.StateMachine;
using UnityEngine;

namespace Player.PlayerStates
{
    public class PlayerLedgeClimbState : PlayerState
    {
        // We use it to store the position of the corner, when it is detected
        private Vector2 _cornerPos;
        
        // We use them to locate the starting and ending point from where the corner climbs to where it ends
        private Vector2 _startPosition;
        private Vector2 _stopPos;

        // We use it to know if the player is hanging
        private bool _isHanging;
        
        // We use it to check if the corner is climbing
        private bool _isClimbing;
        
        // We use it so that the player can jump, while hanging
        private bool _jumpInput;
        
        // We use it to know if when climbing a roof and we must go crouch
        private bool _isTouchingCeiling;
        
        // We use it to detect the input and know whether to climb the corner or drop
        private int _xInput;
        private int _yInput;
        
        // Generate id parameters for the animator
        private static readonly int ClimbLedge = Animator.StringToHash("LedgeClimbUp");
        private static readonly int IsTouchingCeiling = Animator.StringToHash("TouchingCeiling");

        // Class Constructor
        public PlayerLedgeClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData,
            string animBoolName): base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void AnimationFinishTrigger()
        {
            base.AnimationFinishTrigger();
            
            Player.PlayerAnimator.SetBool(ClimbLedge, false);
        }

        public override void AnimationTrigger()
        {
            base.AnimationTrigger();

            _isHanging = true;
        }

        public override void Enter()
        {
            base.Enter();

            Player.SetVelocityZero();
            _cornerPos = Player.DetermineCornerPosition();

            _startPosition.Set(_cornerPos.x - (Player.FacingDirection * PlayerData.startOffset.x),
                _cornerPos.y - PlayerData.startOffset.y);
            
            _stopPos.Set(_cornerPos.x + (Player.FacingDirection * PlayerData.stopOffset.x),
                _cornerPos.y + PlayerData.stopOffset.y);

            Player.transform.position = _startPosition;
        }

        public override void Exit()
        {
            base.Exit();

            _isHanging = false;

            if (!_isClimbing) return;
            
            Player.transform.position = _stopPos;
            _isClimbing = false;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsAnimationFinished)
            {
                if (_isTouchingCeiling)
                {
                    StateMachine.ChangeState(Player.CrouchIdleState);
                }
                else
                {
                    StateMachine.ChangeState(Player.IdleState);
                }
            }
            else
            {
                _xInput = Player.InputHandler.NormInputX;
                _yInput = Player.InputHandler.NormInputY;
                _jumpInput = Player.InputHandler.JumpInput;

                Player.SetVelocityZero();
                Player.transform.position = _startPosition;

                if (_xInput == Player.FacingDirection && _isHanging && !_isClimbing)
                {
                    CheckForSpace();
                    _isClimbing = true;
                    Player.PlayerAnimator.SetBool(ClimbLedge, true);
                }
                else if (_yInput == -1 && _isHanging && !_isClimbing)
                {
                    StateMachine.ChangeState(Player.InAirState);
                }
                else if(_jumpInput && !_isClimbing)
                {
                    Player.WallJumpState.DetermineWallJumpDirection(true);
                    StateMachine.ChangeState(Player.WallJumpState);
                }
            }
        }
        
        // We use it to throw lightning from the corner and check if we have full room to stand up
        private void CheckForSpace()
        {
            _isTouchingCeiling = Physics2D.Raycast(_cornerPos + Vector2.up * 0.015f + Vector2.right * (Player.FacingDirection * 0.015f),
                Vector2.up, PlayerData.normalColliderHeight, PlayerData.layerGroundWalls);
            
            Player.PlayerAnimator.SetBool(IsTouchingCeiling, _isTouchingCeiling);
        }
    }
}
