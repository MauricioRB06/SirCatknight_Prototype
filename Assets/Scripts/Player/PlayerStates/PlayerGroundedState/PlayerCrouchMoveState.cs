using Player.Data;
using Player.StateMachine;

namespace Player.PlayerStates.PlayerGroundedState
{
    public class PlayerCrouchMoveState : PlayerGroundedState
    {
        // Class Constructor
        public PlayerCrouchMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData,
            string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            Player.SetColliderHeight(PlayerData.crouchColliderHeight);
        }

        public override void Exit()
        {
            base.Exit();
            
            Player.SetColliderHeight(PlayerData.normalColliderHeight);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsExitingState) return;
            
            Player.SetVelocityX(PlayerData.crouchMovementVelocity * Player.FacingDirection);
            Player.CheckIfShouldFlip(XInput);

            if(XInput == 0)
            {
                StateMachine.ChangeState(Player.CrouchIdleState);
            }
            else if(YInput != -1 && !IsTouchingCeiling)
            {
                StateMachine.ChangeState(Player.RunState);
            }
        }
    }
}
