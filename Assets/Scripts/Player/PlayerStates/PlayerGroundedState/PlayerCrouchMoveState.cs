using Player.Data;

namespace Player.PlayerStates.PlayerGroundedState
{
    public class PlayerCrouchMoveState : PlayerGroundedState
    {
        // Class Constructor
        public PlayerCrouchMoveState(PlayerController playerController, StateMachine.StateMachine stateMachine, PlayerData playerData,
            string animBoolName) : base(playerController, stateMachine, playerData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            PlayerController.SetColliderHeight(PlayerData.crouchColliderHeight);
        }

        public override void Exit()
        {
            base.Exit();
            
            PlayerController.SetColliderHeight(PlayerData.normalColliderHeight);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsExitingState) return;
            
            Core.Movement.SetVelocityX(PlayerData.crouchMovementVelocity * Core.Movement.FacingDirection);
            Core.Movement.CheckIfShouldFlip(XInput);

            if(XInput == 0)
            {
                StateMachine.ChangeState(PlayerController.CrouchIdleState);
            }
            else if(YInput != -1 && !IsTouchingCeiling)
            {
                StateMachine.ChangeState(PlayerController.RunState);
            }
        }
    }
}
