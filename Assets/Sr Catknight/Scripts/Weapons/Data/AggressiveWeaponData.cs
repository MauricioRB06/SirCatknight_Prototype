
// The purpose of this Script is:
/* Insert Here */

/* Documentation and References:
 *
 * OnEnable: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnEnable.html
 * 
 */

using UnityEngine;
using Weapons.Structs;

namespace Weapons.Data
{
    [CreateAssetMenu(fileName ="newAggressiveWeaponData",
        menuName ="Sr.Catknight Data/Weapon Data/1. Aggressive Weapon", order = 1)]
    
    public class AggressiveWeaponData : WeaponData
    {
        // We use it to store the AttackDetails of the weapon
        [SerializeField] private WeaponAttackDetails[] attackDetails;
        
        // We use it to give other scripts access to AttackDetails
        public WeaponAttackDetails[] AttackDetails => attackDetails;

        private void OnEnable()
        {
            AmountOfAttacks = attackDetails.Length;
            WeaponCooldownAttack = new float[AmountOfAttacks];
            AttackMovementSpeed = new float[AmountOfAttacks];
            
            for (var i = 0; i < AmountOfAttacks; i++)
            {
                AttackMovementSpeed[i] = attackDetails[i].movementSpeed;
                WeaponCooldownAttack[i] = attackDetails[i].cooldown;
            }
        }
    }
}
