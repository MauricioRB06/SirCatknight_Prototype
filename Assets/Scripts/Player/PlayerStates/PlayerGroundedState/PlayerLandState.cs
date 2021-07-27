using Player.Data;

namespace Player.PlayerStates.PlayerGroundedState
{
    public class PlayerLandState : PlayerGroundedState
    {
        // Class constructor
        public PlayerLandState(PlayerController playerController, StateMachine.StateMachine stateMachine,
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
            else if (IsAnimationFinished)
            {
                StateMachine.ChangeState(PlayerController.IdleState);
            }
        }
    }
}
