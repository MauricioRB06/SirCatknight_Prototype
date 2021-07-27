using Player.Data;
using UnityEngine;

// The purpose of this Script is:
/*  */

namespace Player.PlayerStates.PlayerGroundedState
{
    public class PlayerIdleState : PlayerGroundedState
    {
        // Class constructor
        public PlayerIdleState(PlayerController playerController, StateMachine.StateMachine stateMachine, PlayerData playerData,
            string animBoolName) : base(playerController, stateMachine, playerData, animBoolName) { }

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
                StateMachine.ChangeState(PlayerController.RunState);
            }
            else if (YInput == -1)
            {
                StateMachine.ChangeState(PlayerController.CrouchIdleState);
            }
            else if (Time.time >= StartTime + PlayerData.sleepTime)
            {
                StateMachine.ChangeState(PlayerController.SleepState);
            }
        }
    }
}
