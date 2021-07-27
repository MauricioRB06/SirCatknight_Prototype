using UnityEngine;

namespace _Development.Scripts.Mauricio.Levels
{
    public class PendulumObject : MonoBehaviour
    {
        [Header("Rotation Settings")] [Space(5)]
        [Tooltip("Sets whether the object rotates or not")]
        [SerializeField] private bool isItOscillating = true;
        [Tooltip("Sets the rotation speed")]
        [Range(0.3f, 1.0f)][SerializeField] private float oscillationSpeed = 0.5f;
        [Space(15)]
        
        [Header("Damage Settings")] [Space(5)]
        [Tooltip("Sets the amount of damage it causes when it collides")]
        [Range(5.0f, 50.0f)][SerializeField] private float damageToGive = 10.0f;
        [Space(15)]
        
        [Header("Audio Settings")] [Space(5)]
        [Tooltip("Insert here an AudioPrefab")]
        [SerializeField] private GameObject objectAudio;
        [Tooltip("Determine the pivot point for instantiating an audio in case you have")]
        [SerializeField] private Transform sfxAxis;
        
        private float rotation;
        
        private void Start()
        {
            if (sfxAxis == null)
            {
                Debug.LogError("The Sfx axis cannot be empty");
            }
            else
            {
                if (objectAudio == null) return;
                Instantiate(objectAudio, transform.position, Quaternion.identity, sfxAxis);
            }
        }
        
        private void Update()
        {
            if (!isItOscillating) return;

            transform.rotation = Quaternion.Euler(0.0f,0.0f,rotation);
            rotation += oscillationSpeed;
            
            if (rotation >= 100)
            {
                oscillationSpeed *= -1;
            }
            
            if (rotation <= -100)
            {
                oscillationSpeed *= -1;
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("Player")) return;
            
            collision.transform.GetComponent<Player.PlayerController>().Damage(damageToGive);
        }
    }
}
