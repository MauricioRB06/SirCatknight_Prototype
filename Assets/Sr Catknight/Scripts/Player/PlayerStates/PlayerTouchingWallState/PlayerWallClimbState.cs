
using Player.Data;
using StateMachine;

namespace Player.PlayerStates.PlayerTouchingWallState
{
    public class PlayerWallClimbState : BaseStates.PlayerTouchingWallState
    {
        // Class Constructor
        public PlayerWallClimbState(PlayerController playerController, PlayerStateMachine playerStateMachine,
            DataPlayerController dataPlayerController, string animationBoolName)
            : base(playerController, playerStateMachine, dataPlayerController, animationBoolName)
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
