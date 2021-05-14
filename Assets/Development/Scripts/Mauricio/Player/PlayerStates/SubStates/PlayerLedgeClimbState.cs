using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeClimbState : PlayerState
{
    private Vector2 detectedPos;
    private Vector2 cornerPos;
    private Vector2 startPos;
    private Vector2 stopPos;

    private bool isHanging;
    private bool isClimbing;
    private bool jumpInput;
    private bool isTouchingCeiling;

    private int xInput;
    private int yInput;

    public PlayerLedgeClimbState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        _player.playerAnimator.SetBool("climbLedge", false);
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        isHanging = true;
    }

    public override void Enter()
    {
        base.Enter();

        _player.SetVelocityZero();
        _player.transform.position = detectedPos;
        cornerPos = _player.DetermineCornerPosition();

        startPos.Set(cornerPos.x - (_player.FacingDirection * _playerData.startOffset.x), cornerPos.y - _playerData.startOffset.y);
        stopPos.Set(cornerPos.x + (_player.FacingDirection * _playerData.stopOffset.x), cornerPos.y + _playerData.stopOffset.y);

        _player.transform.position = startPos;

    }

    public override void Exit()
    {
        base.Exit();

        isHanging = false;

        if (isClimbing)
        {
            _player.transform.position = stopPos;
            isClimbing = false;
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (_isAnimationFinished)
        {
            if (isTouchingCeiling)
            {
                _stateMachine.ChangeState(_player.CrouchIdleState);
            }
            else
            {
                _stateMachine.ChangeState(_player.IdleState);
            }
        }
        else
        {
            xInput = _player.InputHandler.NormInputX;
            yInput = _player.InputHandler.NormInputY;
            jumpInput = _player.InputHandler.JumpInput;

            _player.SetVelocityZero();
            _player.transform.position = startPos;

            if (xInput == _player.FacingDirection && isHanging && !isClimbing)
            {
                CheckForSpace();
                isClimbing = true;
                _player.playerAnimator.SetBool("climbLedge", true);
            }
            else if (yInput == -1 && isHanging && !isClimbing)
            {
                _stateMachine.ChangeState(_player.InAirState);
            }
            else if(jumpInput && !isClimbing)
            {
                _player.WallJumpState.DetermineWallJumpDirection(true);
                _stateMachine.ChangeState(_player.WallJumpState);
            }
        }
      
    }

    public void SetDetectedPosition(Vector2 pos) => detectedPos = pos;

    private void CheckForSpace()
    {
        isTouchingCeiling = Physics2D.Raycast(cornerPos + (Vector2.up * 0.015f) + (Vector2.right * _player.FacingDirection * 0.015f), Vector2.up, _playerData.standColliderHeight, _playerData.whatIsGround);
        _player.playerAnimator.SetBool("isTouchingCeiling", isTouchingCeiling);
    }
    
}
