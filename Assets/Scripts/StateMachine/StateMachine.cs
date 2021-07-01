
/* Documentation:
 *
 * Auto-implemented: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/properties
 *
 */

namespace StateMachine
{
    public class StateMachine
    {
        /* The state machine is very simple, basically it is a variable that has a reference to the current state of
         the entity, a function to initialize the state for the first time and another function to change the state */
        public EntityState CurrentState { get; private set; }
        
        public void Initialize(EntityState startingState)
        {
            CurrentState = startingState;
            CurrentState.Enter();
        }

        public void ChangeState(EntityState newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}
