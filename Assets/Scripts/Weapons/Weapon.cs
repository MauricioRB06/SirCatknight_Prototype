using Player.PlayerStates.PlayerAbilityState;
using UnityEngine;
using Weapons.Data;

namespace Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] protected WeaponData weaponData;
        
        // 
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int AttacksCounter = Animator.StringToHash("AttacksCounter");
        
        // 
        private Animator _baseAnimator;
        private Animator _weaponAnimator;
        
        //
        protected int AttackCounter;
        
        //
        private EntityAttackState _weaponState;
        

        protected virtual void Awake()
        {
            _baseAnimator = transform.Find("Base").GetComponent<Animator>();
            _weaponAnimator = transform.Find("Weapon").GetComponent<Animator>();
            
            gameObject.SetActive(false);
        }
        
        public virtual void EnterWeapon()
        {
            gameObject.SetActive(true);

            if (AttackCounter >= weaponData.AmountOfAttacks)
            {
                AttackCounter = 0;
            }
            
            _baseAnimator.SetBool(Attack, true);
            _weaponAnimator.SetBool(Attack, true);
            
            _baseAnimator.SetInteger(AttacksCounter,AttackCounter);
            _weaponAnimator.SetInteger(AttacksCounter,AttackCounter);
        }

        public void ExitWeapon()
        {
            _baseAnimator.SetBool(Attack, false);
            _weaponAnimator.SetBool(Attack, false);

            AttackCounter++;
            gameObject.SetActive(false);
        }

        public virtual void AnimationFinishTrigger()
        {
            _weaponState.AnimationFinishTrigger();
        }

        public virtual void AnimationStartMovementTrigger()
        {
            _weaponState.SetPlayerVelocity(weaponData.AttackMovementSpeed[AttackCounter]);
        }

        public virtual void AnimationStopMovementTrigger()
        {
            _weaponState.SetPlayerVelocity(0f);
        }

        public virtual void AnimationTurnOnFlipTrigger()
        {
            _weaponState.SetFlipCheck(true);
        }
        
        public virtual void AnimationTurnOffFlipTrigger()
        {
            _weaponState.SetFlipCheck(false);
        }

        public virtual void AnimationActionTrigger()
        {
            
        }
        
        public void InitializeWeapon(EntityAttackState attackState)
        {
            _weaponState = attackState;
        }
    }
}
