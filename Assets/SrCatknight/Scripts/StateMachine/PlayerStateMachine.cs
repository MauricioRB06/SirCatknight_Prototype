
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The Purpose Of This Script Is:
//
//  Create a state machine, which allows to change the behavior of the player.
//
//  Documentation and References:
//
//  C# Properties: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/properties
//  C# Fields: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/fields

namespace SrCatknight.Scripts.StateMachine
{
    public class PlayerStateMachine
    {
        // Controls the current state of the player.
        public PlayerState CurrentState { get; private set; }
        
        // Initializes the state machine, with a first initial state.
        public void Initialize(PlayerState startingState)
        {
            CurrentState = startingState;
            CurrentState.Enter();
        }
        
        // Change the current state to a new one.
        public void ChangeState(PlayerState newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}
