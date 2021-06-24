using Player.Data;
using Player.StateMachine;
using UnityEngine;

namespace Player.PlayerStates.PlayerTouchingWallState
{
    public class PlayerWallJumpState : PlayerAbilityState.PlayerAbilityState
    {   
        // We use it to determine the character's jumping direction from the wall he's on
        private int _wallJumpDirection;
        
        // Generate id parameters for the animator
        private static readonly int YVelocity = Animator.StringToHash("yVelocity");
        private static readonly int XVelocity = Animator.StringToHash("xVelocity");

        // Class Constructor
        public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData,
            string animBoolName) : base(player, stateMachine, playerData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            Player.InputHandler.UseJumpInput();
            
            // We use it to reset the double jump (if we have) after jumping on the wall
            Player.JumpState.ResetAmountOfJumpsLeft();
            
            Player.SetVelocity(PlayerData.wallJumpVelocity, PlayerData.wallJumpAngle, _wallJumpDirection);
            Player.CheckIfShouldFlip(_wallJumpDirection);
            Player.JumpState.DecreaseAmountOfJumpsLeft();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            Player.PlayerAnimator.SetFloat(YVelocity, Player.CurrentVelocity.y);
            Player.PlayerAnimator.SetFloat(XVelocity, Mathf.Abs(Player.CurrentVelocity.x));

            if(Time.time >= StartTime + PlayerData.wallJumpTime)
            {
                IsAbilityDone = true;
            }
        }
        
        // We use it to determine the jump direction, based on the direction the player is facing
        public void DetermineWallJumpDirection(bool isTouchingWall)
        {
            if (isTouchingWall)
            {
                _wallJumpDirection = -Player.FacingDirection;
            }
            else
            {
                _wallJumpDirection = Player.FacingDirection;
            }
        }
    }
}
