using _Development.Scripts.Mauricio;
using Player.Data;

namespace Player.PlayerStates.PlayerAbilityState
{
    public class PlayerJumpState : PlayerAbilityState
    {   
        private int _amountOfJumpsLeft;
        
        // Class constructor
        public PlayerJumpState(PlayerController playerController, StateMachine.StateMachine stateMachine,
            PlayerData playerData, string animBoolName) : base(playerController, stateMachine, playerData, animBoolName)
        {
            _amountOfJumpsLeft = playerData.amountOfJumps;
        }

        public override void Enter()
        {
            base.Enter();
            
            PlayerController.InputHandler.UseJumpInput();
            Core.Movement.SetVelocityY(PlayerData.jumpForce);
            IsAbilityDone = true;
            AudioManager.PlaySound(PlayerController.playerSounds.PlayerJump);
            _amountOfJumpsLeft--;
            PlayerController.InAirState.SetIsJumping();
        }

        public bool CanJump()
        {
            return _amountOfJumpsLeft > 0;
        }
        
        public void ResetAmountOfJumpsLeft() => _amountOfJumpsLeft = PlayerData.amountOfJumps;
        
        public void DecreaseAmountOfJumpsLeft() => _amountOfJumpsLeft--;
    }
}
