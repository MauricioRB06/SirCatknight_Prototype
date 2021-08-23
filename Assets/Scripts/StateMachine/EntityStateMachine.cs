
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The Purpose Of This Script Is:
//
//  Create a state machine, which allows to change the behavior of the entity.
//
//  Documentation and References:
//
//  C# Properties: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/properties
//  C# Fields: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/fields

namespace StateMachine
{
    public class EntityStateMachine
    {
        // Controls the current state of the entity.
        public EntityState CurrentState { get; private set; }
        
        // Initializes the state machine, with a first initial state.
        public void Initialize(EntityState startingState)
        {
            CurrentState = startingState;
            CurrentState.Enter();
        }
        
        // Change the current state to a new one.
        public void ChangeState(EntityState newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}
