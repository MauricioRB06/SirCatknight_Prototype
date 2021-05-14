using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Documentación Usada:
 * 
 * Auto-implemented properties: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/properties
 * 
 */

public class PlayerStateMachine
{
    /* la maquina de estados es muy sencilla, es basicamente una variable que tiene una referencia a nuestro estado
     * actual, una funcion para inicializar nuestro estado y otra función para cambiar de estado */

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
