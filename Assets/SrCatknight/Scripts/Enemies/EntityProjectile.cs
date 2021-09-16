using UnityEngine;

namespace SrCatknight.Scripts.Enemies
{
    public class EntityProjectile : MonoBehaviour
    {
        //private AttackDetails attackDetails;

        private float _speed;
        private float _travelDistance;
        private float _xStartPos;

        [SerializeField]
        private float gravity;
        [SerializeField]
        private float damageRadius;

        private Rigidbody2D _rigidbody2D;

        private bool _isGravityOn;
        private bool _hasHitGround;

        [SerializeField]
        private LayerMask whatIsGround;
        [SerializeField]
        private LayerMask whatIsPlayer;
        [SerializeField]
        private Transform damagePosition;

        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();

            _rigidbody2D.gravityScale = 0.0f;
            var transform1 = transform;
            _rigidbody2D.velocity = transform1.right * _speed;

            _isGravityOn = false;

            _xStartPos = transform1.position.x;
        }

        private void Update()
        {
            if (!_hasHitGround)
            {
                //attackDetails.position = transform.position;

                if (_isGravityOn)
                {
                    var velocity = _rigidbody2D.velocity;
                    float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
            }
        }

        private void FixedUpdate()
        {
            if (!_hasHitGround)
            {
                var position = damagePosition.position;
                Collider2D damageHit = Physics2D.OverlapCircle(position, damageRadius, whatIsPlayer);
                Collider2D groundHit = Physics2D.OverlapCircle(position, damageRadius, whatIsGround);

                if (damageHit)
                {
                    //damageHit.transform.SendMessage("Damage", attackDetails);
                    Destroy(gameObject);
                }

                if (groundHit)
                {
                    _hasHitGround = true;
                    _rigidbody2D.gravityScale = 0f;
                    _rigidbody2D.velocity = Vector2.zero;
                }


                if (Mathf.Abs(_xStartPos - transform.position.x) >= _travelDistance && !_isGravityOn)
                {
                    _isGravityOn = true;
                    _rigidbody2D.gravityScale = gravity;
                }
            }        
        }

        public void FireProjectile(float speed, float travelDistance, float damage)
        {
            this._speed = speed;
            this._travelDistance = travelDistance;
            //attackDetails.damageAmount = damage;
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(damagePosition.position, damageRadius);
        }
    }
}