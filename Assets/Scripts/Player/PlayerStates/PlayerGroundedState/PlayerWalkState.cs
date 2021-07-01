using Player.Data;
using StateMachine;

namespace Player.PlayerStates.PlayerGroundedState
{
    public class EntityWalkState : EntityGroundedState
    {
        // Class Constructor
        public EntityWalkState(Player entity, global::StateMachine.StateMachine stateMachine, PlayerData entityData,
            string animBoolName) : base(entity, stateMachine, entityData, animBoolName)
        {
        }
        
        public override void LogicUpdate()
        {
            base.LogicUpdate();

            Core.Movement.CheckIfShouldFlip(XInput);

            Core.Movement.SetVelocityX(EntityData.walkVelocity * XInput);

            if (IsExitingState) return;
            
            if (XInput == 0)
            {
                StateMachine.ChangeState(Entity.IdleState);
            }
            else if (YInput == -1)
            {
                StateMachine.ChangeState(Entity.CrouchMoveState);
            }
        }
    }
}
