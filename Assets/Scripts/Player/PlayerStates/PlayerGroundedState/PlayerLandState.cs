using Player.Data;

namespace Player.PlayerStates.PlayerGroundedState
{
    public class PlayerLandState : BaseStates.PlayerGroundedState
    {
        // Class constructor
        public PlayerLandState(PlayerController playerController, StateMachine.PlayerStateMachine playerStateMachine,
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
            else if (IsAnimationFinished)
            {
                PlayerStateMachine.ChangeState(PlayerController.IdleState);
            }
        }
    }
}
