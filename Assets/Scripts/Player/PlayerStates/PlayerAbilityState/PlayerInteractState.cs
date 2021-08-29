
using Interfaces;
using Player.Data;
using StateMachine;
using UnityEngine;

namespace Player.PlayerStates.PlayerAbilityState
{
    public class PlayerInteractState : BaseStates.PlayerAbilityState
    {
        // Class constructor
        public PlayerInteractState(PlayerController playerController, PlayerStateMachine playerStateMachine,
            DataPlayerController dataPlayerController, string animationBoolName)
            : base(playerController, playerStateMachine, dataPlayerController, animationBoolName)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Interact();
            PlayerController.InputHandler.UseInteractInput();
            IsAbilityDone = true;
            Core.Movement.SetVelocityZero();
        }

        private void Interact()
        {
            base.AnimationTrigger();
            
            var detectedObjects = Physics2D.OverlapCircleAll(
                PlayerController.InteractPosition.position,
                DataPlayerController.interactionRadius, DataPlayerController.interactableLayer);
            
            // Scroll through all the objects detected in the collider of the interact.
            foreach (var collider in detectedObjects)
            {
                // If it detects within the object a component that is interactable.
                var interactable = collider.GetComponent<IInteractableObject>();
                interactable?.OnInteractable();
            }
        }
    }
}
