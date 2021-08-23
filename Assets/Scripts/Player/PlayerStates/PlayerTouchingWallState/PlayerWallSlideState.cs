using Player.Data;

namespace Player.PlayerStates.PlayerTouchingWallState
{
    public class PlayerWallSlideState : BaseStates.PlayerTouchingWallState
    {
        // Class Constructor
        public PlayerWallSlideState(PlayerController playerController, StateMachine.PlayerStateMachine playerStateMachine, DataPlayerController dataPlayerController,
            string animBoolName): base(playerController, playerStateMachine, dataPlayerController, animBoolName)
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
