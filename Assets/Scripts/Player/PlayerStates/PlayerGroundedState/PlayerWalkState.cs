using Player.Data;
using Player.StateMachine;

namespace Player.PlayerStates.PlayerGroundedState
{
    public class PlayerWalkState : PlayerGroundedState
    {
        // Class Constructor
        public PlayerWalkState(Player player, PlayerStateMachine stateMachine, PlayerData playerData,
            string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }
        
        public override void LogicUpdate()
        {
            base.LogicUpdate();

            Player.CheckIfShouldFlip(XInput);

            Player.SetVelocityX(PlayerData.walkVelocity * XInput);

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
