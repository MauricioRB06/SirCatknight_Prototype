using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    /* la maquina de estados es muy sencilla, es basicamente una variable que tiene una referencia a nuestro estado
     * actual, una funcion para inicializar nuestro estado actual y otra función para cambiar de estado */

    public PlayerState CurrentState { get; private set; }   // Creamos un Getter y un Setter para esta variable

    public void Inicialize(PlayerState startingState)
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

