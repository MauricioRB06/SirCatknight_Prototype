using Player.Data;
using StateMachine;

namespace Player.PlayerStates.PlayerGroundedState
{
    public class EntityCrouchIdleState : EntityGroundedState
    {
        // Class Constructor
        public EntityCrouchIdleState(Player entity, global::StateMachine.StateMachine stateMachine, PlayerData entityData,
            string animBoolName) : base(entity, stateMachine, entityData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            Core.Movement.SetVelocityZero();
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
            
            if(XInput != 0)
            {
                StateMachine.ChangeState(Entity.CrouchMoveState);
            }
            else if(YInput != -1 && !IsTouchingCeiling)
            {
                StateMachine.ChangeState(Entity.IdleState);
            }
        }
    }
}
