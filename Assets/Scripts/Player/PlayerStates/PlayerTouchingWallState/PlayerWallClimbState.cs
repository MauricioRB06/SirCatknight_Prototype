using Player.Data;

namespace Player.PlayerStates.PlayerTouchingWallState
{
    public class PlayerWallClimbState : BaseStates.PlayerTouchingWallState
    {
        // Class Constructor
        public PlayerWallClimbState(PlayerController playerController, StateMachine.PlayerStateMachine playerStateMachine, DataPlayerController dataPlayerController,
            string animBoolName) : base(playerController, playerStateMachine, dataPlayerController, animBoolName)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsExitingState) return;
            
            Core.Movement.SetVelocityY(DataPlayerController.wallClimbVelocity);

            if (YInput != 1)
            {
                PlayerStateMachine.ChangeState(PlayerController.WallGrabState);
            }
        }
    }
}
