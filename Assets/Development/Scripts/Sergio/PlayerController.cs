using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerMovementController controller;
    float horizontalMove = 0f;
    bool jump = false;

    private void Awake()
    {
        controller = GetComponent<PlayerMovementController>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump")) jump = true; // por alguna razon que no comprendo esto function mejor que jump = Input.GetButtonDown("Jump") 

    }
    private void FixedUpdate()
    {
        controller.Move(horizontalMove, jump);
        jump = false;
        Debug.Log(jump);
    }
}
