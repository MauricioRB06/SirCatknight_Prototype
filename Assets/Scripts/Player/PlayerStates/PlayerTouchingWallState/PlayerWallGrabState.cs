using Player.Data;
using StateMachine;
using UnityEngine;

namespace Player.PlayerStates.PlayerTouchingWallState
{
    public class EntityWallGrabState : EntityTouchingWallState
    {   
        // We use it to save the position of the entity and prevent him from moving
        private Vector2 _holdPosition;
        
        // Class Constructor
        public EntityWallGrabState(Player entity, global::StateMachine.StateMachine stateMachine, PlayerData entityData,
            string animBoolName) : base(entity, stateMachine, entityData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _holdPosition = Entity.transform.position;
            HoldPosition();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsExitingState) return;
            
            HoldPosition();

            if (YInput > 0)
            {
                StateMachine.ChangeState(Entity.WallClimbState);
            }
            else if (YInput < 0 || !GrabInput)
            {
                StateMachine.ChangeState(Entity.WallSlideState);
            }
        }

        private void HoldPosition()
        {
            Entity.transform.position = _holdPosition;

            Core.Movement.SetVelocityX(0f);
            
            /* We have to set the speed of Y to 0, since Cinemachine works with the speed of the object so we have
               problems with the camera if we don't set  */
            Core.Movement.SetVelocityY(0f);
        }
    }
}
