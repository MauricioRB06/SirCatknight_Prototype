
/* The purpose of this script is:
 
    Act as a central component to provide functionalities to any object */

using Core.CoreComponents;
using Generics;
using UnityEngine;

namespace Core
{ 
    public class Core : MonoBehaviour
    {
        // 
        private Movement movement;
        private CollisionSenses collisionSenses;
        private Combat combat;
        
        // 
        public Movement Movement
        {
            get => GenericNotImplementedError<Movement>.TryGet(movement, transform.parent.name);
            private set => movement = value;
        }
        
        // 
        public CollisionSenses CollisionSenses
        {
            get => GenericNotImplementedError<CollisionSenses>.TryGet(collisionSenses, transform.parent.name);
            private set => collisionSenses = value;
        }
        
        // 
        public Combat Combat
        {
            get => GenericNotImplementedError<Combat>.TryGet(combat, transform.parent.name);
            private set => combat = value;
        }
        
        // 
        private void Awake()
        {
            Movement = GetComponentInChildren<Movement>();
            CollisionSenses = GetComponentInChildren<CollisionSenses>();
            Combat = GetComponentInChildren<Combat>();
        }
        
        // 
        public void LogicUpdate()
        {
            Movement.LogicUpdate();
            Combat.LogicUpdate();
        }
    }
}
