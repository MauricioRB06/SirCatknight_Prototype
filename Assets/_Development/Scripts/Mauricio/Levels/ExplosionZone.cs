using UnityEngine;

namespace _Development.Scripts.Mauricio.Levels
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class ExplosionZone: MonoBehaviour
    {
        [Header("Explosion Zone Settings")] [Space(5)]
        [Tooltip("Damage that applies in the blast zone")]
        [Range(5.0f, 50.0f)][SerializeField] private float damageToGive = 20.0f;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            Destroy(gameObject, 0.5f);
            
            if (!collision.gameObject.CompareTag("Player")) return;
            
            collision.transform.GetComponent<Player.PlayerController>().Damage(damageToGive);
        }
    }
}
