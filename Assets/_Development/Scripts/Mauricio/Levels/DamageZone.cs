using Player;
using UnityEngine;

namespace _Development.Scripts.Mauricio.Levels
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class DamageZone : MonoBehaviour
    {
        [Header("Damage Settings")] [Space(5)]
        [Tooltip("Damage will be applied consistently on every frame")]
        [Range(0.1f, 0.5f)][SerializeField] private float damageToGive = 0.1f;
        
        private void OnTriggerStay2D(Collider2D collision)
        {
            if(!collision.gameObject.CompareTag("Player")) return;
            
            collision.transform.GetComponent<PlayerController>().Damage(damageToGive);
        }
    }
}
