
using Interfaces;
using UnityEngine;

namespace Levels.General
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(BoxCollider2D))]
    
    public class Chest: MonoBehaviour, IDamageableObject
    {
        [SerializeField] private GameObject treasureChest;
        [Range(10.0f, 50.0f)] [SerializeField] private float lifeChest = 10.0f;
        [SerializeField] private Transform treasurePosition;
        
        private Animator _chestAnimator;
        private BoxCollider2D _chestCollider;
        private static readonly int ChestOpen = Animator.StringToHash("ChestOpen");
        private static readonly int ChestDamaged = Animator.StringToHash("ChestDamaged");

        private void Awake()
        {
            _chestAnimator = GetComponent<Animator>();
            _chestCollider = GetComponent<BoxCollider2D>();

            _chestCollider.isTrigger = true;
        }
        
        public void TakeDamage(float damageAmount)
        {
            lifeChest -= damageAmount;
            _chestAnimator.SetTrigger(ChestDamaged);
            
            if (lifeChest <= 0)
            {
                _chestAnimator.SetTrigger(ChestOpen);
            }
        }

        private void AnimationFinishTrigger()
        {
            Instantiate(treasureChest, treasurePosition.position, Quaternion.identity);
        }
    }
}
