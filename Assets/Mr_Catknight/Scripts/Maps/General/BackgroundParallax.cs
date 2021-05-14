using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Documentación Usada:
 * 
 * Attributes: https://docs.unity3d.com/Manual/Attributes.html
 * 
 */

public class BackgroundParallax : MonoBehaviour
{
    [Header("Camera")][Tooltip("Camera with respect to which parallax is to be performed")]
    public GameObject parallaxCamera; // Para Obtener la referencia de la camara
    [Header("Parallax Axis")][Tooltip("Define X as the parallax axis")]
    public bool axisX;[Tooltip("Define Y as the parallax axis")]
    public bool axisY;
    [Header("Parallax Offset")]
    [Range(0f,1f)][Tooltip("Defines the layer offset factor")]
    public float offsetValue;   // Para controlar el dezplazamiento que tendra la capa

    private float _sizeBackgroundX, _sizeBackgroundY, _startPositionX, _startPositionY; // Para obtener la longitud y posicion inicial del sprite
    private float _distanceX, _distanceY;                         // Para obtener la distancia que deberia moverse el sprite
    private float _temporalPositionX, _temporalPositionY;                 // La distancia recorrida en relacion a la camara

    void Start()
    {
        _startPositionX = transform.position.x;  // Obtenemos la posicion inicial del sprite en X
        _startPositionY = transform.position.y;  // Obtenemos la posicion inicial del sprite en Y
        _sizeBackgroundX = GetComponent<SpriteRenderer>().bounds.size.x;    // Obtenemos el tamaño del sprite render en X
        _sizeBackgroundY = GetComponent<SpriteRenderer>().bounds.size.y;    // Obtenemos el tamaño del sprite render en Y
    }

    void Update()
    {
        if (axisX == true)
        {
            _temporalPositionX = (parallaxCamera.transform.position.x * (1 - offsetValue)); // Para guardar el valor que se ha movido la camara en X
            _distanceX = (parallaxCamera.transform.position.x * offsetValue);               // Para desplazar el sprite, valor recorrido por la camara por el valor del offset

            transform.position = new Vector3(_startPositionX + _distanceX, transform.position.y, transform.position.z);

            if (_temporalPositionX > _startPositionX + _sizeBackgroundX) _startPositionX += _sizeBackgroundX; // desplazamos el sprite a la derecha cuando el valor recorrido es mayor al tamaño del sprite
            else if (_temporalPositionX < _startPositionX - _sizeBackgroundX) _startPositionX -= _sizeBackgroundX;    // nos desplazamos a la izquierda
        }
        else
        {
            transform.position = new Vector3(parallaxCamera.transform.position.x, transform.position.y, transform.position.z);
        }
        
        if (axisY == true)
        {
            _temporalPositionY = (parallaxCamera.transform.position.y * (1 - offsetValue)); // Para guardar el valor que se ha movido la camara en Y
            _distanceY = (parallaxCamera.transform.position.y * offsetValue);               // Para desplazar el sprite, valor recorrido por la camara por el valor del offset

            transform.position = new Vector3(transform.position.x, _startPositionY + _distanceY, transform.position.z);

            if (_temporalPositionY > _startPositionY + _sizeBackgroundY) _startPositionY += _sizeBackgroundY; // desplazamos el sprite hacia arribacuando el valor recorrido es mayor al tamaño del sprite
            else if (_temporalPositionY < _startPositionY - _sizeBackgroundY) _startPositionY -= _sizeBackgroundY;    // nos desplazamos hacia abajo
        }
        else
        {
            transform.position = new Vector3(transform.position.x, parallaxCamera.transform.position.y, transform.position.z);
        }

    }

}
