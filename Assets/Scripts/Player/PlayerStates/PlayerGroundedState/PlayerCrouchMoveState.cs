using Player.Data;

namespace Player.PlayerStates.PlayerGroundedState
{
    public class PlayerCrouchMoveState : BaseStates.PlayerGroundedState
    {
        // Class Constructor
        public PlayerCrouchMoveState(PlayerController playerController, StateMachine.PlayerStateMachine playerStateMachine, DataPlayerController dataPlayerController,
            string animBoolName) : base(playerController, playerStateMachine, dataPlayerController, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            PlayerController.SetColliderHeight(DataPlayerController.crouchColliderHeight);
        }

        public override void Exit()
        {
            base.Exit();
            
            PlayerController.SetColliderHeight(DataPlayerController.normalColliderHeight);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsExitingState) return;
            
            Core.Movement.SetVelocityX(DataPlayerController.crouchMovementVelocity * Core.Movement.FacingDirection);
            Core.Movement.CheckIfShouldFlip(XInput);

            if(XInput == 0)
            {
                PlayerStateMachine.ChangeState(PlayerController.CrouchIdleState);
            }
            else if(YInput != -1 && !IsTouchingCeiling)
            {
                PlayerStateMachine.ChangeState(PlayerController.RunState);
            }
        }
    }
}
