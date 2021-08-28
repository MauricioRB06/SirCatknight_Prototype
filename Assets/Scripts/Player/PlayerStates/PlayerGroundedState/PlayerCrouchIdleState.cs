
using Player.Data;
using StateMachine;

namespace Player.PlayerStates.PlayerGroundedState
{
    public class PlayerCrouchIdleState : BaseStates.PlayerGroundedState
    {
        // Class Constructor
        public PlayerCrouchIdleState(PlayerController playerController,PlayerStateMachine playerStateMachine,
            DataPlayerController dataPlayerController, string animationBoolName)
            : base(playerController, playerStateMachine, dataPlayerController, animationBoolName)
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
