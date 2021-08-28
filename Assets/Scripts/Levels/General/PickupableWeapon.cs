
using Player.Input;
using UnityEngine;

namespace Levels.General
{
    [RequireComponent(typeof(BoxCollider2D))]
    
    public class PickupableWeapon : MonoBehaviour
    {
        private enum WeaponType
        { 
            Fishcalibur
        }
        
        [Header("Key Settings")] [Space(5)]
        [Tooltip("Select the type of key")]
        [SerializeField] private WeaponType weaponType;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.transform.CompareTag("Player")) return;
            
            collision.GetComponent<PlayerInputHandler>().ChangeControllerCanAttack();
            
            switch (weaponType.ToString())
            {
                case "Fishcalibur":
                    Debug.Log($"You Pickup: {gameObject.name}");
                    break;
            }
            Destroy(gameObject);
        }
    }
}
