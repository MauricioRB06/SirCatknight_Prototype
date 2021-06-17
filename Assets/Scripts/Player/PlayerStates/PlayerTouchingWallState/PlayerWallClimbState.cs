using Player.Data;
using Player.StateMachine;

namespace Player.PlayerStates.PlayerTouchingWallState
{
    public class PlayerWallClimbState : PlayerTouchingWallState
    {
        // Class Constructor
        public PlayerWallClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData,
            string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsExitingState) return;
            
            Player.SetVelocityY(PlayerData.wallClimbVelocity);

            if (YInput != 1)
            {
                StateMachine.ChangeState(Player.WallGrabState);
            }
        }
    }
}
