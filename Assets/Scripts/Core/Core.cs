using Core.CoreComponents;
using UnityEngine;

namespace Core
{
    public class Core : MonoBehaviour
    {
       public Movement Movement { get; protected set; }
       public CollisionSenses CollisionSenses { get; protected set; }

       private void Awake()
       {
           Movement = GetComponentInChildren<Movement>();
           if (Movement == null) { Debug.LogError("Missing Core Movement Component"); }
           
           CollisionSenses = GetComponentInChildren<CollisionSenses>();
           if (CollisionSenses == null) { Debug.LogError("Missing Core CollisionSenses Component"); }
       }

       public void LogicUpdate()
       {
           Movement.LogicUpdate();
       }
    }
}