using Player;
using UnityEngine;

namespace _Development.Scripts.Mauricio.Levels
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class Shield: MonoBehaviour
    {
        [Header("Shield Settings")][Space(5)]
        [Tooltip("Amount of shielding the player will earn")]
        [Range(1.0f, 3.0f)][SerializeField] private int shieldAmountToGive = 1;
        [Space(15)]
        
        [Header("Audio Settings")][Space(5)]
        [Tooltip("Insert here an AudioPrefab")]
        [SerializeField] private GameObject sfxShieldPickUp;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            
            if (sfxShieldPickUp == null)
            {
                Debug.LogError("SFX Shield PickUp can not be empty");
            }
            else
            {
                Instantiate(sfxShieldPickUp, transform.position, Quaternion.identity);
                collision.GetComponent<PlayerHealth>().TakeShield(shieldAmountToGive);
                Destroy(gameObject);
            }
        }
    }
}
