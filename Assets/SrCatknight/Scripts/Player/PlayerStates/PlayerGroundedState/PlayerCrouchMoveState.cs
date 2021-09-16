using SrCatknight.Scripts.Player.Data;
using SrCatknight.Scripts.StateMachine;

namespace SrCatknight.Scripts.Player.PlayerStates.PlayerGroundedState
{
    public class PlayerCrouchMoveState : SrCatknight.Scripts.Player.PlayerStates.BaseStates.PlayerGroundedState
    {
        // Class Constructor
        public PlayerCrouchMoveState(PlayerController playerController, PlayerStateMachine playerStateMachine,
            DataPlayerController dataPlayerController, string animationBoolName)
            : base(playerController, playerStateMachine, dataPlayerController, animationBoolName)
        {
        }

        // 
        public override void Enter()
        {
            base.Enter();
            
            PlayerController.SetColliderHeight(DataPlayerController.crouchColliderHeight);
        }

        // 
        public override void Exit()
        {
            base.Exit();
            
            PlayerController.SetColliderHeight(DataPlayerController.normalColliderHeight);
        }

        // 
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

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
