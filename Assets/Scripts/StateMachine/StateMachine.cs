using Player;

/* The purpose of this script is:
  
  Allow an object to switch between different states.

  Documentation and References:

  Properties: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/properties
  Fields: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/fields

*/

namespace StateMachine
{
    public class StateMachine
    {
        public PlayerState CurrentState { get; private set; }
        
        // We use it to set the state that the object will have when it is started.
        public void Initialize(PlayerState startingState)
        {
            CurrentState = startingState;
            CurrentState.Enter();
        }
        
        // We use it to change the state of the object.
        public void ChangeState(PlayerState newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}
