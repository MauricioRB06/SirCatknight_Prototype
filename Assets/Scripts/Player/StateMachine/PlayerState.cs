using Player.Data;
using UnityEngine;

/* Documentation:
 * 
 * Protected: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/protected
 * Virtual & Override: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/virtual
 * Read only: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/readonly
 * 
 */

namespace Player.StateMachine
{
    public class PlayerState
    {
        protected readonly Player Player;                        // Reference to player 
        protected readonly PlayerStateMachine StateMachine;      // Reference to state machine
        protected readonly PlayerData PlayerData;     // Reference to player data
        
        // We use it when we want to mark an animation as finished 
        protected bool IsAnimationFinished;
        
        // 
        protected bool IsExitingState;

        // We use it to have a reference of how long we have been in a specific state
        protected float StartTime;

        // Refers to variables within the animator 
        private readonly string _animationBoolName;

        // Class Constructor
        protected PlayerState(Player player, PlayerStateMachine stateMachine, PlayerData playerData,
            string animationBoolName)
        {
            this.Player = player;
            this.StateMachine = stateMachine;
            this.PlayerData = playerData;
            this._animationBoolName = animationBoolName;
        }
        
        // This Function is called when we enter the state
        public virtual void Enter() 
        {
            DoChecks();
            Player.PlayerAnimator.SetBool(_animationBoolName, true);
            StartTime = Time.time;
            IsAnimationFinished = false;
            IsExitingState = false;
        }
        
        // This Function is called when we leave the state
        public virtual void Exit()    
        {
            Player.PlayerAnimator.SetBool(_animationBoolName, false);
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

        /* We will use it from PhysicsUpdate() and from Enter(), so in this function we will tell the state if we
         * want it to look for ground or walls, things like that, that way we don't have to call them in the physics
         * update and in each state, we can do it once and do checks */
        protected virtual void DoChecks() { }
        
        // We use it if we want to activate an action in the middle of an animation
        public virtual void AnimationTrigger() { }
        
        // We use it to inform the state that the animation we are playing has already finished 
        public virtual void AnimationFinishTrigger() => IsAnimationFinished = true;
    }
}
