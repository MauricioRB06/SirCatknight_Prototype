using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    protected int xInput;
    protected int yInput;

    protected bool isTouchingCeiling;

    private bool JumpInput;
    private bool grabInput;
    private bool isGrounded;
    private bool isTouchingWall;
    private bool isTouchingLedge;
    private bool dashInput;

    public PlayerGroundedState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = _player.CheckIfGrounded();
        isTouchingWall = _player.CheckIfTouchingWall();
        isTouchingLedge = _player.CheckIfTouchingLedge();
        isTouchingCeiling = _player.CheckForCeiling();
    }

    public override void Enter()
    {
        base.Enter();

        _player.JumpState.ResetAmountOfJumpsLeft();
        _player.DashState.ResetCanDash();
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
        JumpInput = _player.InputHandler.JumpInput;
        grabInput = _player.InputHandler.GrabInput;
        dashInput = _player.InputHandler.DashInput;

        if (JumpInput && _player.JumpState.CanJump())
        {
            _stateMachine.ChangeState(_player.JumpState);
        }else if (!isGrounded)
        {
            _player.InAirState.StartCoyoteTime();
            _stateMachine.ChangeState(_player.InAirState);
        }else if(isTouchingWall && grabInput && isTouchingLedge)
        {
            _stateMachine.ChangeState(_player.WallGrabState);
        }
        else if (dashInput && _player.DashState.CheckIfCanDash() && !isTouchingCeiling)
        {
            _stateMachine.ChangeState(_player.DashState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
