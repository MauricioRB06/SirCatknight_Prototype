using Interfaces;
using UnityEngine;

namespace _Development.Scripts.Mauricio.Enemies
{
    public class CombatDummy : MonoBehaviour, IDamageableObject
    {
        [SerializeField] private GameObject hitParticles;
        private Animator _dummyAnimator;
        private static readonly int TakeDamage = Animator.StringToHash("TakeDamage");

        public void Damage (float amount)
        {
            Debug.Log(amount + "Damage Taken");
            Instantiate(hitParticles, transform.position, 
                Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)));
            _dummyAnimator.SetTrigger(TakeDamage);
        }
        
        private void Awake()
        {
            _dummyAnimator = GetComponent<Animator>();
        }
    }
}