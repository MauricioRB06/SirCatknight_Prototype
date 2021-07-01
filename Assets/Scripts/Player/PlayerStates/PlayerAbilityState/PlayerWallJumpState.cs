using Player.Data;
using StateMachine;
using UnityEngine;

namespace Player.PlayerStates.PlayerAbilityState
{
    public class EntityWallJumpState : EntityAbilityState
    {   
        // We use it to determine the character's jumping direction from the wall he's on
        private int _wallJumpDirection;
        
        // Generate id parameters for the animator
        private static readonly int YVelocity = Animator.StringToHash("yVelocity");
        private static readonly int XVelocity = Animator.StringToHash("xVelocity");

        // Class Constructor
        public EntityWallJumpState(Player entity, global::StateMachine.StateMachine stateMachine, PlayerData entityData,
            string animBoolName) : base(entity, stateMachine, entityData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            Entity.InputHandler.UseJumpInput();
            // We use it to reset the double jump (if we have) after jumping on the wall
            Entity.JumpState.ResetAmountOfJumpsLeft();
            Core.Movement.SetVelocity(EntityData.wallJumpVelocity, EntityData.wallJumpAngle, _wallJumpDirection);
            Core.Movement.CheckIfShouldFlip(_wallJumpDirection);
            Entity.JumpState.DecreaseAmountOfJumpsLeft();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            Entity.PlayerAnimator.SetFloat(YVelocity, Core.Movement.CurrentVelocity.y);
            Entity.PlayerAnimator.SetFloat(XVelocity, Mathf.Abs(Core.Movement.CurrentVelocity.x));

            if(Time.time >= StartTime + EntityData.wallJumpTime)
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
