using System;
using UnityEngine;
using UnityEngine.Serialization;
using Weapons.Structs;

namespace Weapons.Data
{
    [CreateAssetMenu(fileName ="newAggressiveWeaponData", menuName ="Data/Weapon Data/Aggressive Weapon")]
    public class AggressiveWeaponData : WeaponData
    {
        [SerializeField] private WeaponAttackDetails[] attackDetails;
        
        public WeaponAttackDetails[] AttackDetails { get => attackDetails; private set => attackDetails = value; }

        private void OnEnable()
        {
            AmountOfAttacks = attackDetails.Length;
            AttackMovementSpeed = new float[AmountOfAttacks];

            for (var i = 0; i < AmountOfAttacks; i++)
            {
                AttackMovementSpeed[i] = attackDetails[i].movementSpeed;
            }
        }
    }
}
