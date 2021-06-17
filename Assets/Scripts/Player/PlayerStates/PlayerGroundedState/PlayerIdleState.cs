using Player.Data;
using Player.StateMachine;

namespace Player.PlayerStates.PlayerGroundedState
{
    public class PlayerIdleState : PlayerGroundedState
    {
        // Class constructor
        public PlayerIdleState(Player player, PlayerStateMachine stateMachine,
            PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            // To avoid animator mistakes and avoid involuntary movements
            Player.SetVelocityX(0f);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsExitingState) return;
            
            if (XInput != 0)
            {
                StateMachine.ChangeState(Player.MoveState);
            }
            else if (YInput == -1)
            {
                StateMachine.ChangeState(Player.CrouchIdleState);
            }
        }
    }
}
