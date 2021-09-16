using SrCatknight.Scripts.Player.Data;
using SrCatknight.Scripts.StateMachine;

namespace SrCatknight.Scripts.Player.PlayerStates.PlayerGroundedState
{
    public class PlayerSleepState : SrCatknight.Scripts.Player.PlayerStates.BaseStates.PlayerGroundedState
    {
        // Class Constructor
        public PlayerSleepState(PlayerController playerController, PlayerStateMachine playerStateMachine, 
            DataPlayerController dataPlayerController, string animationBoolName)
            : base(playerController, playerStateMachine, dataPlayerController, animationBoolName)
        {
        }
        
        // 
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (IsExitingState) return;
            
            if (XInput != 0)
            {
                PlayerStateMachine.ChangeState(PlayerController.RunState);
            }
        }
    }
}
