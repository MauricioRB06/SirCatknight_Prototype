
using Player;
using UnityEngine;

namespace Levels.General
{
    public class FireLauncher : MonoBehaviour
    {
        [SerializeField] private float damageToGive = 5.0f;
        [SerializeField] private float attackSpeed = 2;
        [Range(1.0F, 10.0f)][SerializeField] private float knockbackForce = 5;

        private float _lastAttack;
        private BoxCollider2D _damageZone;
        private Animator _animatorFireLauncher;
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int LowKnockback = Animator.StringToHash("LowKnockback");
        private static readonly int HighKnockback = Animator.StringToHash("HighKnockback");


        private void Awake()
        {
            _damageZone = GetComponent<BoxCollider2D>();
            _animatorFireLauncher = GetComponent<Animator>();
            _damageZone.enabled = false;
            
            _lastAttack = Time.time;
        }

        private void Update()
        {
            if (!(Time.time > attackSpeed + _lastAttack)) return;
            
            _lastAttack = Time.time;
            _animatorFireLauncher.SetTrigger(Attack);
        }
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.transform.CompareTag("Player")) return;
            
            collision.GetComponent<PlayerController>().Core.Combat.TakeDamage(damageToGive);
            
            if (damageToGive <= knockbackForce)
            {
                collision.transform.GetComponent<Player.PlayerController>()
                    .PlayerAnimator.SetTrigger(LowKnockback);
                collision.transform.GetComponent<Player.PlayerController>().Core.Combat.KnockBack(
                    new Vector2(1,1),10,
                    -collision.transform.GetComponent<Player.PlayerController>().Core.Movement.FacingDirection);
            }
            else
            {
                collision.transform.GetComponent<Player.PlayerController>()
                    .PlayerAnimator.SetTrigger(HighKnockback);
                collision.transform.GetComponent<Player.PlayerController>().Core.Combat.KnockBack(
                    new Vector2(1,2),15,
                    -collision.transform.GetComponent<Player.PlayerController>().Core.Movement.FacingDirection);
            }
        }
        
        private void AnimationTrigger()
        {
            _damageZone.enabled = true;
        }
        
        private void AnimationFinishTrigger()
        {
            _damageZone.enabled = false;
        }
    }
}
