using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{
    public Camera camera;

    public Transform subject;

    Vector2 startPosition;
    float startZ;
    Vector2 travel => (Vector2)camera.transform.position - startPosition;
    Vector2 parallaxFactor;



    public void Start()
    {
        startPosition = transform.position;
        startZ = transform.position.z;

    }

    public void Update()
    {
        transform.position = startPosition + travel;
    }

}
