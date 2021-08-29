
using Interfaces;
using Player.Input;
using UnityEngine;

namespace Levels.General
{
    public class Lever: MonoBehaviour, IInteractableObject
    {
        private Animator _leverAnimator;
        private BoxCollider2D _leverCollider;
        private static readonly int Interact = Animator.StringToHash("Interact");
        [SerializeField] private GameObject objectToInteract;
        [SerializeField] private GameObject leverLight;
        
        private void Awake()
        {
            _leverAnimator = GetComponent<Animator>();
            _leverCollider = GetComponent<BoxCollider2D>();
        }
        
        /*private void OnTriggerStay2D(Collider2D collision)
        {
            if (!collision.transform.CompareTag("Player")) return;

            if (!collision.GetComponent<PlayerInputHandler>().InteractInput) return;
            
            _leverAnimator.SetTrigger(Interact);
            _leverCollider.enabled = false;
            
            var interactable = objectToInteract.GetComponent<IInteractableObject>();
            interactable?.OnInteractable();
            
            leverLight.SetActive(false);
        }*/

        public void OnInteractable()
        {
            _leverAnimator.SetTrigger(Interact);
            _leverCollider.enabled = false;
            
            var interactable = objectToInteract.GetComponent<IInteractableObject>();
            interactable?.OnInteractable();
            
            leverLight.SetActive(false);
        }
    }
}
