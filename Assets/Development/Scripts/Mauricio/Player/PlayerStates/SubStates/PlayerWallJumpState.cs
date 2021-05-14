using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAbilityState
{
    private int wallJumpDirection;

    public PlayerWallJumpState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.InputHandler.UseJumpInput();
        _player.JumpState.ResetAmountOfJumpsLeft();
        _player.SetVelocity(_playerData.wallJumpVelocity, _playerData.wallJumpAngle, wallJumpDirection);
        _player.CheckIfShouldFlip(wallJumpDirection);
        _player.JumpState.DecreaseAmountOfJumpsLeft();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        _player.playerAnimator.SetFloat("yVelocity", _player.CurrentVelocity.y);
        _player.playerAnimator.SetFloat("xVelocity", Mathf.Abs(_player.CurrentVelocity.x));

        if(Time.time >= _startTime + _playerData.wallJumpTime)
        {
            isAbilityDone = true;
        }
    }

    public void DetermineWallJumpDirection(bool isTouchingWall)
    {
        if (isTouchingWall)
        {
            wallJumpDirection = -_player.FacingDirection;
        }
        else
        {
            wallJumpDirection = _player.FacingDirection;
        }
    }
}
