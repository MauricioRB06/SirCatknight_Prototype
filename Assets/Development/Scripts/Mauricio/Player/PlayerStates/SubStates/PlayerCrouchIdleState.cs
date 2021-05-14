using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchIdleState : PlayerGroundedState
{
    public PlayerCrouchIdleState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _player.SetVelocityZero();
        _player.SetColliderHeight(_playerData.crouchColliderHeight);
    }

    public override void Exit()
    {
        base.Exit();
        _player.SetColliderHeight(_playerData.standColliderHeight);
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!_isExitingState)
        {
            if(xInput != 0)
            {
                _stateMachine.ChangeState(_player.CrouchMoveState);
            }
            else if(yInput != -1 && !isTouchingCeiling)
            {
                _stateMachine.ChangeState(_player.IdleState);
            }
        }
    }
}
