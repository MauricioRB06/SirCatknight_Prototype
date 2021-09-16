using SrCatknight.Scripts.Player.Data;
using SrCatknight.Scripts.StateMachine;

namespace SrCatknight.Scripts.Player.PlayerStates.PlayerGroundedState
{
    public class PlayerCrouchIdleState : SrCatknight.Scripts.Player.PlayerStates.BaseStates.PlayerGroundedState
    {
        // Class Constructor
        public PlayerCrouchIdleState(PlayerController playerController,PlayerStateMachine playerStateMachine,
            DataPlayerController dataPlayerController, string animationBoolName)
            : base(playerController, playerStateMachine, dataPlayerController, animationBoolName)
        {
        }
        
        // 
        public override void Enter()
        {
            base.Enter();

            Core.Movement.SetVelocityZero();
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
