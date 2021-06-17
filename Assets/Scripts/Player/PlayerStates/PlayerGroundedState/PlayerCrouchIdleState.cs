using Player.Data;
using Player.StateMachine;

namespace Player.PlayerStates.PlayerGroundedState
{
    public class PlayerCrouchIdleState : PlayerGroundedState
    {
        public PlayerCrouchIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData,
            string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            Player.SetVelocityZero();
            Player.SetColliderHeight(PlayerData.crouchColliderHeight);
        }

        public override void Exit()
        {
            base.Exit();
            Player.SetColliderHeight(PlayerData.standColliderHeight);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsExitingState) return;
            
            if(XInput != 0)
            {
                StateMachine.ChangeState(Player.CrouchMoveState);
            }
            else if(YInput != -1 && !IsTouchingCeiling)
            {
                StateMachine.ChangeState(Player.IdleState);
            }
        }
    }
}
