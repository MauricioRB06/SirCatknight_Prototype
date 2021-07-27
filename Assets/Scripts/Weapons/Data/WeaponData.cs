using UnityEngine;

// The purpose of this Script is:
/* Insert Here */
namespace Weapons.Data
{
    public class WeaponData : ScriptableObject
    {
        // We use it to know the number of attacks the weapon has
        public int AmountOfAttacks { get; protected set; }
        
        // We use it to store the cooling time of each attack
        public float[] WeaponCooldownAttack { get; protected set; }
        
        // We use it to store the movement speed that will be applied to the character when attacking
        public float[] AttackMovementSpeed { get; protected set; }
    }
}
