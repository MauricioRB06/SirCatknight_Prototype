using Player.Data;
using Player.StateMachine;

namespace Player.PlayerStates.PlayerTouchingWallState
{
    public class PlayerTouchingWallState : PlayerState
    {
        protected bool IsGrounded;
        protected bool IsTouchingWall;
        protected bool GrabInput;
        protected bool JumpInput;
        protected bool IsTouchingLedge;
        protected int XInput;
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

            IsGrounded = Player.CheckIfGrounded();
            IsTouchingWall = Player.CheckIfTouchingWall();
            IsTouchingLedge = Player.CheckIfTouchingLedge();

            if(IsTouchingWall && !IsTouchingLedge)
            {
                Player.LedgeClimbState.SetDetectedPosition(Player.transform.position);
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            XInput = Player.InputHandler.NormInputX;
            YInput = Player.InputHandler.NormInputY;
            GrabInput = Player.InputHandler.GrabInput;
            JumpInput = Player.InputHandler.JumpInput;

            if (JumpInput)
            {            
                Player.WallJumpState.DetermineWallJumpDirection(IsTouchingWall);
                StateMachine.ChangeState(Player.WallJumpState);
            }
            else if (IsGrounded && !GrabInput)
            {
                StateMachine.ChangeState(Player.IdleState);
            }
            else if(!IsTouchingWall || (XInput != Player.FacingDirection && !GrabInput))
            {
                StateMachine.ChangeState(Player.InAirState);
            }
            else if(IsTouchingWall && !IsTouchingLedge)
            {
                StateMachine.ChangeState(Player.LedgeClimbState);
            }
        }
    }
}
