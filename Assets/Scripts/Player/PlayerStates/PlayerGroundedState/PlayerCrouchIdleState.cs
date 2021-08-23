using Player.Data;

namespace Player.PlayerStates.PlayerGroundedState
{
    public class PlayerCrouchIdleState : BaseStates.PlayerGroundedState
    {
        // Class Constructor
        public PlayerCrouchIdleState(PlayerController playerController,StateMachine.PlayerStateMachine playerStateMachine, DataPlayerController dataPlayerController,
            string animBoolName) : base(playerController, playerStateMachine, dataPlayerController, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            Core.Movement.SetVelocityZero();
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
            
            if(XInput != 0)
            {
                PlayerStateMachine.ChangeState(PlayerController.CrouchMoveState);
            }
            else if(YInput != -1 && !IsTouchingCeiling)
            {
                PlayerStateMachine.ChangeState(PlayerController.IdleState);
            }
        }
    }
}
