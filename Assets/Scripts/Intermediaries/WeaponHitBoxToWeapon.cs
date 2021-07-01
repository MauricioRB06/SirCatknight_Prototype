using UnityEngine;
using Weapons;

namespace Intermediaries
{
    public class WeaponHitBoxToWeapon : MonoBehaviour
    {
        private AggressiveWeapon _currentWeapon;

        private void Awake()
        {
            _currentWeapon = GetComponentInParent<AggressiveWeapon>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _currentWeapon.AddToDetected(collision);
        }
        
        private void OnTriggerExit2D(Collider2D collision)
        {
            _currentWeapon.RemoveFromDetected(collision);
        }
    }
}
