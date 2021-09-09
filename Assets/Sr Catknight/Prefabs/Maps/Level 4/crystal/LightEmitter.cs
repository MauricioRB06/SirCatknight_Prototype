using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LightEmitter : MonoBehaviour
{
    LineRenderer lr;
    [SerializeField]
    bool on = true;
    public Material  material;
    public float width = 0.1f;

    // Start is called before the first frame update

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
        lr.material = material;
        lr.numCornerVertices = 1;
        lr.numCapVertices = 1;
        lr.startWidth = width;
        lr.endWidth = width;
        lr.textureMode = LineTextureMode.Tile;

        lr.SetPosition(0, transform.position);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!on) return;
        Vector3 origin = transform.position;

        lr.SetPosition(0, origin);

        Vector3 direction = transform.up;

        RaycastHit2D hit = Physics2D.Raycast(origin, direction);

        lr.SetPosition(1, hit.point);
        GameObject collidedObj = hit.collider.gameObject;
        
        if (collidedObj != null)
        {
            LightMirror mirror = collidedObj.GetComponent<LightMirror>();
            if (mirror != null)
            {
                mirror.reflect(lr, hit, direction,1);
            }
            
        } 
    }
}
