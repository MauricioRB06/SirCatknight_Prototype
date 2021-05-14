using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _player.CheckIfShouldFlip(xInput);

        _player.SetVelocityX(_playerData.movementVelocity * xInput);

        if (!_isExitingState)
        {
            if (xInput == 0)
            {
                _stateMachine.ChangeState(_player.IdleState);
            }
            else if (yInput == -1)
            {
                _stateMachine.ChangeState(_player.CrouchMoveState);
            }
        }        
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
