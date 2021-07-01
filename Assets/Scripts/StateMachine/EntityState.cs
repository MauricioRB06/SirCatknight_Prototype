using Player.Data;
using UnityEngine;

/* Documentation:
 * 
 * Protected: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/protected
 * Virtual & Override: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/virtual
 * Read only: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/readonly
 * 
 */

namespace StateMachine
{
    public class EntityState
    {
        protected readonly Core.Core Core;
        
        protected readonly Player.Player Entity;            // Reference to Entity 
        protected readonly StateMachine StateMachine;       // Reference to StateMachine
        protected readonly PlayerData EntityData;           // Reference to Entity data
        
        // We use it when we want to mark an animation as finished
        protected bool IsAnimationFinished;
        
        // We use it to prevent that if we are leaving a State, actions of the State from which we are leaving can be executed
        protected bool IsExitingState;

        // We use it to have a reference of how long we have been in a specific State
        protected float StartTime;

        // Refers to variables within the animator
        private readonly string _animationBoolName;

        // Class Constructor
        protected EntityState(Player.Player entity, StateMachine stateMachine, PlayerData entityData,
            string animationBoolName)
        {
            Entity = entity;
            StateMachine = stateMachine;
            EntityData = entityData;
            _animationBoolName = animationBoolName;
            Core = Entity.Core;
        }
        
        // This Function is called when we enter the weaponState
        public virtual void Enter()
        {
            DoChecks();
            Entity.PlayerAnimator.SetBool(_animationBoolName, true);
            StartTime = Time.time;
            IsAnimationFinished = false;
            IsExitingState = false;
        }
        
        // This Function is called when we leave the weaponState
        public virtual void Exit()
        {
            Entity.PlayerAnimator.SetBool(_animationBoolName, false);
            IsExitingState = true;
        }
        
        // This Function is called in each Frame of the game
        public virtual void LogicUpdate()
        {
        }
        
        // This Function is called on every Fixed update
        public virtual void PhysicsUpdate()
        {
            DoChecks();
        }

        /* We will use it from PhysicsUpdate() and from Enter(), so in this function we will tell the weaponState if we
         * want it to look for ground or walls, things like that, that way we don't have to call them in the physics
         * update and in each weaponState, we can do it once and do checks */
        protected virtual void DoChecks() { }
        
        // We use it if we want to activate an action in the middle of an animation
        public virtual void AnimationTrigger() { }
        
        // We use it to inform the weaponState that the animation we are playing has already finished
        public virtual void AnimationFinishTrigger() => IsAnimationFinished = true;
    }
}
