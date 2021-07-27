using Player.AfterImage;
using Player.Data;
using UnityEngine;

/* Documentation:
 *
 * Time: https://docs.unity3d.com/ScriptReference/Time.html
 * TimeScale: https://docs.unity3d.com/ScriptReference/Time-timeScale.html
 * UnscaledTime: https://docs.unity3d.com/ScriptReference/Time-unscaledTime.html
 * SignedAngle: https://docs.unity3d.com/ScriptReference/Vector2.SignedAngle.html
 * QuaternionEuler: https://docs.unity3d.com/ScriptReference/Quaternion.Euler.html
 * 
 */

namespace Player.PlayerStates.PlayerAbilityState
{ 
    public class PlayerDashState : PlayerAbilityState 
    { 
        // We use it to check if the entity can Dash
        private bool CanDash { get; set; }
        
        // We use it to know the entity is in the DashState
        private bool _isHolding;
        
        // We use it to check if the entity has stopped pressing the skill button
        private bool _dashInputStop;
        
        // We use it to know when the cooling time has passed since the last use
        private float _lastDashTime;

        private Vector2 _dashDirection;
        private Vector2 _dashDirectionInput;
        
        // We use it to save the position of the last image of the AfterImagePool
        private Vector2 _lastAfterImagePosition;
        
        // Class Constructor
        public PlayerDashState(PlayerController playerController, StateMachine.StateMachine stateMachine, PlayerData playerData,
            string animBoolName) : base(playerController, stateMachine, playerData, animBoolName)
        {
        }
        public override void Enter()
        {
            base.Enter();

            CanDash = false;
            PlayerController.InputHandler.UseDashInput();
            
            _isHolding = true;
            _dashDirection = Vector2.right * Core.Movement.FacingDirection;

            Time.timeScale = PlayerData.dashHoldTimeScale;
            StartTime = Time.unscaledTime;

            PlayerController.DashDirectionIndicator.gameObject.SetActive(true);
        }

        public override void Exit()
        {
            base.Exit();
            
            // We only call you when you are jumping in positive Y
            if(Core.Movement.CurrentVelocity.y > 0)
            {
                /* Why this?
                 
                  As we apply so much force so that the movement is fast, if we do not limit the movement,
                  we generate that the entity shoots out a great distance since we are applying a physical
                  force to the rigid body of the entity, with this what we achieve is once we leave the board
                  after the timeout expires or we stop pressing the button, the movement is cut off to cut the
                  distance traveled 
                */
                Core.Movement.SetVelocityY(Core.Movement.CurrentVelocity.y * PlayerData.dashEndYLimiter);
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsExitingState) return;
            
            if (_isHolding)
            {
                _dashDirectionInput = PlayerController.InputHandler.DashDirectionInput;
                _dashInputStop = PlayerController.InputHandler.DashInputStop;

                if(_dashDirectionInput != Vector2.zero)
                {
                    _dashDirection = _dashDirectionInput;
                    _dashDirection.Normalize();
                }
                
                /* We obtain the Angle between the entity and the dash input and assign it to the indicator,
                 to rotate it at the same angle as the input*/
                PlayerController.DashDirectionIndicator.rotation = Quaternion.Euler(0f, 0f,
                    Vector2.SignedAngle(Vector2.right, _dashDirection) - 90f);

                if (!_dashInputStop && !(Time.unscaledTime >= StartTime + PlayerData.dashMaxHoldTime)) return;
                    
                _isHolding = false;
                Time.timeScale = 1f;
                StartTime = Time.time;
                
                Core.Movement.CheckIfShouldFlip(Mathf.RoundToInt(_dashDirection.x));
                Core.Movement.SetVelocity(PlayerData.dashImpulseVelocity, _dashDirection);
                PlayerController.DashDirectionIndicator.gameObject.SetActive(false);
                PlaceAfterImage();
            }
            else
            {
                CheckIfShouldPlaceAfterImage();
                if (!(Time.time >= StartTime + PlayerData.dashLifeTime)) return;
                    
                IsAbilityDone = true;
                _lastDashTime = Time.time;
            }
        }
        
        // We use it to check if the necessary space between images has already been covered, to put another
        private void CheckIfShouldPlaceAfterImage()
        {
            if(Vector2.Distance(PlayerController.transform.position, _lastAfterImagePosition) >= PlayerData.distanceBetweenAfterImages)
            {
                PlaceAfterImage();
            }
        }
        
        // We use it to tell the ImagePool to place an image, and then save the position where it was placed
        private void PlaceAfterImage()
        {
            PlayerAfterImagePool.Instance.GetFromPool();
            _lastAfterImagePosition = PlayerController.transform.position;
        }

        public bool CheckIfCanDash()
        {
            return CanDash && Time.time >= _lastDashTime + PlayerData.dashCooldown;
        }

        public void ResetCanDash() => CanDash = true;
    }
}
