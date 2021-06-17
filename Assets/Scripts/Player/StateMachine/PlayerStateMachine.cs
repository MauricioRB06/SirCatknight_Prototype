
/* Documentation:
 * 
 * Auto-implemented: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/properties
 * 
 */

namespace Player.StateMachine
{
    public class PlayerStateMachine
    {
        /* The state machine is very simple, it is basically a variable that has a reference to our current state,
         * a function to initialize our state and another function to change state */
        public PlayerState CurrentState { get; private set; }
        
        public void Initialize(PlayerState startingState)
        {
            CurrentState = startingState;
            CurrentState.Enter();
        }

        public void ChangeState(PlayerState newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}
