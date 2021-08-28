
using Player.AfterImage;
using Player.Data;
using StateMachine;
using UnityEngine;

namespace Player.PlayerStates.PlayerAbilityState
{
    public class PlayerDodgeRoll : BaseStates.PlayerAbilityState
    {
        private bool CanDodgeRoll { get; set; }
        private float _lastDodgeRollTime;
        
        // We use it to save the position of the last image of the AfterImagePool
        private Vector2 _lastAfterImagePosition;
        
        // Class Constructor
        public PlayerDodgeRoll(PlayerController playerController, PlayerStateMachine playerStateMachine,
            DataPlayerController dataPlayerController, string animationBoolName)
            : base(playerController, playerStateMachine, dataPlayerController, animationBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Debug.Log("Dodge");
            CanDodgeRoll = false;
            PlayerController.InputHandler.UseDodgeRollInput();
        }
        
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            if (IsExitingState) return;
            
            Core.Movement.SetVelocity(DataPlayerController.dodgeRollImpulse, new Vector2(1,0), XInput);
            PlaceAfterImage();
            CheckIfShouldPlaceAfterImage();
            
            if (!(Time.time >= StartTime + DataPlayerController.dodgeRollLifeTime)) return;
            
            IsAbilityDone = true;
            _lastDodgeRollTime = Time.time;
        }
        
        private void CheckIfShouldPlaceAfterImage()
        {
            if(Vector2.Distance(PlayerController.transform.position, _lastAfterImagePosition)
               >= DataPlayerController.distanceBetweenAfterImages)
            {
                PlaceAfterImage();
            }
        }
        
        private void PlaceAfterImage()
        {
            PlayerAfterImagePool.Instance.GetFromPool();
            _lastAfterImagePosition = PlayerController.transform.position;
        }
        
        public bool CheckIfCanDodgeRoll()
        {
            return CanDodgeRoll && Time.time >= _lastDodgeRollTime + DataPlayerController.dodgeRollCooldown;
        }
        
        public void ResetCanDodgeRoll() => CanDodgeRoll = true;
    }
}
