using Player.Data;

namespace Player.PlayerStates.PlayerTouchingWallState
{
    public class PlayerWallClimbState : PlayerTouchingWallState
    {
        // Class Constructor
        public PlayerWallClimbState(PlayerController playerController, StateMachine.StateMachine stateMachine, PlayerData playerData,
            string animBoolName) : base(playerController, stateMachine, playerData, animBoolName)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsExitingState) return;
            
            Core.Movement.SetVelocityY(PlayerData.wallClimbVelocity);

            if (YInput != 1)
            {
                StateMachine.ChangeState(PlayerController.WallGrabState);
            }
        }
    }
}
