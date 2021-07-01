using UnityEngine;
using Weapons;

namespace Intermediaries
{
    public class WeaponAnimationToWeapon : MonoBehaviour
    {
        private Weapon _currentWeapon;

        private void Start()
        {
            _currentWeapon = GetComponentInParent<Weapon>();
        }
        
        public virtual void AnimationFinishTrigger()
        {
            _currentWeapon.AnimationFinishTrigger();
        }
        
        private void AnimationStartMovementTrigger()
        {
            _currentWeapon .AnimationStartMovementTrigger();
        }

        private void AnimationStopMovementTrigger()
        {
            _currentWeapon .AnimationStopMovementTrigger();
        }

        private void AnimationTurnOnFlipTrigger()
        {
            _currentWeapon .AnimationTurnOnFlipTrigger();
        }
        
        private void AnimationTurnOffFlipTrigger()
        {
            _currentWeapon .AnimationTurnOffFlipTrigger();
        }

        private void AnimationActionTrigger()
        {
            _currentWeapon.AnimationActionTrigger();
        }
    }
}
