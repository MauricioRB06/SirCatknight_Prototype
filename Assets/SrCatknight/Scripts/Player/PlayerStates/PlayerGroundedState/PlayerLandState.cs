using SrCatknight.Scripts.Player.Data;
using SrCatknight.Scripts.StateMachine;

namespace SrCatknight.Scripts.Player.PlayerStates.PlayerGroundedState
{
    public class PlayerLandState : SrCatknight.Scripts.Player.PlayerStates.BaseStates.PlayerGroundedState
    {
        // Class constructor
        public PlayerLandState(PlayerController playerController, PlayerStateMachine playerStateMachine,
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
            else if (IsAnimationFinished)
            {
                PlayerStateMachine.ChangeState(PlayerController.IdleState);
            }
        }
    }
}
