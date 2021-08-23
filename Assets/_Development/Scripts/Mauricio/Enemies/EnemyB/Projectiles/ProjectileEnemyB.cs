
using UnityEngine;

namespace _Development.Scripts.Mauricio.Enemies.EnemyB.Projectiles
{
    public class ProjectileEnemyB : MonoBehaviour
    {
        private float speed;
        private Rigidbody2D rb;
        private float travelDistance;
        private float startPosition;
        private bool isGravityOn;
        private bool hasHitGround;
        
        [SerializeField] private float gravity = 8.0f;
        [SerializeField] private float damageRadius = 8.0f;
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private Transform damagePosition;
        
        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0.0f;
            rb.velocity = transform.right * speed;
            
            startPosition = transform.position.x;
            isGravityOn = false;
            hasHitGround = false;
        }
        
        private void FixedUpdate()
        {
            if (!hasHitGround)
            {
                var damageHit = Physics2D.OverlapCircle(damagePosition.position,
                    damageRadius, playerLayer);
                
                var groundHit = Physics2D.OverlapCircle(damagePosition.position,
                    damageRadius, groundLayer);

                if (damageHit)
                {
                    Debug.Log("Player");
                    Destroy(gameObject, 0.5f);
                }
                
                if (groundHit)
                {
                    hasHitGround = true;
                    rb.gravityScale = 0f;
                    rb.velocity = Vector2.zero;
                    Debug.Log("Ground");
                    Destroy(gameObject, 0.5f);
                }
                
                if (Mathf.Abs(startPosition - transform.position.x) >= travelDistance && !isGravityOn)
                {
                    isGravityOn = true;
                    rb.gravityScale = gravity;
                }
            }
        }
        
        private void Update()
        {
            if (!hasHitGround)
            {
                
                if (isGravityOn)
                {
                    var angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
            }
        }

        public void FireProjectile(float speed, float travelDistance, float Damage)
        {
            this.speed = speed;
            this.travelDistance = travelDistance;
            //this.speed = Damage;
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(damagePosition.position,damageRadius);
        }
    }
}
