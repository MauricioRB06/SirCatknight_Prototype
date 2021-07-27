using Player;
using UnityEngine;

namespace _Development.Scripts.Mauricio.Levels
{
    
    [RequireComponent(typeof(CircleCollider2D))]
    public class HealthItem : MonoBehaviour
    {
        [Header("Health Item Settings")][Space(5)]
        [Tooltip("Amount of life the player will gain")]
        [Range(1.0f, 100.0f)][SerializeField] private float healthToGive = 5;
        [Space(15)]
        
        [Header("Audio Settings")][Space(5)]
        [Tooltip("Insert here an AudioPrefab")]
        [SerializeField] private GameObject sfxHealthItemPickUp;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            
            if (sfxHealthItemPickUp == null)
            {
                Debug.LogError("SFX Health Item PickUp can not be empty");
            }
            else
            {
                Instantiate(sfxHealthItemPickUp, transform.position, Quaternion.identity);
                collision.GetComponent<PlayerHealth>().CureHealth(healthToGive);
                Destroy(gameObject);
            }
        }
    }
}
