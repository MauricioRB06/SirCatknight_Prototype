using Player.Data;
using StateMachine;

namespace Player.PlayerStates.PlayerTouchingWallState
{
    public class EntityWallClimbState : EntityTouchingWallState
    {
        // Class Constructor
        public EntityWallClimbState(Player entity, global::StateMachine.StateMachine stateMachine, PlayerData entityData,
            string animBoolName) : base(entity, stateMachine, entityData, animBoolName)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsExitingState) return;
            
            Core.Movement.SetVelocityY(EntityData.wallClimbVelocity);

            if (YInput != 1)
            {
                StateMachine.ChangeState(Entity.WallGrabState);
            }
        }
    }
}
