﻿using Player.Data;
using Player.StateMachine;

namespace Player.PlayerStates.PlayerAbilityState
{
    public class PlayerJumpState : PlayerAbilityState
    {   
        private int _amountOfJumpsLeft;
        
        // Class constructor
        public PlayerJumpState(Player player, PlayerStateMachine stateMachine,
            PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
            _amountOfJumpsLeft = playerData.amountOfJumps;
        }

        public override void Enter()
        {
            base.Enter();
            Player.InputHandler.UseJumpInput();
            Player.SetVelocityY(PlayerData.jumpForce);
            IsAbilityDone = true;
            _amountOfJumpsLeft--;
            Player.InAirState.SetIsJumping();
        }

        public bool CanJump()
        {
            return _amountOfJumpsLeft > 0;
        }
        
        public void ResetAmountOfJumpsLeft() => _amountOfJumpsLeft = PlayerData.amountOfJumps;
        
        public void DecreaseAmountOfJumpsLeft() => _amountOfJumpsLeft--;
    }
}
