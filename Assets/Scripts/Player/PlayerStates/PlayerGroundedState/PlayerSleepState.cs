using Player.Data;

namespace Player.PlayerStates.PlayerGroundedState
{
    public class PlayerSleepState : PlayerGroundedState
    {
        // Class Constructor
        public PlayerSleepState(PlayerController playerController, StateMachine.StateMachine stateMachine, 
            PlayerData playerData, string animBoolName) : base(playerController, stateMachine, playerData, animBoolName)
        {
        }
        
        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsExitingState) return;
            
            if (XInput != 0)
            {
                StateMachine.ChangeState(PlayerController.RunState);
            }
        }
    }
}
