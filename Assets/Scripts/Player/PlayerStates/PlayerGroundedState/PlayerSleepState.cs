using Player.Data;
using StateMachine;

namespace Player.PlayerStates.PlayerGroundedState
{
    public class EntitySleepState : EntityGroundedState
    {
        // Class Constructor
        public EntitySleepState(Player entity, global::StateMachine.StateMachine stateMachine, PlayerData entityData,
            string animBoolName) : base(entity, stateMachine, entityData, animBoolName)
        {
        }
        
        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsExitingState) return;
            
            if (XInput != 0)
            {
                StateMachine.ChangeState(Entity.RunState);
            }
        }
    }
}
