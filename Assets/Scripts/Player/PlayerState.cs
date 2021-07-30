using Player.Data;
using UnityEngine;

// The purpose in this script is:
/* To be the basis for generating the different states that the player can take */

/* Documentation and References:
 * 
 * Protected: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/protected
 * Read only: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/readonly
 * Polymorphism: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/virtual
 * Course C# Polymorphism: https://www.youtube.com/watch?v=XzKL94OMDV4&list=PLU8oAlHdN5BmpIQGDSHo5e1r4ZYWQ8m4B&index=46 [ Spanish ]
 * 
 */

namespace Player
{
    public class PlayerState
    {
        protected readonly Core.Core Core;
        protected readonly PlayerController PlayerController;       // Reference to Player 
        protected readonly StateMachine.StateMachine StateMachine;  // Reference to StateMachine
        protected readonly PlayerData PlayerData;                   // Reference to Player data
        
        // We use it to warn that an animation has finished
        protected bool IsAnimationFinished;
        
        // We use it to warn that we are leaving a state
        protected bool IsExitingState;

        // We use it to know how long we have been in a state
        protected float StartTime;

        // We use it to obtain a reference to the name of the animations within the animator
        private readonly string _animationBoolName;

        // Class Constructor
        protected PlayerState(PlayerController playerController, StateMachine.StateMachine stateMachine, PlayerData playerData,
            string animationBoolName)
        {
            PlayerController = playerController;
            StateMachine = stateMachine;
            PlayerData = playerData;
            _animationBoolName = animationBoolName;
            Core = PlayerController.Core;
        }
        
        // This function is called when we enter into a state
        public virtual void Enter()
        {
            DoChecks();
            PlayerController.PlayerAnimator.SetBool(_animationBoolName, true);
            StartTime = Time.time;
            IsAnimationFinished = false;
            IsExitingState = false;
        }
        
        // This function is called when we exit from a state
        public virtual void Exit()
        {
            PlayerController.PlayerAnimator.SetBool(_animationBoolName, false);
            IsExitingState = true;
        }
        
        // This function is called at each frame of the game
        public virtual void LogicUpdate() { }
        
        // This function is called at each fixed update
        public virtual void PhysicsUpdate() { DoChecks(); }

        /* We use it from PhysicsUpdate() and Enter(), in this function we will ask the state we are in to check the
         * floor or the walls for example, this way we don't have to call them in every fixed update if we don't need
         * to perform these checks */
        protected virtual void DoChecks() { }
        
        // We use it to trigger events in the middle of an animation ( We call it inside the animator )
        public virtual void AnimationTrigger() { }
        
        // We use it to trigger events at the end of an animation ( We call it inside the animator )
        public virtual void AnimationFinishTrigger() => IsAnimationFinished = true;
    }
}