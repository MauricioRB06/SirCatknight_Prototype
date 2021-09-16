
/* The purpose of this script is:
 
    Establish the references and functionalities that all components will have */

/* Documentation and References:
 * 
 * Transform.parent: https://docs.unity3d.com/ScriptReference/Transform-parent.html
 * 
 */

using UnityEngine;

namespace SrCatknight.Scripts.Core
{
    public class CoreComponent : MonoBehaviour
    {
        // We use it to store the reference to the core of the object
        protected Core Core;
        
        // 
        protected virtual void Awake()
        {
            Core = transform.parent.GetComponent<Core>();
            
            // 
            if (Core == null) { Debug.LogError("This component must have a Core object as its parent"); }
        }
    }
}
