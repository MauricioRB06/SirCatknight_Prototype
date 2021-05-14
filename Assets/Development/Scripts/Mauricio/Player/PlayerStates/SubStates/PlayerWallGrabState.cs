using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallGrabState : PlayerTouchingWallState
{
    private Vector2 holdPosition;

    public PlayerWallGrabState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
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
    }

    public override void Enter()
    {
        base.Enter();

        holdPosition = _player.transform.position;

        HoldPosition();
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
            HoldPosition();

            if (yInput > 0)
            {
                _stateMachine.ChangeState(_player.WallClimbState);
            }
            else if (yInput < 0 || !grabInput)
            {
                _stateMachine.ChangeState(_player.WallSlideState);
            }
        }       
    }

    private void HoldPosition()
    {
        _player.transform.position = holdPosition;

        _player.SetVelocityX(0f);
        _player.SetVelocityY(0f);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
