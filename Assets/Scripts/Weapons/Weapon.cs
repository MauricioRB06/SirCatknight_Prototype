using Player.PlayerStates.PlayerAbilityState;
using UnityEngine;
using Weapons.Data;

// The purpose of this Script is:
// Define how our AttackState will interact with the weapon we have equipped.

namespace Weapons
{
    public class Weapon : MonoBehaviour
    {
        // We use it to store the weapon data.
        [SerializeField] protected WeaponData weaponData;
        
        // Generate id parameters for the animator.
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int AttacksCounter = Animator.StringToHash("AttacksCounter");
        
        // We use them to refer to the animators of the weapon.
        private Animator _characterAnimator;
        private Animator _weaponAnimator;
        
        // We use it to find out what attack we are in.
        protected int AttackCounter;
        
        // We use it to know when to restart the attack counter if we stop attacking.
        private float _attackCooldown;
        
        // We use it as a reference to the player's AttackState.
        private PlayerAttackState _weaponAttackState;
        
        protected virtual void Awake()
        {
            _characterAnimator = transform.Find("Character").GetComponent<Animator>();
            _weaponAnimator = transform.Find("Weapon").GetComponent<Animator>();

            gameObject.SetActive(false);
        }
        
        // We use it when an attack is initiated, to communicate to the weapon that is being used.
        public virtual void EnterWeapon()
        {
            gameObject.SetActive(true);
            
            if (AttackCounter >= weaponData.AmountOfAttacks  || (Time.time - _attackCooldown) >= weaponData.WeaponCooldownAttack[AttackCounter])
            {
                _attackCooldown = 0.0f;
                AttackCounter = 0;
            }

            _characterAnimator.SetBool(Attack, true);
            _weaponAnimator.SetBool(Attack, true);
            
            _characterAnimator.SetInteger(AttacksCounter,AttackCounter);
            _weaponAnimator.SetInteger(AttacksCounter,AttackCounter);
        }
        
        // We use it when an attack is finished, to communicate to the weapon that it is no longer being used.
        public void ExitWeapon()
        {
            _characterAnimator.SetBool(Attack, false);
            _weaponAnimator.SetBool(Attack, false);

            AttackCounter++;
            _attackCooldown = Time.time;
            gameObject.SetActive(false);
        }

        // We use it to let the weapon know which AttackState is the one that has called it.
        public void InitializeWeapon(PlayerAttackState attackState) { _weaponAttackState = attackState; }

        // We call the AnimationTriggers inside the Intermediary component.
        public virtual void AnimationStartMovementTrigger() { _weaponAttackState.SetPlayerVelocity(weaponData.AttackMovementSpeed[AttackCounter]); }

        public virtual void AnimationStopMovementTrigger() { _weaponAttackState.SetPlayerVelocity(0f); }
        
        public virtual void AnimationTurnOnFlipTrigger() { _weaponAttackState.SetFlipCheck(true); }
        
        public virtual void AnimationTurnOffFlipTrigger() { _weaponAttackState.SetFlipCheck(false); }
        
        public virtual void AnimationFinishTrigger() { _weaponAttackState.AnimationFinishTrigger(); }
        
        // We use it to store the logic that checks if we have hit an IDamageableObject.
        public virtual void AnimationActionTrigger() { }
    }
}
