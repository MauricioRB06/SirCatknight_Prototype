using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMirror : MonoBehaviour
{
    public void rotate()
    {
        
    }

    public void reflect(LineRenderer lr, RaycastHit2D originalHit, Vector3 originalDirection, int hitCount)
    {
        Vector3 direction = Vector3.Reflect(originalDirection, originalHit.normal);
        Vector3 origin = ((Vector3)originalHit.point) + 0.01f * direction;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction);
        GameObject collidedObj = hit.collider.gameObject;
        if (lr.positionCount !=(hitCount+2)) lr.positionCount = hitCount+2;
        lr.SetPosition(hitCount + 1, hit.point);
        if (collidedObj != null)
        {
            LightMirror mirror = collidedObj.GetComponent<LightMirror>();
            Debug.Log(string.Format("Game object has mirror {0}", mirror != null));
            if (mirror != null)
            {
                mirror.reflect(lr, hit, direction, hitCount+1);
            }

        }
    }
}
