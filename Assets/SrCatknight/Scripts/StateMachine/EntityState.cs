
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The Purpose Of This Script Is:
//
//  To be the basis for generating the different states that the entities can take.
// 
//  Documentation and References:
//
//  C# Protected: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/protected
//  C# Read only: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/readonly
//  C# Polymorphism: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/virtual
//  C# Polymorphism: https://www.youtube.com/watch?v=XzKL94OMDV4&list=PLU8oAlHdN5BmpIQGDSHo5e1r4ZYWQ8m4B&index=46 [ Spanish ]

using Enemies;
using UnityEngine;

namespace SrCatknight.Scripts.StateMachine
{
    public class EntityState
    {
        // To store a reference to the core.
        protected readonly SrCatknight.Scripts.Core.Core Core;
        
        // To store a reference to the entity.
        protected readonly EntityController EntityController;
        
        // Reference to entity state machine.
        protected readonly EntityStateMachine EntityStateMachine;
        
        // To know the time in which we enter a state.
        public float StartTime { get; private set; }
        
        // To get a reference to the name of the animations, inside the animator.
        private readonly string _animationBoolName;
        
        // Class Constructor.
        protected EntityState(EntityController entityController, EntityStateMachine entityStateMachine,
            string animationBoolName)
        {
            EntityController = entityController;
            EntityStateMachine = entityStateMachine;
            _animationBoolName = animationBoolName;
            Core = EntityController.Core;
        }
        
        // This method is called when we enter into a state.
        public virtual void Enter()
        {
            StartTime = Time.time;
            EntityController.EntityAnimator.SetBool(_animationBoolName, true);
            DoChecks();
        }
        
        // This method is called when we exit from a state.
        public virtual void Exit()
        {
            EntityController.EntityAnimator.SetBool(_animationBoolName, false);
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
    }
}
