using Player.Data;
using Player.StateMachine;

namespace Player.PlayerStates.PlayerGroundedState
{
    public class PlayerLandState : PlayerGroundedState
    {
        // Class constructor
        public PlayerLandState(Player player, PlayerStateMachine stateMachine,
        PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            //
            if (IsExitingState) return;
                
            if (XInput != 0)
            {
                StateMachine.ChangeState(Player.MoveState);
            }
            else if (IsAnimationFinished)
            {
                StateMachine.ChangeState(Player.IdleState);
            }
        }
    }
}
