//using _Development.Scripts.Mauricio;

using StateMachine;
using Player.Data;

namespace Player.PlayerStates.PlayerAbilityState
{
    public class PlayerJumpState : BaseStates.PlayerAbilityState
    {   
        // 
        private int _amountOfJumpsLeft;
        
        // Class constructor
        public PlayerJumpState(PlayerController playerController, PlayerStateMachine playerStateMachine,
            DataPlayerController dataPlayerController, string animationBoolName)
            : base(playerController, playerStateMachine, dataPlayerController, animationBoolName)
        {
            _amountOfJumpsLeft = dataPlayerController.amountOfJumps;
        }
        
        // 
        public override void Enter()
        {
            base.Enter();
            PlayerController.JumpDust();
            PlayerController.InputHandler.UseJumpInput();
            Core.Movement.SetVelocityY(DataPlayerController.jumpForce);
            IsAbilityDone = true;
            _amountOfJumpsLeft--;
            PlayerController.InAirState.SetIsJumping();
        }
        
        // 
        public bool CanJump()
        {
            return _amountOfJumpsLeft > 0;
        }
        
        public void ResetAmountOfJumpsLeft() => _amountOfJumpsLeft = DataPlayerController.amountOfJumps;
        
        public void DecreaseAmountOfJumpsLeft() => _amountOfJumpsLeft--;
    }
}
