using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LightRay : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField]
    bool on = false;
    [SerializeField]
    [Range(-180f,180f)]
    float angle = 90f;
    [SerializeField]
    float distance = 10f;
    [SerializeField]
    int maxBounces = 1;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (!on) return;
        
        int currentBounce = 0;
        Vector3 origin = rb.transform.position;
        Vector3 initialDirection = Quaternion.Euler(0f,0f,angle) * Vector3.up;
        Vector3 currPosition = origin;
        Vector3 currDirection = initialDirection;
        RaycastHit2D currHit;
        float currAngle = angle;
        Color[] colrs = { Color.cyan, Color.magenta };
     

        for (int i = 0; i < maxBounces; i++)
        {
            Debug.Log("Casting from: " + currPosition.x + "," + currPosition.y + "in direction" + currDirection.x + "," + currDirection.y);
            currHit = Physics2D.Raycast(currPosition, currDirection);
            if (currHit == null) break;
            Debug.DrawLine(currPosition, currHit.point, Color.red);
            Debug.DrawRay(currHit.point, currHit.normal, colrs[i % 2]);
            
            currPosition = currHit.point;
            currDirection = Vector3.Reflect(currDirection, currHit.normal);
            Debug.DrawRay(currPosition, currDirection);
            currPosition += 0.01f * currDirection ;

        }


    }
}
