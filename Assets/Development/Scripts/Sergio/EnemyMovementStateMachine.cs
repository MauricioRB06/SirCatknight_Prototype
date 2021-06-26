using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class EnemyMovementStateMachine : MonoBehaviour
{

    public static state Idle = new state()
    {
            Enter = (eC) => Debug.Log("Ahora Toy quieto"),
            Exit = (eC) => Debug.Log("Depronto Dejo de estar quieto"),
            LogicUpdate = (eC) => {
                Debug.Log("Estando quieto");
                int movement = eC.ShouldMove();
                if (movement != 0) ChangeState(eC, Moving);
                eC.horizontalMove = movement;
                return () => { };
            }
        };
    public static state Moving = new state()
    {
            Enter = (eC) => Debug.Log("Vo a empezar a moverme"),
            Exit = (eC) => Debug.Log("A dejar de moverse"),
            LogicUpdate = (eC) => {
                Debug.Log("Me toy moviendo");
                int movement = eC.ShouldMove();
                if (movement == 0) ChangeState(eC, Idle);
                eC.horizontalMove = movement;
                return () => eC.Move();
            }
        };
    public static state InitialState = Idle;
    //[SerializeField] GameObject enemy;
    //EnemyController enContrl;
    // Start is called before the first frame update
    public static void ChangeState(EnemyController eC, state newState)
    {
        eC.currentState.Exit(eC);
        eC.currentState = newState;
        eC.currentState.Enter(eC);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
