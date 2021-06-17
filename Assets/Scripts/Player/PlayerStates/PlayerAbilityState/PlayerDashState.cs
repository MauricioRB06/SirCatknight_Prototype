using Player.Data;
using Player.StateMachine;
using UnityEngine;

namespace Player.PlayerStates.PlayerAbilityState
{ 
    public class PlayerDashState : PlayerAbilityState 
    { 
    private bool CanDash { get; set; }
    private bool _isHolding;
    private bool _dashInputStop;

    private float _lastDashTime;

    private Vector2 _dashDirection;
    private Vector2 _dashDirectionInput;
    private Vector2 _lastAIPos;
    
    private static readonly int YVelocity = Animator.StringToHash("yVelocity");
    private static readonly int XVelocity = Animator.StringToHash("xVelocity");

    // Class Constructor
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, PlayerData playerData,
        string animBoolName) : base(player, stateMachine, playerData, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();

        CanDash = false;
        Player.InputHandler.UseDashInput();

        _isHolding = true;
        _dashDirection = Vector2.right * Player.FacingDirection;

        Time.timeScale = PlayerData.holdTimeScale;
        StartTime = Time.unscaledTime;

        Player.DashDirectionIndicator.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();

        if(Player.CurrentVelocity.y > 0)
        {
            Player.SetVelocityY(Player.CurrentVelocity.y * PlayerData.dashEndYMultiplier);
        }
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if (IsExitingState) return;
        
        Player.PlayerAnimator.SetFloat(YVelocity, Player.CurrentVelocity.y);
        Player.PlayerAnimator.SetFloat(XVelocity, Mathf.Abs(Player.CurrentVelocity.x));


        if (_isHolding)
        {
            _dashDirectionInput = Player.InputHandler.DashDirectionInput;
            _dashInputStop = Player.InputHandler.DashInputStop;

            if(_dashDirectionInput != Vector2.zero)
            {
                _dashDirection = _dashDirectionInput;
                _dashDirection.Normalize();
            }

            var angle = Vector2.SignedAngle(Vector2.right, _dashDirection);
            Player.DashDirectionIndicator.rotation = Quaternion.Euler(0f, 0f, angle - 45f);

            if (!_dashInputStop && !(Time.unscaledTime >= StartTime + PlayerData.maxHoldTime)) return;
                
            _isHolding = false;
            Time.timeScale = 1f;
            StartTime = Time.time;
            Player.CheckIfShouldFlip(Mathf.RoundToInt(_dashDirection.x));
            Player.PlayerRigidBody2D.drag = PlayerData.drag;
            Player.SetVelocity(PlayerData.dashVelocity, _dashDirection);
            Player.DashDirectionIndicator.gameObject.SetActive(false);
            PlaceAfterImage();
        }
        else
        {
            Player.SetVelocity(PlayerData.dashVelocity, _dashDirection);
            CheckIfShouldPlaceAfterImage();

            if (!(Time.time >= StartTime + PlayerData.dashTime)) return;
                
            Player.PlayerRigidBody2D.drag = 0f;
            IsAbilityDone = true;
            _lastDashTime = Time.time;
        }
    }

    private void CheckIfShouldPlaceAfterImage()
    {
        if(Vector2.Distance(Player.transform.position, _lastAIPos) >= PlayerData.distBetweenAfterImages)
        {
            PlaceAfterImage();
        }
    }

    private void PlaceAfterImage()
    {
        PlayerAfterImagePool.Instance.GetFromPool();
        _lastAIPos = Player.transform.position;
    }

    public bool CheckIfCanDash()
    {
        return CanDash && Time.time >= _lastDashTime + PlayerData.dashCooldown;
    }

    public void ResetCanDash() => CanDash = true;
    }
}
