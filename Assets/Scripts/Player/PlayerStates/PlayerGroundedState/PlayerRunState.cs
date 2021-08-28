
using Player.Data;

namespace Player.PlayerStates.PlayerGroundedState
{
    public class PlayerRunState : BaseStates.PlayerGroundedState
    {
        // Class Constructor
        public PlayerRunState(PlayerController playerController, StateMachine.PlayerStateMachine playerStateMachine,
            DataPlayerController dataPlayerController, string animationBoolName) :
            base(playerController, playerStateMachine, dataPlayerController, animationBoolName)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            Core.Movement.CheckIfShouldFlip(XInput);

            Core.Movement.SetVelocityX(DataPlayerController.runVelocity * XInput);
            
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
