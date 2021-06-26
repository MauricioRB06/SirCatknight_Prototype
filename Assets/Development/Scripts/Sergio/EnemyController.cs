using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyController : MonoBehaviour
{
    [SerializeField] PlayerMovementController controller;
    [SerializeField] [Range(-1, 1)] int whereToMove = 0;
    public float horizontalMove = 0f;
    bool jump = false;
    public state currentState;
    private Action physicsUpdate = () => { };
    private void Awake()
    {
        controller = GetComponent<PlayerMovementController>();

        currentState = EnemyMovementStateMachine.InitialState;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = 0;
        if (false) jump = true; // por alguna razon que no comprendo esto function mejor que jump = Input.GetButtonDown("Jump") 
        physicsUpdate = currentState.LogicUpdate(this);
    }
    private void FixedUpdate()
    {
        physicsUpdate();
        jump = false;
        Debug.Log(jump);
    }

    public void Move()
    {
        controller.Move(horizontalMove, jump);
        Debug.Log(string.Format("---------\nHorizontal Move: {0}\n-------------", horizontalMove));
    }
    public int ShouldMove()
    {
        return whereToMove;
    }
}
