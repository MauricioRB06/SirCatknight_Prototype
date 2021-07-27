using Player.Data;
using Weapons;

// The purpose of this script is:
/* Insert Here */

namespace Player.PlayerStates.PlayerAbilityState
{
    public class PlayerAttackState : PlayerAbilityState
    {
        // Reference to weapon.
        private Weapon _currentWeapon;
        
        // We use it to track whether the speed should be set or not.
        private bool _setVelocity;
        
        // We use it to store the speed at which the player will move.
        private float _velocityToSet;
        
        // We use them to check whether a Flip() can be performed during the attack or not.
        private int _xInput;
        private bool _shouldCheckFlip;
        
        // Class Constructor.
        public PlayerAttackState(PlayerController playerController, StateMachine.StateMachine stateMachine, PlayerData playerData,
            string animBoolName) : base(playerController, stateMachine, playerData, animBoolName) { }

        public override void Enter()
        {
            base.Enter(); 
            _setVelocity = false; 
            _currentWeapon.EnterWeapon();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            _xInput = PlayerController.InputHandler.NormInputX;
            
            if (_shouldCheckFlip)
            {
                Core.Movement.CheckIfShouldFlip(_xInput);
            }

            if (!_setVelocity) return;
            
            Core.Movement.SetVelocityX(_velocityToSet * Core.Movement.FacingDirection);
            Core.Movement.SetVelocityY(0);
        }

        public override void Exit()
        {
            base.Exit(); 
            _currentWeapon.ExitWeapon();
        }

        // We use it to let the state know which weapon to call.
        public void SetWeapon(Weapon weapon)
        {
            _currentWeapon = weapon;
            _currentWeapon.InitializeWeapon(this);
        }

        public override void AnimationFinishTrigger()
        {
            base.AnimationFinishTrigger(); 
            IsAbilityDone = true;
        }
        
        // We use it to move the character when an attack moves the character forward.
        public void SetPlayerVelocity(float movementAttackSpeed)
        {
            Core.Movement.SetVelocityX((movementAttackSpeed * Core.Movement.FacingDirection));
            Core.Movement.ReduceGravityScale(PlayerData.decreaseGravityScale);
            Core.Movement.SetVelocityY(0);
            _velocityToSet = movementAttackSpeed;
            _setVelocity = true;
        }
        
        // 
        public void SetFlipCheck(bool value)
        {
            _shouldCheckFlip = value;
        }
    }
}
