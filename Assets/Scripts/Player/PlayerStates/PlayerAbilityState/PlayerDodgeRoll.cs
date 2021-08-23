using Player.Data;
using UnityEngine;

namespace Player.PlayerStates.PlayerAbilityState
{
    public class PlayerDodgeRoll : BaseStates.PlayerAbilityState
    {
        // Class Constructor
        public PlayerDodgeRoll(PlayerController playerController, StateMachine.PlayerStateMachine playerStateMachine, DataPlayerController dataPlayerController,
            string animationBoolName) : base(playerController, playerStateMachine, dataPlayerController, animationBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            PlayerController.InputHandler.UseDodgeRollInput();
            
            Core.Movement.CheckIfShouldFlip(XInput);

            if (XInput == 0)
            {
                XInput *= Core.Movement.FacingDirection;
            } 
            
            Core.Movement.SetVelocityX(DataPlayerController.dodgeRollImpulse * XInput);

        }
        
        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (!(Time.time >= StartTime + DataPlayerController.dodgeRollLifeTime)) return;
            
            Core.Movement.CheckIfShouldFlip(-XInput);
            IsAbilityDone = true;
        }
    }
}
