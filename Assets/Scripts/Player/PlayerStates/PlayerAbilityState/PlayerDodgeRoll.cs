using Player.Data;
using UnityEngine;

namespace Player.PlayerStates.PlayerAbilityState
{
    public class PlayerDodgeRoll : PlayerAbilityState
    {
        // Class Constructor
        public PlayerDodgeRoll(PlayerController playerController, StateMachine.StateMachine stateMachine, PlayerData playerData,
            string animBoolName) : base(playerController, stateMachine, playerData, animBoolName)
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
            
            Core.Movement.SetVelocityX(PlayerData.dodgeRollImpulse * XInput);

        }
        
        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (!(Time.time >= StartTime + PlayerData.dodgeRollLifeTime)) return;
            
            Core.Movement.CheckIfShouldFlip(-XInput);
            IsAbilityDone = true;
        }
    }
}
