using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
        _player.SetVelocityX(0f);   // Para evitar errores del animador y evitar movimientos involuntarios
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!_isExitingState)
        {
            if (xInput != 0)
            {
                _stateMachine.ChangeState(_player.MoveState);
            }
            else if (yInput == -1)
            {
                _stateMachine.ChangeState(_player.CrouchIdleState);
            }
        }       
        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
