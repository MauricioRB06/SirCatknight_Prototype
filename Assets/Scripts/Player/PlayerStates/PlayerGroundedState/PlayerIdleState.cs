using Player.Data;
using StateMachine;
using UnityEngine;

namespace Player.PlayerStates.PlayerGroundedState
{
    public class EntityIdleState : EntityGroundedState
    {
        // Class constructor
        public EntityIdleState(Player entity, global::StateMachine.StateMachine stateMachine,
            PlayerData entityData, string animBoolName) : base(entity, stateMachine, entityData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            // To avoid animator mistakes and avoid involuntary movements
            Core.Movement.SetVelocityX(0f);
            StartTime = Time.time;
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsExitingState) return;
            
            if (XInput != 0)
            {
                StateMachine.ChangeState(Entity.RunState);
            }
            else if (YInput == -1)
            {
                StateMachine.ChangeState(Entity.CrouchIdleState);
            }
            else if (Time.time >= StartTime + EntityData.sleepTime)
            {
                StateMachine.ChangeState(Entity.SleepState);
            }
        }
    }
}
