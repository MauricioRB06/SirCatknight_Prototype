using UnityEngine;

namespace _Development.Scripts.Mauricio.Others
{
    public class SoundController : MonoBehaviour
    {
        [Header("Audio Settings")] [Space]
        [Tooltip("Destruction time, set based on the length of the audio")]
        [SerializeField] private float destroyTime = 0.5f;
        
        private void Start()
        {
            Destroy(gameObject, destroyTime);
        }
    }
}
