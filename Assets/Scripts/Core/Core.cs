using Core.CoreComponents;
using UnityEngine;

/* The purpose of this script is:
 
    Act as a central component to provide functionalities to any object */

namespace Core
{
    public class Core : MonoBehaviour
    {
       public Movement Movement { get; private set; }
       public CollisionSenses CollisionSenses { get; private set; }

       private void Awake()
       {
           Movement = GetComponentInChildren<Movement>();
           if (Movement == null) { Debug.LogError("Missing Movement Component"); }
           
           CollisionSenses = GetComponentInChildren<CollisionSenses>();
           if (CollisionSenses == null) { Debug.LogError("Missing CollisionSenses Component"); }
       }

       public void LogicUpdate()
       {
           Movement.LogicUpdate();
       }
    }
}