using Player.Data;

namespace Player.PlayerStates.PlayerGroundedState
{
    public class PlayerWalkState : BaseStates.PlayerGroundedState
    {
        // Class Constructor
        public PlayerWalkState(PlayerController playerController, StateMachine.PlayerStateMachine playerStateMachine, DataPlayerController dataPlayerController,
            string animBoolName) : base(playerController, playerStateMachine, dataPlayerController, animBoolName)
        {
        }
        
        public override void LogicUpdate()
        {
            base.LogicUpdate();

            Core.Movement.CheckIfShouldFlip(XInput);

            Core.Movement.SetVelocityX(DataPlayerController.walkVelocity * XInput);

            if (IsExitingState) return;
            
            if (XInput == 0)
            {
                PlayerStateMachine.ChangeState(PlayerController.IdleState);
            }
            else if (YInput == -1)
            {
                PlayerStateMachine.ChangeState(PlayerController.CrouchMoveState);
            }
        }
    }
}
