using Player.Data;
using StateMachine;

namespace Player.PlayerStates.BaseStates
{
    public class PlayerAbilityState : PlayerState
    {
        // We use it to determine the motion in the X axis.
        protected int XInput;
        
        // We use it to know if the skill has already been performed.
        protected bool IsAbilityDone;
        
        // To check if the character is on the ground.
        private bool _isGrounded;
        
        // Class constructor.
        protected PlayerAbilityState(PlayerController playerController, PlayerStateMachine playerStateMachine,
            DataPlayerController dataPlayerController, string animationBoolName)
            : base(playerController, playerStateMachine, dataPlayerController, animationBoolName)
        {
        }
        
        // 
        protected override void DoChecks()
        {
            base.DoChecks();
            
            _isGrounded = Core.CollisionSenses.Ground;
        }
        
        // 
        public override void Enter()
        {
            base.Enter();
            
            IsAbilityDone = false;
            Core.Movement.RestoreGravityScale(DataPlayerController.restoreGravityScale);
        }
        
        // 
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            XInput = PlayerController.InputHandler.NormInputX;

            if (!IsAbilityDone) return;
            
            if (_isGrounded && Core.Movement.CurrentVelocity.y < 0.01f)
            {
                PlayerStateMachine.ChangeState(PlayerController.IdleState);
            }
            else
            {
                PlayerStateMachine.ChangeState(PlayerController.InAirState);
            }
        }
    }
}
