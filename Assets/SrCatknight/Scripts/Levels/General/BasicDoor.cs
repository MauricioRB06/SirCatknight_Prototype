using SrCatknight.Scripts.Interfaces;
using UnityEngine;

namespace SrCatknight.Scripts.Levels.General
{
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(BoxCollider2D))]
    
    public class BasicDoor : MonoBehaviour, IInteractableObject
    {
        // 
        private Animator _basicDoorAnimator;
        private BoxCollider2D _basicDoorCollider;
        private static readonly int OpenDoor = Animator.StringToHash("OpenDoor");
        
        // 
        private void Awake()
        {
            _basicDoorAnimator = GetComponent<Animator>();
            _basicDoorCollider = GetComponent<BoxCollider2D>();
        }
        
        // 
        public void OnInteractable()
        {
            _basicDoorAnimator.SetTrigger(OpenDoor);
        }
        
        // 
        private void AnimationFinishTrigger()
        {
            _basicDoorCollider.enabled = false;
        }
    }
}
