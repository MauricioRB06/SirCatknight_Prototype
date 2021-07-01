using Player.Data;

namespace Player.PlayerStates.PlayerGroundedState
{
    public class EntityRunState : EntityGroundedState
    {
        // Class Constructor
        public EntityRunState(Player entity, global::StateMachine.StateMachine stateMachine, PlayerData entityData,
            string animBoolName) : base(entity, stateMachine, entityData, animBoolName)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            Core.Movement.CheckIfShouldFlip(XInput);

            Core.Movement.SetVelocityX(EntityData.runVelocity * XInput);

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
