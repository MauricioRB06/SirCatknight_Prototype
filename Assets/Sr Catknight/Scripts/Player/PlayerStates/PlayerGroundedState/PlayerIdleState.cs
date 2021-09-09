
using Player.Data;
using StateMachine;
using UnityEngine;

// The purpose of this Script is:
/*  */

namespace Player.PlayerStates.PlayerGroundedState
{
    public class PlayerIdleState : BaseStates.PlayerGroundedState
    {
        // Class constructor
        public PlayerIdleState(PlayerController playerController, PlayerStateMachine playerStateMachine,
            DataPlayerController dataPlayerController, string animationBoolName)
            : base(playerController, playerStateMachine, dataPlayerController, animationBoolName)
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
