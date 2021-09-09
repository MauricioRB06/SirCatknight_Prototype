
using Interfaces;
using UnityEngine;

namespace Enemies.Level_0
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(BoxCollider2D))]
    
    public class CombatDummy : MonoBehaviour, IDamageableObject
    {
        [SerializeField] private GameObject hitParticles;
        [SerializeField] private GameObject breakParticles;
        [SerializeField] private Transform hitParticlesTransform;
        [SerializeField] private Transform breakParticlesTransform;
        [Space(15)] [SerializeField] private GameObject dummyBrokenA;
        [SerializeField] private GameObject dummyBrokenB;
        [SerializeField] private Transform dummyBrokenATransform;
        [SerializeField] private Transform dummyBrokenBTransform;
        
        [Range(10.0f, 50.0f)] [SerializeField] private float dummyHealthAmount = 30.0f;
        
        private Animator _dummyAnimator;
        private BoxCollider2D _dummyCollider;
        
        private static readonly int TakeDamageSend = Animator.StringToHash("TakeDamage");
        private static readonly int DummyBroken = Animator.StringToHash("DummyBroken");
        
        // 
        private void Awake()
        {
            if (hitParticles == null)
            {
                Debug.LogError($"The {gameObject.name} cannot have an empty {hitParticles} component," +
                               " please add one.");
            }
            else if (breakParticles == null)
            {
                Debug.LogError($"The {gameObject.name} cannot have an empty {breakParticles} component," +
                               " please add one.");
            }
            else if (hitParticlesTransform == null)
            {
                Debug.LogError($"The {gameObject.name} cannot have an empty {hitParticlesTransform} component," +
                               " please add one.");
            }
            else if (breakParticlesTransform == null)
            {
                Debug.LogError($"The {gameObject.name} cannot have an empty {breakParticlesTransform} component," +
                               " please add one.");
            }
            else if (dummyBrokenA == null)
            {
                Debug.LogError($"The {gameObject.name} cannot have an empty {dummyBrokenA} component," +
                               " please add one.");
            }
            else if (dummyBrokenB == null)
            {
                Debug.LogError($"The {gameObject.name} cannot have an empty {dummyBrokenB} component," +
                               " please add one.");
            }
            else if (dummyBrokenATransform == null)
            {
                Debug.LogError($"The {gameObject.name} cannot have an empty {dummyBrokenATransform} component," +
                               " please add one.");
            }
            else if (dummyBrokenBTransform == null)
            {
                Debug.LogError($"The {gameObject.name} cannot have an empty {dummyBrokenBTransform} component," +
                               " please add one.");
            }
            
            _dummyAnimator = GetComponent<Animator>();
            _dummyCollider = GetComponent<BoxCollider2D>();
        }

        // 
        public void TakeDamage(float damageAmount)
        {
            dummyHealthAmount -= damageAmount;

            if (dummyHealthAmount <= 0)
            {
                _dummyCollider.enabled = false;
                _dummyAnimator.SetTrigger(DummyBroken);
                Instantiate(breakParticles, breakParticlesTransform.position,
                    Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));

            }
            else
            {
                _dummyCollider.enabled = false;
                _dummyAnimator.SetTrigger(TakeDamageSend);
                Instantiate(hitParticles, hitParticlesTransform.position,
                    Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
            }
        }
        
        // 
        private void AnimationTrigger()
        {
            _dummyCollider.enabled = true;
        }
        
        // 
        private void AnimationFinishTrigger()
        {
            Instantiate(dummyBrokenA, dummyBrokenATransform.position, Quaternion.identity);
            Instantiate(dummyBrokenB, dummyBrokenBTransform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
