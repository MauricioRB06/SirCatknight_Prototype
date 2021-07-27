using Development.Scripts.Mauricio;
using Player.Data;

namespace Player.PlayerStates.PlayerGroundedState
{
    public class PlayerRunState : PlayerGroundedState
    {
        // Class Constructor
        public PlayerRunState(PlayerController playerController, StateMachine.StateMachine stateMachine, PlayerData playerData,
            string animBoolName) : base(playerController, stateMachine, playerData, animBoolName) { }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            Core.Movement.CheckIfShouldFlip(XInput);

            Core.Movement.SetVelocityX(PlayerData.runVelocity * XInput);
            
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
