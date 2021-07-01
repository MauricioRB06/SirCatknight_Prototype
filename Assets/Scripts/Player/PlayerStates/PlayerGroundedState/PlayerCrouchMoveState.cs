using Player.Data;
using StateMachine;

namespace Player.PlayerStates.PlayerGroundedState
{
    public class EntityCrouchMoveState : EntityGroundedState
    {
        // Class Constructor
        public EntityCrouchMoveState(Player entity, global::StateMachine.StateMachine stateMachine, PlayerData entityData,
            string animBoolName) : base(entity, stateMachine, entityData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            Entity.SetColliderHeight(EntityData.crouchColliderHeight);
        }

        public override void Exit()
        {
            base.Exit();
            
            Entity.SetColliderHeight(EntityData.normalColliderHeight);
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsExitingState) return;
            
            Core.Movement.SetVelocityX(EntityData.crouchMovementVelocity * Core.Movement.FacingDirection);
            Core.Movement.CheckIfShouldFlip(XInput);

            if(XInput == 0)
            {
                StateMachine.ChangeState(Entity.CrouchIdleState);
            }
            else if(YInput != -1 && !IsTouchingCeiling)
            {
                StateMachine.ChangeState(Entity.RunState);
            }
        }
    }
}
