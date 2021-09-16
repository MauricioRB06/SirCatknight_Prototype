using SrCatknight.Scripts.Player.Data;
using SrCatknight.Scripts.StateMachine;

namespace SrCatknight.Scripts.Player.PlayerStates.PlayerGroundedState
{
    public class PlayerWalkState : SrCatknight.Scripts.Player.PlayerStates.BaseStates.PlayerGroundedState
    {
        // Class Constructor
        public PlayerWalkState(PlayerController playerController, PlayerStateMachine playerStateMachine,
            DataPlayerController dataPlayerController, string animationBoolName)
            : base(playerController, playerStateMachine, dataPlayerController, animationBoolName)
        {
        }
        
        // 
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            Core.Movement.CheckIfShouldFlip(XInput);

            Core.Movement.SetVelocityX(DataPlayerController.walkVelocity * XInput);

            if (IsExitingState) return;
            
            if (XInput == 0)
            {
                PlayerStateMachine.ChangeState(PlayerController.IdleState);
            }
            else if (YInput == -1 && controllerCanCrouch)
            {
                PlayerStateMachine.ChangeState(PlayerController.CrouchMoveState);
            }
        }
    }
}
