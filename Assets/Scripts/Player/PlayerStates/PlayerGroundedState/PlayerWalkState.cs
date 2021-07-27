using Player.Data;

namespace Player.PlayerStates.PlayerGroundedState
{
    public class PlayerWalkState : PlayerGroundedState
    {
        // Class Constructor
        public PlayerWalkState(PlayerController playerController, StateMachine.StateMachine stateMachine, PlayerData playerData,
            string animBoolName) : base(playerController, stateMachine, playerData, animBoolName)
        {
        }
        
        public override void LogicUpdate()
        {
            base.LogicUpdate();

            Core.Movement.CheckIfShouldFlip(XInput);

            Core.Movement.SetVelocityX(PlayerData.walkVelocity * XInput);

            if (IsExitingState) return;
            
            if (XInput == 0)
            {
                StateMachine.ChangeState(PlayerController.IdleState);
            }
            else if (YInput == -1)
            {
                StateMachine.ChangeState(PlayerController.CrouchMoveState);
            }
        }
    }
}
