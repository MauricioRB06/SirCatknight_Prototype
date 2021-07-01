using Player.Data;
using StateMachine;

namespace Player.PlayerStates.PlayerGroundedState
{
    public class EntityGroundedState : EntityState
    {
        // We use it to determine the motion in the X axis
        protected int XInput;
        
        // We use it to determine if the entity is crouching or not
        protected int YInput;
        
        // We use them to check if the entity is sleeping
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
        private bool _dodgeRollInput;
        
        // Class constructor
        protected EntityGroundedState(Player entity, global::StateMachine.StateMachine stateMachine, PlayerData entityData,
            string animBoolName) : base(entity, stateMachine, entityData, animBoolName)
        {
        }

        protected override void DoChecks()
        {
            base.DoChecks();

            _isGrounded = Core.CollisionSenses.Ground;
            _isTouchingWall = Core.CollisionSenses.WallFront;
            _isTouchingLedge = Core.CollisionSenses.Ledge;
            IsTouchingCeiling = Core.CollisionSenses.Ceiling;
        }

        public override void Enter()
        {
            base.Enter();

            _isSleeping = Entity.CheckIfPlayerSleep();
            Entity.JumpState.ResetAmountOfJumpsLeft();
            Entity.DashState.ResetCanDash();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            XInput = Entity.InputHandler.NormInputX;
            YInput = Entity.InputHandler.NormInputY;
            _jumpInput = Entity.InputHandler.JumpInput;
            _grabInput = Entity.InputHandler.GrabInput;
            _dashInput = Entity.InputHandler.DashInput;
            _dodgeRollInput = Entity.InputHandler.DodgeRollInput;

            if (Entity.InputHandler.AttackInputs[(int) CombatInputs.Primary] && !IsTouchingCeiling)
            {
                StateMachine.ChangeState((Entity.PrimaryAttackState));
            }
            else if (Entity.InputHandler.AttackInputs[(int) CombatInputs.Secondary] && !IsTouchingCeiling)
            {
                StateMachine.ChangeState((Entity.SecondaryAttackState));
            }
            else if (_jumpInput && Entity.JumpState.CanJump() && !IsTouchingCeiling && !_isSleeping)
            {
                StateMachine.ChangeState(Entity.JumpState);
            }
            else if (!_isGrounded)
            {
                Entity.InAirState.StartCoyoteTime();
                StateMachine.ChangeState(Entity.InAirState);
            }
            else if(_isTouchingWall && _grabInput && _isTouchingLedge)
            {
                StateMachine.ChangeState(Entity.WallGrabState);
            }
            else if (_dashInput && Entity.DashState.CheckIfCanDash() && !IsTouchingCeiling && !_isSleeping)
            {
                StateMachine.ChangeState(Entity.DashState);
            }
            else if (_dodgeRollInput && !_isSleeping)
            {
                StateMachine.ChangeState(Entity.DodgeRoll);
            }
        }
    }
}
