using SrCatknight.Scripts.Player.Data;
using SrCatknight.Scripts.StateMachine;

namespace SrCatknight.Scripts.Player.PlayerStates.PlayerTouchingWallState
{
    public class PlayerWallClimbState : SrCatknight.Scripts.Player.PlayerStates.BaseStates.PlayerTouchingWallState
    {
        // Class Constructor
        public PlayerWallClimbState(PlayerController playerController, PlayerStateMachine playerStateMachine,
            DataPlayerController dataPlayerController, string animationBoolName)
            : base(playerController, playerStateMachine, dataPlayerController, animationBoolName)
        {
        }
        
        // 
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (IsExitingState) return;
            
            Core.Movement.SetVelocityY(DataPlayerController.wallClimbVelocity);

            if (YInput != 1)
            {
                PlayerStateMachine.ChangeState(PlayerController.WallGrabState);
            }
        }
    }
}
