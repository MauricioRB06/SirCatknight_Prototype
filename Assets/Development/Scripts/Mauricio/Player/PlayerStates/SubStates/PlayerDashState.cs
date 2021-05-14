using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerAbilityState
{
    public bool CanDash { get; private set; }
    private bool isHolding;
    private bool dashInputStop;

    private float lastDashTime;

    private Vector2 dashDirection;
    private Vector2 dashDirectionInput;
    private Vector2 lastAIPos;

    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData, string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();

        CanDash = false;
        _player.InputHandler.UseDashInput();

        isHolding = true;
        dashDirection = Vector2.right * _player.FacingDirection;

        Time.timeScale = _playerData.holdTimeScale;
        _startTime = Time.unscaledTime;

        _player.DashDirectionIndicator.gameObject.SetActive(true);

    }

    public override void Exit()
    {
        base.Exit();

        if(_player.CurrentVelocity.y > 0)
        {
            _player.SetVelocityY(_player.CurrentVelocity.y * _playerData.dashEndYMultiplier);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (!_isExitingState)
        {

            _player.playerAnimator.SetFloat("yVelocity", _player.CurrentVelocity.y);
            _player.playerAnimator.SetFloat("xVelocity", Mathf.Abs(_player.CurrentVelocity.x));


            if (isHolding)
            {
                dashDirectionInput = _player.InputHandler.DashDirectionInput;
                dashInputStop = _player.InputHandler.DashInputStop;

                if(dashDirectionInput != Vector2.zero)
                {
                    dashDirection = dashDirectionInput;
                    dashDirection.Normalize();
                }

                float angle = Vector2.SignedAngle(Vector2.right, dashDirection);
                _player.DashDirectionIndicator.rotation = Quaternion.Euler(0f, 0f, angle - 45f);

                if(dashInputStop || Time.unscaledTime >= _startTime + _playerData.maxHoldTime)
                {
                    isHolding = false;
                    Time.timeScale = 1f;
                    _startTime = Time.time;
                    _player.CheckIfShouldFlip(Mathf.RoundToInt(dashDirection.x));
                    _player.playerRigidBody2D.drag = _playerData.drag;
                    _player.SetVelocity(_playerData.dashVelocity, dashDirection);
                    _player.DashDirectionIndicator.gameObject.SetActive(false);
                    PlaceAfterImage();
                }
            }
            else
            {
                _player.SetVelocity(_playerData.dashVelocity, dashDirection);
                CheckIfShouldPlaceAfterImage();

                if (Time.time >= _startTime + _playerData.dashTime)
                {
                    _player.playerRigidBody2D.drag = 0f;
                    isAbilityDone = true;
                    lastDashTime = Time.time;
                }
            }
        }
    }

    private void CheckIfShouldPlaceAfterImage()
    {
        if(Vector2.Distance(_player.transform.position, lastAIPos) >= _playerData.distBetweenAfterImages)
        {
            PlaceAfterImage();
        }
    }

    private void PlaceAfterImage()
    {
        PlayerAfterImagePool.Instance.GetFromPool();
        lastAIPos = _player.transform.position;
    }

    public bool CheckIfCanDash()
    {
        return CanDash && Time.time >= lastDashTime + _playerData.dashCooldown;
    }

    public void ResetCanDash() => CanDash = true;

}
