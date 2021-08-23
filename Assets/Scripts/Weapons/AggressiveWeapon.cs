using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using Weapons.Data;
using System.Linq;

// The purpose of this script is:
/* Defining the behavior of AggressiveWeapons */

namespace Weapons
{
    public class AggressiveWeapon : Weapon
    {
        // We use it to store a reference to the WeaponAttackDetails of our weapon
        protected AggressiveWeaponData AggressiveWeaponData;

        // We use it to store the IDamageableObjects detected by the HitBox of the weapon
        private readonly List<IDamageableObject> _damageableObjectsDetected = new List<IDamageableObject>();
        private readonly List<IKnockbackableObject> _knockbackableObjectsDetected = new List<IKnockbackableObject>();

        protected override void Awake()
        {
            base.Awake();

            if (weaponData.GetType() == typeof(AggressiveWeaponData))
            {
                AggressiveWeaponData = (AggressiveWeaponData)weaponData;
            }
            else
            {
                Debug.LogError("Wrong data for the weapon");
            }
        }

        public override void AnimationActionTrigger() { base.AnimationActionTrigger(); CheckMeleeAttack(); }
        
        // We use it to scroll through the list of IDamageableObjects that we reach with the weapon to apply damage
        private void CheckMeleeAttack()
        {
            var attackDetails = AggressiveWeaponData.AttackDetails[AttackCounter];
                
            foreach (var item in _damageableObjectsDetected.ToList())
            {
                item.TakeDamage(attackDetails.damageAmount);
            }
            
            foreach (var item in _knockbackableObjectsDetected.ToList())
            {
                item.KnockBack(attackDetails.knockBackAngle, attackDetails.knockBackStrenght,
                    Core.Movement.FacingDirection);
            }
        }
        
        // We use it to add to the list the IDamageableObjects that are detected
        public void AddToDetected(Collider2D collision)
        {
            var checkDamageableObjectToAdd = collision.GetComponent<IDamageableObject>();
            
            if (checkDamageableObjectToAdd != null)
            {
                _damageableObjectsDetected.Add(checkDamageableObjectToAdd);
            }
            
            var checkKnockbakableObjectToAdd = collision.GetComponent<IKnockbackableObject>();
            
            if (checkKnockbakableObjectToAdd != null)
            {
                _knockbackableObjectsDetected.Add(checkKnockbakableObjectToAdd);
            }
        }

        // We use it to remove the IDamageableObjects from the list after applying the damage function
        public void RemoveFromDetected(Collider2D collision)
        {
            var checkDamageableObjectToRemove = collision.GetComponent<IDamageableObject>();
            
            if (checkDamageableObjectToRemove != null)
            {
                _damageableObjectsDetected.Remove(checkDamageableObjectToRemove);
            }
            
            var checkKnockbakableObjectToRemove = collision.GetComponent<IKnockbackableObject>();
            
            if (checkKnockbakableObjectToRemove != null)
            {
                _knockbackableObjectsDetected.Remove(checkKnockbakableObjectToRemove);
            }
        }
    }
}
