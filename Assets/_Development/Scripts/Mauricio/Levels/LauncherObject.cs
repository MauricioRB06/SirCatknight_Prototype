using UnityEngine;

namespace _Development.Scripts.Mauricio.Levels
{
    public class LauncherObject: MonoBehaviour
    {
        [Header("Projectile Settings")] [Space(5)]
        [Tooltip("Waiting time between each attack (in seconds)")]
        [Range(2.0f, 8.0f)][SerializeField] private float launchAttackTime = 3.0f;
        [Tooltip("Projectile launch velocity")]
        [Range(2.0f, 10.0f)][SerializeField] private float projectileSpeed = 2.0f;
        [Space(15)]
        
        [Header("Launcher Settings")] [Space(5)]
        [Tooltip("Sets the direction of the projectile, based on the rotation of the launcher")]
        public Transform launcherRotationAxis;
        [Tooltip("Projectile launching point")]
        public Transform projectileSpawnPoint;
        [Tooltip("Projectile to be fired (Projectile Prefab)")]
        public GameObject projectile;
        [Tooltip("Projectile travel limit")]
        public GameObject projectileLimit;
        [Space(15)]
        
        [Header("Audio Settings")] [Space(5)]
        [Tooltip("Insert Here An AudioPrefab")]
        public GameObject launcherSfx;
        
        private float waitedTime;
        private Animator launcherAnimator;
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Reload = Animator.StringToHash("Reload");
        
        public int LauncherRotation { get; private set; }
        public float ProjectileSpeed => projectileSpeed;
        
        private void Awake()
        {
            if (launcherSfx == null)
            {
                Debug.LogError("The Launcher Sfx cannot be empty");
            }
            else
            {
                LauncherRotation = (int)launcherRotationAxis.transform.rotation.y;
                launcherAnimator = GetComponent<Animator>();
            }
        }
        
        private void Start()
        {
            waitedTime = launchAttackTime;
        }
        
        private void Update()
        {
            if (waitedTime <= 0)
            {
                waitedTime = launchAttackTime;
                launcherAnimator.SetTrigger(Attack);
            }
            else
            {
                waitedTime -= Time.deltaTime;
            }
        }

        private void AnimationTrigger()
        {
            Instantiate(launcherSfx, transform.position, Quaternion.identity);
            Instantiate(projectile, projectileSpawnPoint.position, Quaternion.identity, transform);
        }

        public void AnimationFinishTrigger() => launcherAnimator.SetTrigger(Reload);
    }
}
