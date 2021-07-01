using UnityEngine.Serialization;

namespace Weapons.Structs
{
    [System.Serializable]
    public struct WeaponAttackDetails
    {
        public string attackName;
        public float movementSpeed;
        public float damageAmount;
    }
}
