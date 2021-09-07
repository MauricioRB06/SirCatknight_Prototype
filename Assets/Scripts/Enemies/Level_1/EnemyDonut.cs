
using Interfaces;
using Player;
using UnityEngine;

namespace Enemies.Level_1
{
    public class EnemyDonut : MonoBehaviour
    {
        [RequireComponent(typeof(SpriteRenderer))]
        [RequireComponent(typeof(Animator))]
        [RequireComponent(typeof(BoxCollider2D))]
        public class EnemyBat : MonoBehaviour, IDamageableObject
        {
            [Range(5.0f, 15.0f)] [SerializeField] private float enemyBatHealth = 10;
            [Range(5.0f, 15.0f)] [SerializeField] private float damageToGive = 5;
            [Range(1.0F, 10.0f)] [SerializeField] private float kockbackForce = 5;
            [SerializeField] private GameObject batParticles;
            [Range(1F, 10F)] [SerializeField] private float movementSpeed = 1;
            [Range(0.1F, 5F)] [SerializeField] private float movementWaitingTime = 0.2f;
            [SerializeField] private GameObject[] movementPoints;
            private int _movementPointIterator;
            private float _currentWaitTime;
            private Animator _batAnimator;

            // 
            private void Awake()
            {
                if (batParticles == null)
                {
                    Debug.LogError($"Faltan las particulas del {gameObject.name}, añada unas");
                }
                else if (movementPoints == null)
                {
                    Debug.LogError($"Faltan puntos de movimiento en{gameObject.name}, añada unos");
                }
            }



            // 
            private void Update()
            {
                transform.position = Vector2.MoveTowards(transform.position,
                    movementPoints[_movementPointIterator].transform.position,
                    movementSpeed * Time.deltaTime);

                if (!(Vector2.Distance(transform.position,
                    movementPoints[_movementPointIterator].transform.position) < 0.1f)) return;

                if (_currentWaitTime <= 0)
                {
                    if (movementPoints[_movementPointIterator] != movementPoints[movementPoints.Length - 1])
                    {
                        _movementPointIterator++;
                    }
                    else
                    {
                        _movementPointIterator = 0;
                    }

                    _currentWaitTime = movementWaitingTime;
                }
                else
                {
                    _currentWaitTime -= Time.deltaTime;
                }
            }

            // 
            public void TakeDamage(float damageAmount)
            {
                enemyBatHealth -= damageAmount;

                if (!(enemyBatHealth <= 0)) return;

                Instantiate(batParticles, transform.position, Quaternion.identity);
                Destroy(gameObject, 0.05f);
            }

            // 
            private void OnCollisionEnter2D(Collision2D collision)
            {
                if (!collision.transform.CompareTag("Player")) return;

                collision.transform.GetComponent<PlayerController>().Core.Combat.TakeDamage(damageToGive);
                if (damageToGive <= kockbackForce)
                {
                    collision.transform.GetComponent<Player.PlayerController>()
                        .PlayerAnimator.SetTrigger("LowKnockback");
                    collision.transform.GetComponent<Player.PlayerController>().Core.Combat.KnockBack(
                        new Vector2(1, 1), 10,
                        -collision.transform.GetComponent<Player.PlayerController>().Core.Movement.FacingDirection);
                }
                else
                {
                    collision.transform.GetComponent<Player.PlayerController>()
                        .PlayerAnimator.SetTrigger("HighKnockback");
                    collision.transform.GetComponent<Player.PlayerController>().Core.Combat.KnockBack(
                        new Vector2(1, 2), 15,
                        -collision.transform.GetComponent<Player.PlayerController>().Core.Movement.FacingDirection);
                }
            }
        }
    }
}
