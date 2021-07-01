using Player.Data;
using StateMachine;

namespace Player.PlayerStates.PlayerTouchingWallState
{
    public class EntityWallSlideState : EntityTouchingWallState
    {
        // Class Constructor
        public EntityWallSlideState(Player entity, global::StateMachine.StateMachine stateMachine, PlayerData entityData,
            string animBoolName): base(entity, stateMachine, entityData, animBoolName)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsExitingState) return;
            
            Core.Movement.SetVelocityY(-EntityData.wallSlideVelocity);

            if (GrabInput && YInput == 0)
            {
                StateMachine.ChangeState(Entity.WallGrabState);
            }
        }
    }
}
