using UnityEngine;

/* In our CoreComponent we are only going to establish the references that all the components will have */

namespace Core.CoreComponents
{
    public class CoreComponent : MonoBehaviour
    {
        // We use it to store the reference to the Core of the main object
        protected Core Core;

        protected virtual void Awake()
        {
            /* The reason why we use .parent is because the CoreComponents are going to be Child's
             of the object that has the main Core */
            
            Core = transform.parent.GetComponent<Core>();
            if (Core == null) { Debug.LogError("There is no Core in parent, please add one"); }
        }
        
    }
}