using Enemies;
using Player;
using SrCatknight.Scripts.Player;
using UnityEngine;

namespace SrCatknight.Scripts.Levels.General
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(PolygonCollider2D))]
    public class Stalactite: MonoBehaviour
    {
        [SerializeField] private bool damageEntitys;
        [Space(15)]
        
        [Range(1.0f, 10.0f)][SerializeField] private float damageToGive = 3.0f;
        [Range(1.0F, 10.0f)][SerializeField] private float knockbackForce = 5;
        [Space(15)]
        
        [SerializeField] private GameObject crashParticles;
        
        
        // Navigates through the movementPoints to indicate to the object to which it should move.
        private int _movementPointIterator;
        private static readonly int LowKnockback = Animator.StringToHash("LowKnockback");
        private static readonly int HighKnockback = Animator.StringToHash("HighKnockback");
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.CompareTag("Player"))
            {
                Instantiate(crashParticles, transform.position + new Vector3(0,1,0), Quaternion.identity);
                collision.transform.GetComponent<PlayerController>().Core.Combat.TakeDamage(damageToGive);
                collision.transform.GetComponent<PlayerController>().PlayerHealth.TakeDamage(damageToGive);
                
                if (damageToGive <= knockbackForce)
                {
                    collision.transform.GetComponent<PlayerController>()
                        .PlayerAnimator.SetTrigger(LowKnockback);
                    collision.transform.GetComponent<PlayerController>().Core.Combat.KnockBack(
                        new Vector2(1,1),10,
                        -collision.transform.GetComponent<PlayerController>().Core.Movement.FacingDirection);
                }
                else
                {
                    collision.transform.GetComponent<PlayerController>()
                        .PlayerAnimator.SetTrigger(HighKnockback);
                    collision.transform.GetComponent<PlayerController>().Core.Combat.KnockBack(
                        new Vector2(1,2),15,
                        -collision.transform.GetComponent<PlayerController>().Core.Movement.FacingDirection);
                }
                
                Destroy(gameObject);
            }
            else if (collision.transform.CompareTag("Ground"))
            {
                Instantiate(crashParticles, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            else if (collision.transform.CompareTag("Enemy") && damageEntitys)
            {
                Instantiate(crashParticles, transform.position, Quaternion.identity);
                collision.transform.GetComponent<EntityController>().Core.Combat.TakeDamage(damageToGive);
                Destroy(gameObject);
            }
        }
    }
}
