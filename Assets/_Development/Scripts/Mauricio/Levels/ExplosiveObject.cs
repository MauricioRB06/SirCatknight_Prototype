using System.Collections;
using UnityEngine;

namespace _Development.Scripts.Mauricio.Levels
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class ExplosiveObject : MonoBehaviour
    {
        private Animator boomAnimator;
        
        [Header("Explosion Settings")] [Space(5)]
        [Tooltip("The time it takes for the bomb to explode after activation")]
        [Range(2.0f, 6.0f)][SerializeField] private float timeToExplosion = 5.0f;
        [Tooltip("The area affected by the explosion")]
        [SerializeField] private GameObject explosionZone;
        [Space(15)]
        
        [Header("VFX Settings")] [Space(5)]
        [Tooltip("Insert here an ParticlesPrefab")]
        [SerializeField] private GameObject vfxExplosion;
        [Space(15)]
        
        [Header("Audio Settings")] [Space(5)]
        [Tooltip("Insert here an AudioPrefab")]
        [SerializeField] private GameObject sfxBombActivated;
        [Tooltip("Insert here an AudioPrefab")]
        [SerializeField] private GameObject sfxExplosion;

        private static readonly int BombActivated = Animator.StringToHash("BombActivated");

        private IEnumerator StartExplosion()
        {
            var objectTransform = transform;
            var objectPosition = objectTransform.position;
            yield return new WaitForSeconds(timeToExplosion);
            
            Instantiate(sfxExplosion, objectPosition, Quaternion.identity);
            Instantiate(explosionZone, objectPosition, Quaternion.identity);
            Instantiate(vfxExplosion, objectPosition, Quaternion.identity);
            Destroy(gameObject);
        }
        
        private void Start()
        {
            boomAnimator = GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("Player")) return;
            
            if (sfxExplosion == null)
            {
                Debug.LogError("The SFX Explosion cannot be empty, please place one");
            }
            else if (sfxBombActivated == null)
            {
                Debug.LogError("The SFX Bomb Activated cannot be empty, please place one");
            }
            else if (vfxExplosion == null)
            {
                Debug.LogError("The VFX Explosion cannot be empty, please place one");
            }
            else
            {
                var bombTransform = transform;
                boomAnimator.SetTrigger(BombActivated);
                Instantiate(sfxBombActivated, bombTransform.position, Quaternion.identity, bombTransform);
                StartCoroutine(StartExplosion());
            }
        }
    }
}
