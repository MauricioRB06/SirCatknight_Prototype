using Player.Data;
using Player.StateMachine;
using UnityEngine;

namespace Player.PlayerStates.PlayerTouchingWallState
{
    public class PlayerWallGrabState : PlayerTouchingWallState
    {   
        // We use it to save the position of the player and prevent him from moving
        private Vector2 _holdPosition;
        
        // Class Constructor
        public PlayerWallGrabState(Player player, PlayerStateMachine stateMachine, PlayerData playerData,
            string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();

            _holdPosition = Player.transform.position;
            HoldPosition();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsExitingState) return;
            
            HoldPosition();

            if (YInput > 0)
            {
                StateMachine.ChangeState(Player.WallClimbState);
            }
            else if (YInput < 0 || !GrabInput)
            {
                StateMachine.ChangeState(Player.WallSlideState);
            }
        }

        private void HoldPosition()
        {
            Player.transform.position = _holdPosition;

            Player.SetVelocityX(0f);
            
            /* We have to set the speed of Y to 0, since Cinemachine works with the speed of the object so we have
               problems with the camera if we don't set  */
            Player.SetVelocityY(0f);
        }
    }
}
