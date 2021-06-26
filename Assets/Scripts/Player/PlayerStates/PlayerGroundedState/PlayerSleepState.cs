using Player.Data;
using Player.StateMachine;

namespace Player.PlayerStates.PlayerGroundedState
{
    public class PlayerSleepState : PlayerGroundedState
    {
        // Class Constructor
        public PlayerSleepState(Player player, PlayerStateMachine stateMachine, PlayerData playerData,
            string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }
        
        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsExitingState) return;
            
            if (XInput != 0)
            {
                StateMachine.ChangeState(Player.RunState);
            }
        }
    }
}
