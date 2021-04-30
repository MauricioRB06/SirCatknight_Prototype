using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    [Header("Camera")]
    public GameObject playerCamera; // Para Obtener la referencia de la camara
    [Header("Parallax Axis")]
    public bool axisX;
    public bool axisY;
    [Header("Parallax Offset")]
    public float offsetValue;   // Para controlar el dezplazamiento que tendra la capa

    private float lengthBackgroundX, lengthBackgroundY, startPositionX, startPositionY;  // Para obtener la longitud y posicion inicial del sprite
    private float distanceX, distanceY;                         // Para obtener la distancia que deberia moverse el sprite
    private float temporalPositionX, temporalPositionY;                 // La distancia recorrida en relacion a la camara

    void Start()
    {
        startPositionX = transform.position.x;  // Obtenemos la posicion inicial del sprite en X
        startPositionY = transform.position.y;  // Obtenemos la posicion inicial del sprite en Y
        lengthBackgroundX = GetComponent<SpriteRenderer>().bounds.size.x;    // Obtenemos el tamaño del sprite render en X
        lengthBackgroundY = GetComponent<SpriteRenderer>().bounds.size.y;    // Obtenemos el tamaño del sprite render en Y
    }

    void Update()
    {
        if (axisX == true)
        {
            temporalPositionX = (playerCamera.transform.position.x * (1 - offsetValue)); // Para guardar el valor que se ha movido la camara en X
            distanceX = (playerCamera.transform.position.x * offsetValue);               // Para desplazar el sprite, valor recorrido por la camara por el valor del offset

            transform.position = new Vector3(startPositionX + distanceX, transform.position.y, transform.position.z);

            if (temporalPositionX > startPositionX + lengthBackgroundX) startPositionX += lengthBackgroundX; // desplazamos el sprite a la derecha cuando el valor recorrido es mayor al tamaño del sprite
            else if (temporalPositionX < startPositionX - lengthBackgroundX) startPositionX -= lengthBackgroundX;    // nos desplazamos a la izquierda
        }
        else
        {
            transform.position = new Vector3(playerCamera.transform.position.x, transform.position.y, transform.position.z);
        }
        
        if (axisY == true)
        {
            temporalPositionY = (playerCamera.transform.position.y * (1 - offsetValue)); // Para guardar el valor que se ha movido la camara en Y
            distanceY = (playerCamera.transform.position.y * offsetValue);               // Para desplazar el sprite, valor recorrido por la camara por el valor del offset

            transform.position = new Vector3(transform.position.x, startPositionY + distanceY, transform.position.z);

            if (temporalPositionY > startPositionY + lengthBackgroundY) startPositionY += lengthBackgroundY; // desplazamos el sprite hacia arribacuando el valor recorrido es mayor al tamaño del sprite
            else if (temporalPositionY < startPositionY - lengthBackgroundY) startPositionY -= lengthBackgroundY;    // nos desplazamos hacia abajo
        }
        else
        {
            transform.position = new Vector3(transform.position.x, playerCamera.transform.position.y, transform.position.z);
        }

    }

}
