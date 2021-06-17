using Player.Data;
using Player.StateMachine;
using UnityEngine;

namespace Player.PlayerStates
{
    public class PlayerLedgeClimbState : PlayerState
    {
        private Vector2 _detectedPos;
        private Vector2 _cornerPos;
        private Vector2 _startPos;
        private Vector2 _stopPos;

        private bool _isHanging;
        private bool _isClimbing;
        private bool _jumpInput;
        private bool _isTouchingCeiling;

        private int _xInput;
        private int _yInput;
        
        private static readonly int ClimbLedge = Animator.StringToHash("climbLedge");
        private static readonly int IsTouchingCeiling = Animator.StringToHash("isTouchingCeiling");

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
            Player.transform.position = _detectedPos;
            _cornerPos = Player.DetermineCornerPosition();

            _startPos.Set(_cornerPos.x - (Player.FacingDirection * PlayerData.startOffset.x), _cornerPos.y - PlayerData.startOffset.y);
            _stopPos.Set(_cornerPos.x + (Player.FacingDirection * PlayerData.stopOffset.x), _cornerPos.y + PlayerData.stopOffset.y);

            Player.transform.position = _startPos;

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
                Player.transform.position = _startPos;

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

        public void SetDetectedPosition(Vector2 pos) => _detectedPos = pos;

        private void CheckForSpace()
        {
            _isTouchingCeiling = Physics2D.Raycast(_cornerPos + (Vector2.up * 0.015f) + (Vector2.right * (Player.FacingDirection * 0.015f)), Vector2.up, PlayerData.standColliderHeight, PlayerData.layerGroundAndWalls);
            Player.PlayerAnimator.SetBool(IsTouchingCeiling, _isTouchingCeiling);
        }
    }
}
