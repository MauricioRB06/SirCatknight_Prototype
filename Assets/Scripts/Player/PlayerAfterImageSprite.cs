using UnityEngine;

/* Documentation:
 * 
 * OnEnable: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnEnable.html
 * 
 */

namespace Player
{
    public class PlayerAfterImageSprite : MonoBehaviour
    {
        private Transform _player;                      // Reference to player 
        private SpriteRenderer _playerSpriteRenderer;   // Reference to player SpriteRenderer
        private SpriteRenderer _spriteRenderer;         // Reference to AfterImage SpriteRenderer
        
        [SerializeField]
        // Time that the AfterImage lasts
        private float activeTime = 0.1f;
        // We use it to save the time the object was created
        private float _timeActivated;
        // We use it to update the new alpha as it decays
        private float _alphaSprite;
        
        [SerializeField]
        // The value of the Alpha that will be taken when the object is created
        private float alphaSet = 0.8f;
        
        [SerializeField]
        // The value of the Alpha that we use for the decay of the same
        private float alphaDecay = 0.85f;

        private void OnEnable()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            _playerSpriteRenderer = _player.GetComponent<SpriteRenderer>();

            _alphaSprite = alphaSet;
            _spriteRenderer.sprite = _playerSpriteRenderer.sprite;
            
            // Create the AfterImage in the position and rotation that the player had
            var transformAfterImage = transform;
            transformAfterImage.position = _player.position;
            transformAfterImage.rotation = _player.rotation;
            
            _timeActivated = Time.time;
        }

        private void Update()
        {
            _alphaSprite -= alphaDecay * Time.deltaTime;
            _spriteRenderer.color = new Color(1f, 1f, 1f, _alphaSprite);

            if(Time.time >= (_timeActivated + activeTime))
            {
                // After the time is up, we return this GameObject to the queue
                PlayerAfterImagePool.Instance.AddToPool(gameObject);
            }

        }
    }
}
