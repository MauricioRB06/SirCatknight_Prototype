using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchMoveState : PlayerGroundedState
{
    public PlayerCrouchMoveState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
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
            _player.SetVelocityX(_playerData.crouchMovementVelocity * _player.FacingDirection);
            _player.CheckIfShouldFlip(xInput);

            if(xInput == 0)
            {
                _stateMachine.ChangeState(_player.CrouchIdleState);
            }
            else if(yInput != -1 && !isTouchingCeiling)
            {
                _stateMachine.ChangeState(_player.MoveState);
            }
        }

    }
}
