using Player.Data;
using Player.StateMachine;

namespace Player.PlayerStates.PlayerAbilityState
{
    public class PlayerAbilityState : PlayerState
    {
        // We use it to know if the skill has already been performed
        protected bool IsAbilityDone;
        
        // To check if the character is on the ground 
        private bool _isGrounded;
        
        // Class constructor
        protected PlayerAbilityState(Player player, PlayerStateMachine stateMachine, PlayerData playerData,
            string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }

        protected override void DoChecks()
        {
            base.DoChecks();
            _isGrounded = Player.CheckIfGrounded();
        }

        public override void Enter()
        {
            base.Enter();
            IsAbilityDone = false;
        }
        
        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (!IsAbilityDone) return;
            
            if (_isGrounded && Player.CurrentVelocity.y < 0.01f)
            {
                StateMachine.ChangeState(Player.IdleState);
            }
            else
            {
                StateMachine.ChangeState(Player.InAirState);
            }
        }
    }
}
