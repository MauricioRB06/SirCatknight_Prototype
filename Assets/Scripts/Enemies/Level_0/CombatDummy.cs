using Interfaces;
using UnityEngine;

namespace _Development.Scripts.Mauricio.Enemies
{
    public class CombatDummy : MonoBehaviour, IDamageableObject
    {
        [SerializeField] private GameObject hitParticlesA;
        [SerializeField] private GameObject hitParticlesB;
        private Animator _dummyAnimator;
        private static readonly int TakeDamageSend = Animator.StringToHash("TakeDamage");

        public void TakeDamage (float amount)
        {
            Debug.Log(amount + "Damage Taken");
            Instantiate(hitParticlesA, transform.position, 
                Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
            Instantiate(hitParticlesB, transform.position, 
                Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
            _dummyAnimator.SetTrigger(TakeDamageSend);
        }
        
        private void Awake()
        {
            _dummyAnimator = GetComponent<Animator>();
        }
    }
}