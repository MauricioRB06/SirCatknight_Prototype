using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class Credits : MonoBehaviour
{
    void Start()
    {
        Invoke("EndCredits",120);
    }

    void Update()
    {
        
    }

    public void EndCredits()
    {
        SceneManager.LoadScene("Main_Menu");
    }
}
