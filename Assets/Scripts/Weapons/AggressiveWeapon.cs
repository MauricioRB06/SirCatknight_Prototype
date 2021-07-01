using System;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;
using Weapons.Data;
using Weapons.Structs;

namespace Weapons
{
    public class AggressiveWeapon : Weapon
    {
        // 
        protected AggressiveWeaponData AggressiveWeaponData;
        
        // 
        private readonly List<IDamageable> _detectedDamageable = new List<IDamageable>();

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

        public override void AnimationActionTrigger()
        {
            base.AnimationActionTrigger();

            CheckMeleeAttack();
        }
        
        //
        private void CheckMeleeAttack()
        {
            WeaponAttackDetails attackDetails = AggressiveWeaponData.AttackDetails[AttackCounter];
                
            foreach (var item in _detectedDamageable)
            {
                item.Damage(attackDetails.damageAmount);
            }
        }
        
        // 
        public void AddToDetected(Collider2D collision)
        {
            var damageable = collision.GetComponent<IDamageable>();
            if (damageable != null)
            {
                _detectedDamageable.Add(damageable);
            }
        }

        // 
        public void RemoveFromDetected(Collider2D collision)
        {
            var damageable = collision.GetComponent<IDamageable>();
            if (damageable != null)
            {
                _detectedDamageable.Remove(damageable);
            }
        }
    }
}