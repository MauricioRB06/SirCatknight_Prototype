using Player.Data;
using StateMachine;

namespace Player.PlayerStates.PlayerAbilityState
{
    public class EntityJumpState : EntityAbilityState
    {   
        private int _amountOfJumpsLeft;
        
        // Class constructor
        public EntityJumpState(Player entity, global::StateMachine.StateMachine stateMachine,
            PlayerData entityData, string animBoolName) : base(entity, stateMachine, entityData, animBoolName)
        {
            _amountOfJumpsLeft = entityData.amountOfJumps;
        }

        public override void Enter()
        {
            base.Enter();
            
            Entity.InputHandler.UseJumpInput();
            Core.Movement.SetVelocityY(EntityData.jumpForce);
            IsAbilityDone = true;
            _amountOfJumpsLeft--;
            Entity.InAirState.SetIsJumping();
        }

        public bool CanJump()
        {
            return _amountOfJumpsLeft > 0;
        }
        
        public void ResetAmountOfJumpsLeft() => _amountOfJumpsLeft = EntityData.amountOfJumps;
        
        public void DecreaseAmountOfJumpsLeft() => _amountOfJumpsLeft--;
    }
}
