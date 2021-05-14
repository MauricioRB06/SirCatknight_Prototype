using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTouchingWallState : PlayerState
{
    protected bool isGrounded;
    protected bool isTouchingWall;
    protected bool grabInput;
    protected bool jumpInput;
    protected bool isTouchingLedge;
    protected int xInput;
    protected int yInput;

    public PlayerTouchingWallState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = _player.CheckIfGrounded();
        isTouchingWall = _player.CheckIfTouchingWall();
        isTouchingLedge = _player.CheckIfTouchingLedge();

        if(isTouchingWall && !isTouchingLedge)
        {
            _player.LedgeClimbState.SetDetectedPosition(_player.transform.position);
        }
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

        xInput = _player.InputHandler.NormInputX;
        yInput = _player.InputHandler.NormInputY;
        grabInput = _player.InputHandler.GrabInput;
        jumpInput = _player.InputHandler.JumpInput;

        if (jumpInput)
        {            
            _player.WallJumpState.DetermineWallJumpDirection(isTouchingWall);
            _stateMachine.ChangeState(_player.WallJumpState);
        }
        else if (isGrounded && !grabInput)
        {
            _stateMachine.ChangeState(_player.IdleState);
        }
        else if(!isTouchingWall || (xInput != _player.FacingDirection && !grabInput))
        {
            _stateMachine.ChangeState(_player.InAirState);
        }
        else if(isTouchingWall && !isTouchingLedge)
        {
            _stateMachine.ChangeState(_player.LedgeClimbState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
