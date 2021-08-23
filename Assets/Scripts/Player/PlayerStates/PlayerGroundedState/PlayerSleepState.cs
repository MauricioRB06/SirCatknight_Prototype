using Player.Data;

namespace Player.PlayerStates.PlayerGroundedState
{
    public class PlayerSleepState : BaseStates.PlayerGroundedState
    {
        // Class Constructor
        public PlayerSleepState(PlayerController playerController, StateMachine.PlayerStateMachine playerStateMachine, 
            DataPlayerController dataPlayerController, string animBoolName) : base(playerController, playerStateMachine, dataPlayerController, animBoolName)
        {
        }
        
        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsExitingState) return;
            
            if (XInput != 0)
            {
                PlayerStateMachine.ChangeState(PlayerController.RunState);
            }
        }
    }
}
