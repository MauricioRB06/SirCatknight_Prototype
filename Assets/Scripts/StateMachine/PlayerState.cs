
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The Purpose Of This Script Is:
//
//  To be the basis for generating the different states that the player can take.
// 
//  Documentation and References:
//
//  C# Protected: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/protected
//  C# Read only: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/readonly
//  C# Polymorphism: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/virtual
//  C# Polymorphism: https://www.youtube.com/watch?v=XzKL94OMDV4&list=PLU8oAlHdN5BmpIQGDSHo5e1r4ZYWQ8m4B&index=46 [ Spanish ]

using Player;
using Player.Data;
using UnityEngine;

namespace StateMachine
{
    public class PlayerState
    {
        // To store a reference to the core.
        protected readonly Core.Core Core;
        
        // To store a reference to the player.
        protected readonly PlayerController PlayerController;
        
        // To store a reference to player state machine.
        protected readonly PlayerStateMachine PlayerStateMachine;
        
        // To store a reference to Player data.
        protected readonly DataPlayerController DataPlayerController;                   
        
        // To know if an animation is finished.
        protected bool IsAnimationFinished;
        
        // To know if you are leaving a state.
        protected bool IsExitingState;
        
        // To know the time in which we enter a state.
        protected float StartTime;
        
        // To get a reference to the name of the animations, inside the animator.
        private readonly string _animationBoolName;
        
        // Class Constructor.
        protected PlayerState(PlayerController playerController, PlayerStateMachine playerStateMachine,
            DataPlayerController dataPlayerController, string animationBoolName)
        {
            PlayerController = playerController;
            PlayerStateMachine = playerStateMachine;
            DataPlayerController = dataPlayerController;
            _animationBoolName = animationBoolName;
            Core = PlayerController.Core;
        }
        
        // This method is called when we enter into a state.
        public virtual void Enter()
        {
            DoChecks();
            PlayerController.PlayerAnimator.SetBool(_animationBoolName, true);
            StartTime = Time.time;
            IsAnimationFinished = false;
            IsExitingState = false;
        }
        
        // This method is called when we exit from a state.
        public virtual void Exit()
        {
            PlayerController.PlayerAnimator.SetBool(_animationBoolName, false);
            IsExitingState = true;
        }
        
        // This method is called at each frame of the game (within update).
        public virtual void LogicUpdate()
        {
        }
        
        // This method is called at each fixed update.
        public virtual void PhysicsUpdate()
        {
            DoChecks();
        }
        
        // They are called from PhysicsUpdate() and Enter(), in this method we will ask the state in which we are
        // to check the floor or the walls for example, this way we do not have to call them in each fixed update
        // if we do not need to make these checks.
        protected virtual void DoChecks()
        {
        }
        
        // We use it to trigger events in the middle of an animation ( We call it inside the animator ).
        public virtual void AnimationTrigger()
        {
        }
        
        // We use it to trigger events at the end of an animation ( We call it inside the animator ).
        public virtual void AnimationFinishTrigger() => IsAnimationFinished = true;
    }
}
