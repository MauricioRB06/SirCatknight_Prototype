using Player.Data;

namespace Player.PlayerStates.PlayerGroundedState
{
    public class PlayerCrouchIdleState : PlayerGroundedState
    {
        // Class Constructor
        public PlayerCrouchIdleState(PlayerController playerController,StateMachine.StateMachine stateMachine, PlayerData playerData,
            string animBoolName) : base(playerController, stateMachine, playerData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            Core.Movement.SetVelocityZero();
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
            
            if(XInput != 0)
            {
                StateMachine.ChangeState(PlayerController.CrouchMoveState);
            }
            else if(YInput != -1 && !IsTouchingCeiling)
            {
                StateMachine.ChangeState(PlayerController.IdleState);
            }
        }
    }
}
