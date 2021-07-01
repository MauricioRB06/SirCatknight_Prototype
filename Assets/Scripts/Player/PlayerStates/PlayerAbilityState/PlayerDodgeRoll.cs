using Player.Data;
using StateMachine;
using UnityEngine;

namespace Player.PlayerStates.PlayerAbilityState
{
    public class EntityDodgeRoll : EntityAbilityState
    {
        // Class Constructor
        public EntityDodgeRoll(Player entity, global::StateMachine.StateMachine stateMachine, PlayerData entityData,
            string animBoolName) : base(entity, stateMachine, entityData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            Entity.InputHandler.UseDodgeRollInput();
            
            Core.Movement.CheckIfShouldFlip(XInput);

            if (XInput == 0)
            {
                XInput *= Core.Movement.FacingDirection;
            } 
            
            Core.Movement.SetVelocityX(EntityData.dodgeRollImpulse * XInput);

        }
        
        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (!(Time.time >= StartTime + EntityData.dodgeRollLifeTime)) return;
            
            Core.Movement.CheckIfShouldFlip(-XInput);
            IsAbilityDone = true;
        }
    }
}
