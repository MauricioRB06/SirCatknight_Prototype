using SrCatknight.Scripts.Player.Data;
using SrCatknight.Scripts.StateMachine;
using UnityEngine;

namespace SrCatknight.Scripts.Player.PlayerStates.PlayerAbilityState
{
    public class PlayerWallJumpState : SrCatknight.Scripts.Player.PlayerStates.BaseStates.PlayerAbilityState
    {   
        // We use it to determine the character's jumping direction from the wall he's on
        private int _wallJumpDirection;
        
        // Generate id parameters for the animator
        private static readonly int YVelocity = Animator.StringToHash("yVelocity");
        private static readonly int XVelocity = Animator.StringToHash("xVelocity");

        // Class Constructor
        public PlayerWallJumpState(PlayerController playerController, PlayerStateMachine playerStateMachine,
            DataPlayerController dataPlayerController, string animationBoolName)
            : base(playerController, playerStateMachine, dataPlayerController, animationBoolName)
        {
        }
        
        // 
        public override void Enter()
        {
            base.Enter();
            
            PlayerController.InputHandler.UseJumpInput();
            
            // We use it to reset the double jump (if we have) after jumping on the wall
            PlayerController.JumpState.ResetAmountOfJumpsLeft();
            
            Core.Movement.SetVelocity(DataPlayerController.wallJumpVelocity, DataPlayerController.wallJumpAngle,
                _wallJumpDirection);
            Core.Movement.CheckIfShouldFlip(_wallJumpDirection);
            
            PlayerController.JumpState.DecreaseAmountOfJumpsLeft();
        }

        // 
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            PlayerController.PlayerAnimator.SetFloat(YVelocity, Core.Movement.CurrentVelocity.y);
            PlayerController.PlayerAnimator.SetFloat(XVelocity, Mathf.Abs(Core.Movement.CurrentVelocity.x));

            if(Time.time >= StartTime + DataPlayerController.wallJumpTime)
            {
                IsAbilityDone = true;
            }
        }
        
        // We use it to determine the jump direction, based on the direction the entity is facing
        public void DetermineWallJumpDirection(bool isTouchingWall)
        {
            if (isTouchingWall)
            {
                _wallJumpDirection = -Core.Movement.FacingDirection;
            }
            else
            {
                _wallJumpDirection = Core.Movement.FacingDirection;
            }
        }
    }
}
