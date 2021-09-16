
/* The purpose of this script is:
 
    Act as a central component to provide functionalities to any object */

using SrCatknight.Scripts.Core.CoreComponents;
using SrCatknight.Scripts.Generics;
using UnityEngine;

namespace SrCatknight.Scripts.Core
{ 
    public class Core : MonoBehaviour
    {
        // 
        private Movement _movement;
        private CollisionSenses _collisionSenses;
        private Combat _combat;
        
        // 
        public Movement Movement
        {
            get => GenericNotImplementedError<Movement>.TryGet(_movement, transform.parent.name);
            private set => _movement = value;
        }
        
        // 
        public CollisionSenses CollisionSenses
        {
            get => GenericNotImplementedError<CollisionSenses>.TryGet(_collisionSenses, transform.parent.name);
            private set => _collisionSenses = value;
        }
        
        // 
        public Combat Combat
        {
            get => GenericNotImplementedError<Combat>.TryGet(_combat, transform.parent.name);
            private set => _combat = value;
        }
        
        // 
        private void Awake()
        {
            Movement = GetComponentInChildren<Movement>();
            CollisionSenses = GetComponentInChildren<CollisionSenses>();
            Combat = GetComponentInChildren<Combat>();
        }
        
        // 
        public void PhysicsUpdate()
        {
            Movement.PhysicsUpdate();
            Combat.PhysicsUpdate();
        }
    }
}
