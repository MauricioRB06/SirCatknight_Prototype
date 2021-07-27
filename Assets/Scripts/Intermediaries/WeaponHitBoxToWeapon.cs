using UnityEngine;
using Weapons;

// The purpose of this Script is:
/* Pass the information of the objects that have been hit with HitBox, to the main object of the weapon */

/* Documentation and References:
 * 
 * OnTriggerEnter2D: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnTriggerEnter2D.html
 * OnTriggerExit2D: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnTriggerExit2D.html
 * 
 */

namespace Intermediaries
{
    public class WeaponHitBoxToWeapon : MonoBehaviour
    {
        private AggressiveWeapon _currentWeapon;
        private void Awake() { _currentWeapon = GetComponentInParent<AggressiveWeapon>(); }
        private void OnTriggerEnter2D(Collider2D collision) { _currentWeapon.AddToDetected(collision); }
        private void OnTriggerExit2D(Collider2D collision) { _currentWeapon.RemoveFromDetected(collision); }
    }
}