
using Player.Data;
using StateMachine;
using UnityEngine;

namespace Player.PlayerStates.PlayerTouchingWallState
{
    public class PlayerWallGrabState : BaseStates.PlayerTouchingWallState
    {   
        // We use it to save the position of the entity and prevent him from moving
        private Vector2 _holdPosition;
        
        // Class Constructor
        public PlayerWallGrabState(PlayerController playerController, PlayerStateMachine playerStateMachine,
            DataPlayerController dataPlayerController, string animationBoolName)
            : base(playerController, playerStateMachine, dataPlayerController, animationBoolName)
        {
        }
        
        public override void Enter()
        {
            base.Enter();

            _holdPosition = PlayerController.transform.position;
            HoldPosition();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsExitingState) return;
            
            HoldPosition();

            if (YInput > 0)
            {
                PlayerStateMachine.ChangeState(PlayerController.WallClimbState);
            }
            else if ((YInput < 0 || !GrabInput) && controllerCanWallSlide)
            {
                PlayerStateMachine.ChangeState(PlayerController.WallSlideState);
            }
        }

        private void HoldPosition()
        {
            PlayerController.transform.position = _holdPosition;

            Core.Movement.SetVelocityX(0f);
            
            /* We have to set the speed of Y to 0, since Cinemachine works with the speed of the object so we have
               problems with the camera if we don't set  */
            Core.Movement.SetVelocityY(0f);
        }
    }
}
