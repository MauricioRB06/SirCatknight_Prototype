using Player.Data;
using StateMachine;
using UnityEngine;

namespace Player.PlayerStates
{
    public class EntityLedgeClimbState : EntityState
    {
        // We use it to store the position of the corner, when it is detected
        private Vector2 _cornerPos;
        
        // We use them to locate the starting and ending point from where the corner climbs to where it ends
        private Vector2 _startPosition;
        private Vector2 _stopPos;

        // We use it to know if the entity is hanging
        private bool _isHanging;
        
        // We use it to check if the corner is climbing
        private bool _isClimbing;
        
        // We use it so that the entity can jump, while hanging
        private bool _jumpInput;
        
        // We use it to know if when climbing a roof and we must go crouch
        private bool _isTouchingCeiling;
        
        // We use it to detect the input and know whether to climb the corner or drop
        private int _xInput;
        private int _yInput;
        
        // We use it to return to store the position of the corners that are detected
        private Vector2 _detectedCornerPosition;
        
        // Generate id parameters for the animator
        private static readonly int ClimbLedge = Animator.StringToHash("LedgeClimbUp");
        private static readonly int IsTouchingCeiling = Animator.StringToHash("TouchingCeiling");

        // Class Constructor
        public EntityLedgeClimbState(Player entity, global::StateMachine.StateMachine stateMachine, PlayerData entityData,
            string animBoolName): base(entity, stateMachine, entityData, animBoolName)
        {
        }

        public override void AnimationFinishTrigger()
        {
            base.AnimationFinishTrigger();
            
            Entity.PlayerAnimator.SetBool(ClimbLedge, false);
        }

        public override void AnimationTrigger()
        {
            base.AnimationTrigger();

            _isHanging = true;
        }

        public override void Enter()
        {
            base.Enter();

            Core.Movement.SetVelocityZero();
            _cornerPos = DetermineCornerPosition();

            _startPosition.Set(_cornerPos.x - (Core.Movement.FacingDirection * EntityData.startOffset.x),
                _cornerPos.y - EntityData.startOffset.y);
            
            _stopPos.Set(_cornerPos.x + (Core.Movement.FacingDirection * EntityData.stopOffset.x),
                _cornerPos.y + EntityData.stopOffset.y);

            Entity.transform.position = _startPosition;
        }

        public override void Exit()
        {
            base.Exit();

            _isHanging = false;

            if (!_isClimbing) return;
            
            Entity.transform.position = _stopPos;
            _isClimbing = false;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsAnimationFinished)
            {
                if (_isTouchingCeiling)
                {
                    StateMachine.ChangeState(Entity.CrouchIdleState);
                }
                else
                {
                    StateMachine.ChangeState(Entity.IdleState);
                }
            }
            else
            {
                _xInput = Entity.InputHandler.NormInputX;
                _yInput = Entity.InputHandler.NormInputY;
                _jumpInput = Entity.InputHandler.JumpInput;

                Core.Movement.SetVelocityZero();
                Entity.transform.position = _startPosition;

                if (_xInput == Core.Movement.FacingDirection && _isHanging && !_isClimbing)
                {
                    CheckForSpace();
                    _isClimbing = true;
                    Entity.PlayerAnimator.SetBool(ClimbLedge, true);
                }
                else if (_yInput == -1 && _isHanging && !_isClimbing)
                {
                    StateMachine.ChangeState(Entity.InAirState);
                }
                else if(_jumpInput && !_isClimbing)
                {
                    Entity.WallJumpState.DetermineWallJumpDirection(true);
                    StateMachine.ChangeState(Entity.WallJumpState);
                }
            }
        }
        
        // We use it to throw lightning from the corner and check if we have full room to stand up
        private void CheckForSpace()
        {
            _isTouchingCeiling = Physics2D.Raycast(_cornerPos + Vector2.up * 0.015f + Vector2.right * (Core.Movement.FacingDirection * 0.015f),
                Vector2.up, EntityData.normalColliderHeight, Core.CollisionSenses.LayerGroundWalls);
            
            Entity.PlayerAnimator.SetBool(IsTouchingCeiling, _isTouchingCeiling);
        }
        
        // We use it to determine the position of the detected corner
        private Vector2 DetermineCornerPosition()
        {
            var wallCheckPosition = Core.CollisionSenses.WallCheck.position;
            var ledgeCheckPosition = Core.CollisionSenses.LedgeCheck.position;
            
            // First we detect the distance from the wall
            var xWallHit = Physics2D.Raycast(wallCheckPosition, Vector2.right * Core.Movement.FacingDirection,
                Core.CollisionSenses.WallCheckDistance, Core.CollisionSenses.LayerGroundWalls);
            var xWallDistance = xWallHit.distance;
            
            // We save that distance from the wall to use to check the ground clearance
            _detectedCornerPosition.Set((xWallDistance + 0.015f) * Core.Movement.FacingDirection, 0f);
            
            // Determine the distance from the ground
            var yFloorHit = Physics2D.Raycast(ledgeCheckPosition + (Vector3)(_detectedCornerPosition),
                Vector2.down, ledgeCheckPosition.y - wallCheckPosition.y + 0.015f,Core.CollisionSenses.LayerGroundWalls);
            var yFloorDistance = yFloorHit.distance;
            
            // Finally we get the exact position of the corner
            _detectedCornerPosition.Set(wallCheckPosition.x + (xWallDistance * Core.Movement.FacingDirection),
                ledgeCheckPosition.y - yFloorDistance);
            
            return _detectedCornerPosition;
        }
    }
}
