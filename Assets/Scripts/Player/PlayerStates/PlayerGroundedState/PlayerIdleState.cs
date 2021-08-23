using Player.Data;
using UnityEngine;

// The purpose of this Script is:
/*  */

namespace Player.PlayerStates.PlayerGroundedState
{
    public class PlayerIdleState : BaseStates.PlayerGroundedState
    {
        // Class constructor
        public PlayerIdleState(PlayerController playerController, StateMachine.PlayerStateMachine playerStateMachine, DataPlayerController dataPlayerController,
            string animBoolName) : base(playerController, playerStateMachine, dataPlayerController, animBoolName) { }

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
            else if (YInput == -1)
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
