
// The purpose of this Script is:
/*  */

using SrCatknight.Scripts.Player.Data;
using SrCatknight.Scripts.StateMachine;
using UnityEngine;

namespace SrCatknight.Scripts.Player.PlayerStates.PlayerGroundedState
{
    public class PlayerIdleState : SrCatknight.Scripts.Player.PlayerStates.BaseStates.PlayerGroundedState
    {
        // Class constructor
        public PlayerIdleState(PlayerController playerController, PlayerStateMachine playerStateMachine,
            DataPlayerController dataPlayerController, string animationBoolName)
            : base(playerController, playerStateMachine, dataPlayerController, animationBoolName)
        {
        }

        // 
        public override void Enter()
        {
            base.Enter();
            
            // To avoid animator mistakes and avoid involuntary movements
            Core.Movement.SetVelocityX(0f);
            StartTime = Time.time;
        }
        
        // 
        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if (IsExitingState) return;
            
            if (XInput != 0)
            {
                PlayerStateMachine.ChangeState(PlayerController.RunState);
            }
            else if (YInput == -1 && controllerCanCrouch)
            {
                PlayerStateMachine.ChangeState(PlayerController.CrouchIdleState);
            }
            else if (Time.time >= StartTime + DataPlayerController.sleepTime)
            {
                PlayerStateMachine.ChangeState(PlayerController.SleepState);
            }
        }
    }
}
