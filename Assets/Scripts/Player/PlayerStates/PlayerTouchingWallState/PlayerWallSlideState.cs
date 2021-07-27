using Player.Data;

namespace Player.PlayerStates.PlayerTouchingWallState
{
    public class PlayerWallSlideState : PlayerTouchingWallState
    {
        // Class Constructor
        public PlayerWallSlideState(PlayerController playerController, StateMachine.StateMachine stateMachine, PlayerData playerData,
            string animBoolName): base(playerController, stateMachine, playerData, animBoolName)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsExitingState) return;
            
            Core.Movement.SetVelocityY(-PlayerData.wallSlideVelocity);

            if (GrabInput && YInput == 0)
            {
                StateMachine.ChangeState(PlayerController.WallGrabState);
            }
        }
    }
}
