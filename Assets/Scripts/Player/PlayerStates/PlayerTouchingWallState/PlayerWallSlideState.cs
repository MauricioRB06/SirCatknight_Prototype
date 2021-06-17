using Player.Data;
using Player.StateMachine;

namespace Player.PlayerStates.PlayerTouchingWallState
{
    public class PlayerWallSlideState : PlayerTouchingWallState
    {
        // Class Constructor
        public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, PlayerData playerData,
            string animBoolName): base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsExitingState) return;
            
            Player.SetVelocityY(-PlayerData.wallSlideVelocity);

            if (GrabInput && YInput == 0)
            {
                StateMachine.ChangeState(Player.WallGrabState);
            }
        }
    }
}
