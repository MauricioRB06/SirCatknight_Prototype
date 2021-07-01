using Player.Data;
using StateMachine;

namespace Player.PlayerStates.PlayerTouchingWallState
{
    public class EntityTouchingWallState : EntityState
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
        protected EntityTouchingWallState(Player entity, global::StateMachine.StateMachine stateMachine, PlayerData entityData,
            string animBoolName): base(entity, stateMachine, entityData, animBoolName)
        {
        }

        protected override void DoChecks()
        {
            base.DoChecks();

            _isGrounded = Core.CollisionSenses.Ground;
            _isTouchingWall = Core.CollisionSenses.WallFront;
            _isTouchingLedge = Core.CollisionSenses.Ledge;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            _xInput = Entity.InputHandler.NormInputX;
            YInput = Entity.InputHandler.NormInputY;
            GrabInput = Entity.InputHandler.GrabInput;
            _jumpInput = Entity.InputHandler.JumpInput;

            if (_jumpInput)
            {            
                Entity.WallJumpState.DetermineWallJumpDirection(_isTouchingWall);
                StateMachine.ChangeState(Entity.WallJumpState);
            }
            else if (_isGrounded && !GrabInput)
            {
                StateMachine.ChangeState(Entity.IdleState);
            }
            else if(!_isTouchingWall || (_xInput != Core.Movement.FacingDirection && !GrabInput))
            {
                StateMachine.ChangeState(Entity.InAirState);
            }
            else if(_isTouchingWall && !_isTouchingLedge)
            {
                StateMachine.ChangeState(Entity.LedgeClimbState);
            }
        }
    }
}
