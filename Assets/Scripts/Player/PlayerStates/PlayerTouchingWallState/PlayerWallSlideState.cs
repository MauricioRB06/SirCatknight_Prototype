using Player.Data;

namespace Player.PlayerStates.PlayerTouchingWallState
{
    public class PlayerWallSlideState : BaseStates.PlayerTouchingWallState
    {
        // Class Constructor
        public PlayerWallSlideState(PlayerController playerController, StateMachine.PlayerStateMachine playerStateMachine, DataPlayerController dataPlayerController,
            string animationBoolName): base(playerController, playerStateMachine, dataPlayerController, animationBoolName)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsExitingState) return;
            
            Core.Movement.SetVelocityY(-DataPlayerController.wallSlideVelocity);

            if (GrabInput && YInput == 0)
            {
                PlayerStateMachine.ChangeState(PlayerController.WallGrabState);
            }
        }
    }
}
