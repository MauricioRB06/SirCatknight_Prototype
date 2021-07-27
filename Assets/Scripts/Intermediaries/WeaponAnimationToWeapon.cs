using UnityEngine;
using Weapons;

// The purpose of this Script is:
/* Communicate the Animator Component of the objects that make up the weapon with the main object of the weapon */


/* Documentation and References:
 * 
 * Sealed modifier: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/sealed
 * 
 */

namespace Intermediaries
{
    public sealed class WeaponAnimationToWeapon : MonoBehaviour
    {
        // We use it as a reference to the weapon with which we want to communicate
        private Weapon _currentWeapon;

        private void Start() { _currentWeapon = GetComponentInParent<Weapon>(); }
        
        // We call the AnimationTriggers inside the Animator component
        public void IntermediaryAnimationFinishTrigger() { _currentWeapon.AnimationFinishTrigger(); }
        
        private void IntermediaryAnimationStartMovementTrigger() { _currentWeapon .AnimationStartMovementTrigger(); }

        private void IntermediaryAnimationStopMovementTrigger() { _currentWeapon .AnimationStopMovementTrigger(); }

        private void IntermediaryAnimationTurnOnFlipTrigger() { _currentWeapon .AnimationTurnOnFlipTrigger(); }
        
        private void IntermediaryAnimationTurnOffFlipTrigger() { _currentWeapon .AnimationTurnOffFlipTrigger(); }

        private void IntermediaryAnimationActionTrigger() { _currentWeapon.AnimationActionTrigger(); }
    }
}
