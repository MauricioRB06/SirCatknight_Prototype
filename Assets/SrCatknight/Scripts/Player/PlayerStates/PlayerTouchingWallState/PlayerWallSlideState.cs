using SrCatknight.Scripts.Player.Data;
using SrCatknight.Scripts.StateMachine;
using UnityEngine;

namespace SrCatknight.Scripts.Player.PlayerStates.PlayerTouchingWallState
{
    public class PlayerWallSlideState : SrCatknight.Scripts.Player.PlayerStates.BaseStates.PlayerTouchingWallState
    {
        // Class Constructor
        public PlayerWallSlideState(PlayerController playerController, PlayerStateMachine playerStateMachine,
            DataPlayerController dataPlayerController, string animationBoolName)
            : base(playerController, playerStateMachine, dataPlayerController, animationBoolName)
        {
        }

        // 
        private const float DustTime = 0.2f;
        private float _lastDustTime;

        // 
        public override void Enter()
        {
            base.Enter();
            
            PlayerController.WallSlideDust();
            _lastDustTime = Time.time;
        }
        
        // 
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

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
