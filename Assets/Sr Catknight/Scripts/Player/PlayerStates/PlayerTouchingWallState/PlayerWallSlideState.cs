
using Player.Data;
using StateMachine;
using UnityEngine;

namespace Player.PlayerStates.PlayerTouchingWallState
{
    public class PlayerWallSlideState : BaseStates.PlayerTouchingWallState
    {
        // Class Constructor
        public PlayerWallSlideState(PlayerController playerController, PlayerStateMachine playerStateMachine,
            DataPlayerController dataPlayerController, string animationBoolName)
            : base(playerController, playerStateMachine, dataPlayerController, animationBoolName)
        {
        }

        private const float DustTime = 0.2f;
        private float _lastDustTime;

        public override void Enter()
        {
            base.Enter();
            
            PlayerController.WallSlideDust();
            _lastDustTime = Time.time;
        }
        
        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsExitingState) return;
            
            Core.Movement.SetVelocityY(-DataPlayerController.wallSlideVelocity);
            
            if (Time.time > _lastDustTime + DustTime )
            {
                PlayerController.WallSlideDust();
                _lastDustTime = Time.time;
            }
            
            if (GrabInput && YInput == 0)
            {
                PlayerStateMachine.ChangeState(PlayerController.WallGrabState);
            }
        }
    }
}
