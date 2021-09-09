
using Player;
using Player.Input;
using UnityEngine;

namespace Levels.General
{
    [RequireComponent(typeof(BoxCollider2D))]
    
    public class PickupableWeapon : MonoBehaviour
    {
        [SerializeField] private RuntimeAnimatorController weaponPlayerAnimator;
        private enum WeaponType
        { 
            Fishcalibur
        }
        
        [Header("Key Settings")] [Space(5)]
        [Tooltip("Select the type of key")]
        [SerializeField] private WeaponType weaponType;

        [SerializeField] private GameObject swordUI;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.transform.CompareTag("Player")) return;
            
            switch (weaponType.ToString())
            {
                case "Fishcalibur":
                    
                    collision.GetComponent<PlayerInputHandler>().ChangeControllerCanAttack();
                    collision.GetComponent<PlayerController>().ChangeAnimatorWeapon(weaponPlayerAnimator);
                    break;
            }
            
            swordUI.SetActive(true);
            Destroy(gameObject);
        }
    }
}
