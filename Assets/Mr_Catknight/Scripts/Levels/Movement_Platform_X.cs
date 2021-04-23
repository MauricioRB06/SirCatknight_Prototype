using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Platform_X : MonoBehaviour
{

    [SerializeField] private Transform EndPoint;    // Objeto Destino
    [SerializeField] private float ObjectSpeed;     // Velocidad de movimiento

    private Vector3 PlatformStart, PlatformEnd;     // Puntos de Movimiento

    // Dibujo de Referencias de movimiento
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, EndPoint.position);
        Gizmos.DrawSphere(transform.position, 0.2f);
        Gizmos.DrawSphere(EndPoint.position, 0.2f);
    }

    private void Start() 
    {
        EndPoint.parent = null;                 // Para que no se mueva el punto de destino junto a la plataforma 
        PlatformStart = transform.position;     // Tomamos como punto inicial la posicion de la plataforma
        PlatformEnd = EndPoint.position;        // Tomamos como punto final un objeto
    }

    private void Update()
    {
        // Mover la plataforma desde el punto inicial hacia el punto final
        transform.position = Vector3.MoveTowards(transform.position, EndPoint.position, ObjectSpeed * Time.deltaTime);
        if(transform.position == EndPoint.position)
        {
            // Si se llega al final, se intercambian los puntos
            EndPoint.position = (EndPoint.position == PlatformEnd) ? PlatformStart : PlatformEnd;
        }
    }
}