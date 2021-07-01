using Player.Data;
using StateMachine;
using Weapons;

namespace Player.PlayerStates.PlayerAbilityState
{
    public class EntityAttackState : EntityAbilityState
    {
        
        // Reference to Entity Weapon
        private Weapon _currentWeapon;
        
        //
        private int _xInput;
        
        //
        private float _velocityToSet;
        // 
        private bool _setVelocity;
        
        //
        private bool _shouldCheckFlip;
        
        // Class Constructor
        public EntityAttackState(Player entity, global::StateMachine.StateMachine stateMachine, PlayerData entityData,
            string animBoolName) : base(entity, stateMachine, entityData, animBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            //
            _setVelocity = false;
            
            _currentWeapon.EnterWeapon();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            _xInput = Entity.InputHandler.NormInputX;
            
            if (_shouldCheckFlip)
            {
                Core.Movement.CheckIfShouldFlip(_xInput);
            }
            
            if (_setVelocity)
            {
                Core.Movement.SetVelocityX(_velocityToSet * Core.Movement.FacingDirection);
            }
        }

        public override void Exit()
        {
            base.Exit();
            
            _currentWeapon.ExitWeapon();
        }

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

        public void SetPlayerVelocity(float movementAttackSpeed)
        {
            Core.Movement.SetVelocityX((movementAttackSpeed * Core.Movement.FacingDirection));
            _velocityToSet = movementAttackSpeed;
            _setVelocity = true;
        }
        
        public void SetFlipCheck(bool value)
        {
            _shouldCheckFlip = value;
        }
    }
}
