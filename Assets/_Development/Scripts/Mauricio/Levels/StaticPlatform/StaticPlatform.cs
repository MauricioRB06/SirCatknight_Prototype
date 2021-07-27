using System.Collections;
using UnityEngine;

/* The Purpose Of This Script Is:

    Insert Here

    Documentation and References:

    Vector2.MoveTowards: https://docs.unity3d.com/ScriptReference/Vector2.MoveTowards.html
    Vector2.Distance: https://docs.unity3d.com/ScriptReference/Vector2.Distance.html
    C# Properties: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/properties

 */

namespace _Development.Scripts.Mauricio.Levels.StaticPlatform
{
    [RequireComponent(typeof(BoxCollider2D))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class StaticPlatform : MonoBehaviour
    {
        [Header("Platform Fall Settings")] [Space(5)]
        [Tooltip("Set whether the platform falls or not")]
        [SerializeField] private bool platformFalls = true;
        [Tooltip("Sets the time it takes for the platform to start falling down (in seconds)")]
        [Range(0.5f, 3f)] [SerializeField] private float fallingTime = 1;
        [Space(15)]
        
        [Header("Platform Vibrate Settings")] [Space(5)]
        [Tooltip("Sets whether the platform vibrates before falling")]
        [SerializeField] private bool vibratesWhenFalling = true;
        [Tooltip("Sets the vibration value")]
        [Range(0.04f, 0.1f)] [SerializeField] private float vibrationValue = 0.04f;
        [Space(15)]
        
        [Header("Platform Return Settings")] [Space(5)]
        [Tooltip("Sets whether the platform returns after falling")]
        [SerializeField] private bool platformReturn = true;
        [Tooltip("Sets the time it takes for the platform to return (in seconds)")]
        [Range(3f, 15f)] [SerializeField] private float returnTime = 5;
        [Space(15)]
        
        [Header("Platform Destroy Settings")] [Space(5)]
        [Tooltip("If the platform does not return after falling, set the time it will be destroyed (in seconds)")]
        [SerializeField] private float destroyTime;
        
        private Rigidbody2D _platformRigidBody2D;
        private Animator _platformAnimator;
        private static readonly int Falling = Animator.StringToHash("Falling");
        private static readonly int Fall = Animator.StringToHash("Fall");
        private bool _vibrate;
        private float _vibrateAmount = 0.02f;
        private Vector2 _initialPositionPlatform;
        private SpriteRenderer _platformSpriteRenderer;
        private Color _platformSpriteColor;
        
        private IEnumerator PlatformFadeIn()
        {
            for (var platformAlpha = 0.0f; platformAlpha <= 1.0f; platformAlpha += 0.1f)
            {
                _platformSpriteColor.a = platformAlpha;
                _platformSpriteRenderer.material.color = _platformSpriteColor;
                yield return new WaitForSeconds(0.05f);
            }
            
            _platformSpriteColor.a = 1;
            _platformSpriteRenderer.material.color = _platformSpriteColor;
        }

        private IEnumerator PlatformFall()
        {
            yield return new WaitForSeconds(fallingTime);
            _platformAnimator.SetBool(Falling, false);
            _platformAnimator.SetBool(Fall, true);
            _vibrate = false;
            _platformRigidBody2D.isKinematic = false;
            

            if (!platformReturn) yield break;

            yield return new WaitForSeconds(returnTime);
            _platformRigidBody2D.velocity = Vector2.zero;
            transform.gameObject.SetActive(false);
            transform.gameObject.SetActive(true);
            _platformRigidBody2D.isKinematic = true;
            _platformAnimator.SetBool(Fall, false);
            transform.position = _initialPositionPlatform;
            _platformSpriteColor.a = 0;
            _platformSpriteRenderer.material.color = _platformSpriteColor;
            StartCoroutine(PlatformFadeIn());
        }
        
        private void Start()
        {
            _initialPositionPlatform = transform.position;
            _platformRigidBody2D = GetComponent<Rigidbody2D>();
            _platformAnimator = GetComponent<Animator>();
            _platformSpriteRenderer = GetComponent<SpriteRenderer>();
            _platformSpriteColor = _platformSpriteRenderer.material.color;
        }

        private void FixedUpdate()
        {
            if (!vibratesWhenFalling) return;

            if (!_vibrate) return;
            
            var currentPlatformTransform = transform;
            var currentPosition = currentPlatformTransform.position;
            
            currentPosition = new Vector3(currentPosition.x + _vibrateAmount, currentPosition.y, currentPosition.z);
            currentPlatformTransform.position = currentPosition;

            if (transform.position.x >= _initialPositionPlatform.x + vibrationValue ||
                transform.position.x <= _initialPositionPlatform.x - vibrationValue)
            {
                _vibrateAmount *= -1;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            switch (platformFalls)
            {
                case false:
                    return;
                
                case true when !platformReturn:
                {
                    if(!collision.gameObject.CompareTag("Player")) return;
                    _vibrate = true;
                    _platformAnimator.SetBool(Falling, true);
                    StartCoroutine(PlatformFall());
                    Destroy(gameObject, destroyTime);
                    break;
                }
            }

            if(!collision.gameObject.CompareTag("Player")) return;
            
            _vibrate = true;    
            _platformAnimator.SetBool(Falling, true);
            StartCoroutine(PlatformFall());
        }
    }
}
