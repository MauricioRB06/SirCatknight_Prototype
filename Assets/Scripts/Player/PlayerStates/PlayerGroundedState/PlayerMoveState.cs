using Player.Data;
using Player.StateMachine;

namespace Player.PlayerStates.PlayerGroundedState
{
    public class PlayerMoveState : PlayerGroundedState
    {
        // Class Constructor
        public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData,
            string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            Player.CheckIfShouldFlip(XInput);

            Player.SetVelocityX(PlayerData.movementVelocity * XInput);

            if (IsExitingState) return;
            
            if (XInput == 0)
            {
                StateMachine.ChangeState(Player.IdleState);
            }
            else if (YInput == -1)
            {
                StateMachine.ChangeState(Player.CrouchMoveState);
            }
        }
    }
}
