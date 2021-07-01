using Player.Data;

namespace Player.PlayerStates.PlayerGroundedState
{
    public class EntityLandState : EntityGroundedState
    {
        // Class constructor
        public EntityLandState(Player entity, global::StateMachine.StateMachine stateMachine,
        PlayerData entityData, string animBoolName) : base(entity, stateMachine, entityData, animBoolName)
        {
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            //
            if (IsExitingState) return;
                
            if (XInput != 0)
            {
                StateMachine.ChangeState(Entity.RunState);
            }
            else if (IsAnimationFinished)
            {
                StateMachine.ChangeState(Entity.IdleState);
            }
        }
    }
}
