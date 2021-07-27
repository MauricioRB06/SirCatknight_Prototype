using UnityEngine;

namespace _Development.Scripts.Mauricio.Levels.Trampoline
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Trampoline : MonoBehaviour
    {
        [Header("Trampoline Settings")] [Space(5)]
        [Tooltip("Force of impulse to be applied to the player")]
        [Range(5.0f, 30.0f)][SerializeField] private float trampolineForce = 5;
        [Space(15)]
        
        [Header("Audio Settings")] [Space(5)]
        [Tooltip("Insert here an AudioPrefab")]
        [SerializeField] private GameObject sfxTrampoline;
            
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.transform.CompareTag("Player")) return;
            
            if (sfxTrampoline == null)
            {
                Debug.LogError("SFX Trampoline can not be empty");
            }
            else
            {
                Instantiate(sfxTrampoline, transform.position, Quaternion.identity);
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.up * trampolineForce;
            }
        }
    }
}
