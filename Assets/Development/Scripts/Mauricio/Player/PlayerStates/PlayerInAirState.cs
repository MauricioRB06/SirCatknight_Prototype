using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInAirState : PlayerState
{
    //Input
    private int xInput;
    private bool jumpInput;
    private bool jumpInputStop;
    private bool grabInput;
    private bool dashInput;

    //Checks
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isTouchingWallBack;
    private bool oldIsTouchingWall;
    private bool oldIsTouchingWallBack;
    private bool isTouchingLedge;

    private bool coyoteTime;
    private bool wallJumpCoyoteTime;
    private bool isJumping;

    private float startWallJumpCoyoteTime;

    public PlayerInAirState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        oldIsTouchingWall = isTouchingWall;
        oldIsTouchingWallBack = isTouchingWallBack;

        isGrounded = _player.CheckIfGrounded();
        isTouchingWall = _player.CheckIfTouchingWall();
        isTouchingWallBack = _player.CheckIfTouchingWallBack();
        isTouchingLedge = _player.CheckIfTouchingLedge();

        if(isTouchingWall && !isTouchingLedge)
        {
            _player.LedgeClimbState.SetDetectedPosition(_player.transform.position);
        }

        if(!wallJumpCoyoteTime && !isTouchingWall && !isTouchingWallBack && (oldIsTouchingWall || oldIsTouchingWallBack))
        {
            StartWallJumpCoyoteTime();
        }
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        oldIsTouchingWall = false;
        oldIsTouchingWallBack = false;
        isTouchingWall = false;
        isTouchingWallBack = false;
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        CheckCoyoteTime();
        CheckWallJumpCoyoteTime();

        xInput = _player.InputHandler.NormInputX;
        jumpInput = _player.InputHandler.JumpInput;
        jumpInputStop = _player.InputHandler.JumpInputStop;
        grabInput = _player.InputHandler.GrabInput;
        dashInput = _player.InputHandler.DashInput;

        CheckJumpMultiplier();

        if (isGrounded && _player.CurrentVelocity.y < 0.01f)
        {            
            _stateMachine.ChangeState(_player.LandState);
        }
        else if(isTouchingWall && !isTouchingLedge && !isGrounded)
        {
            _stateMachine.ChangeState(_player.LedgeClimbState);
        }
        else if(jumpInput && (isTouchingWall || isTouchingWallBack || wallJumpCoyoteTime))
        {
            StopWallJumpCoyoteTime();
            isTouchingWall = _player.CheckIfTouchingWall();
            _player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            _stateMachine.ChangeState(_player.WallJumpState);
        }
        else if(jumpInput && _player.JumpState.CanJump())
        {
            _stateMachine.ChangeState(_player.JumpState);
        }
        else if(isTouchingWall && grabInput && isTouchingLedge)
        {
            _stateMachine.ChangeState(_player.WallGrabState);
        }
        else if(isTouchingWall && xInput == _player.FacingDirection && _player.CurrentVelocity.y <= 0)
        {
            _stateMachine.ChangeState(_player.WallSlideState);
        }
        else if(dashInput && _player.DashState.CheckIfCanDash())
        {
            _stateMachine.ChangeState(_player.DashState);
        }
        else
        {
            _player.CheckIfShouldFlip(xInput);
            _player.SetVelocityX(_playerData.movementVelocity * xInput);

            _player.playerAnimator.SetFloat("yVelocity", _player.CurrentVelocity.y);
            _player.playerAnimator.SetFloat("xVelocity", Mathf.Abs(_player.CurrentVelocity.x));
        }

    }

    private void CheckJumpMultiplier()
    {
        if (isJumping)
        {
            if (jumpInputStop)
            {
                _player.SetVelocityY(_player.CurrentVelocity.y * _playerData.variableJumpHeightMultiplier);
                isJumping = false;
            }
            else if (_player.CurrentVelocity.y <= 0f)
            {
                isJumping = false;
            }

        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    private void CheckCoyoteTime()
    {
        if(coyoteTime && Time.time > _startTime + _playerData.coyoteTime)
        {
            coyoteTime = false;
            _player.JumpState.DecreaseAmountOfJumpsLeft();
        }
    }

    private void CheckWallJumpCoyoteTime()
    {
        if(wallJumpCoyoteTime && Time.time > startWallJumpCoyoteTime + _playerData.coyoteTime)
        {
            wallJumpCoyoteTime = false;
        }
    }

    public void StartCoyoteTime() => coyoteTime = true;

    public void StartWallJumpCoyoteTime()
    {
        wallJumpCoyoteTime = true;
        startWallJumpCoyoteTime = Time.time;
    }

    public void StopWallJumpCoyoteTime() => wallJumpCoyoteTime = false;

    public void SetIsJumping() => isJumping = true;
}
