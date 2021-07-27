using UnityEngine;

namespace _Development.Scripts.Mauricio.Levels
{
    [RequireComponent(typeof(CircleCollider2D))]
    [RequireComponent(typeof(Animator))]
    public class Projectile : MonoBehaviour
    {
        [Header("Audio Settings")] [Space(5)]
        [Tooltip("Reproduces when the projectile explodes")]
        [SerializeField] private GameObject projectileCrashSfx;
        
        private Animator projectileAnimator;
        private Vector2 projectileLimit;
        private float projectileSpeed;
        private int orientationProjectile;
        private bool _projectileCrash;
        
        
        private static readonly int Crash = Animator.StringToHash("Crash");
        
        private void Awake()
        {
            if (projectileCrashSfx == null)
            {
                Debug.LogError("The Projectile Crash Sfx cannot be empty");
            }
            else
            {
                projectileSpeed = GetComponentInParent<LauncherObject>().ProjectileSpeed;
                orientationProjectile = GetComponentInParent<LauncherObject>().LauncherRotation;
                projectileLimit = GetComponentInParent<LauncherObject>().projectileLimit.transform.position;
                projectileAnimator = GetComponent<Animator>();
            }
        }
        
        private void Update()
        {
            if (_projectileCrash) return;
            
            switch (orientationProjectile)
            {
                case 1:
                {
                    transform.Translate(Vector2.left * (projectileSpeed * Time.deltaTime));
                
                    if (!(Vector2.Distance(transform.position, projectileLimit) < 0.1f)) return;
                    projectileAnimator.SetTrigger(Crash);
                    break;
                }
                case 0:
                {
                    transform.Translate(Vector2.right * (projectileSpeed * Time.deltaTime));
                
                    if (!(Vector2.Distance(transform.position, projectileLimit) < 0.1f)) return;
                    projectileAnimator.SetTrigger(Crash);
                    break;
                }
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            projectileAnimator.SetTrigger(Crash);
            _projectileCrash = true;

            if (collision.gameObject.CompareTag("Player"))
            {
                collision.transform.GetComponent<Player.PlayerController>().Damage(10);
            }
        }
        
        private void AnimationTrigger()
        {
            Instantiate(projectileCrashSfx, transform.position, Quaternion.identity);
            GetComponent<CircleCollider2D>().enabled = false;
        }
        
        public void AnimationFinishTrigger() => Destroy(gameObject);
    }
}
