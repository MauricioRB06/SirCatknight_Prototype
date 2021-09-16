
/* The purpose of this script is:
 
    Have a set of functions that set the velocity of the rigid body component of the object */

using UnityEngine;

namespace SrCatknight.Scripts.Core.CoreComponents
{
    public class Movement : CoreComponent
    {
        // We use it to store a reference to the rigid body component of the object
        public Rigidbody2D RigidBody2D { get; private set; }
        
        // We use it to detect the direction in which the object is facing
        public int FacingDirection { get; private set; }
        
        // 
        public bool CanSetVelocity { get; set; }
        
        // We use it to avoid calling the RigidBody2D component and ask for the 'Y' velocity at all times
        public Vector2 CurrentVelocity { get; private set; }
    
        // We use it to avoid having to create a new vector every time the character moves
        private Vector2 _newVelocity;
        
        // 
        protected override void Awake()
        {
            base.Awake();

            RigidBody2D = GetComponentInParent<Rigidbody2D>();
            
            // By default, the object will always be placed facing right
            FacingDirection = 1;
            CanSetVelocity = true;
        }
        
        // 
        public void PhysicsUpdate()
        {
            CurrentVelocity = RigidBody2D.velocity;
        }
        
        // 
        private void SetFinalVelocity()
        {
            if (!CanSetVelocity) return;
            
            RigidBody2D.velocity = _newVelocity;
            CurrentVelocity = _newVelocity;
        }
        
        // We use it to set the object's velocity to 0 and keep it in position
        public void SetVelocityZero()
        {
            _newVelocity = Vector2.zero;
            SetFinalVelocity();
        }
        
        // We use it to apply a force to the object at an angle ( e.g. for jumping from a wall )
        public void SetVelocity(float velocity, Vector2 angle, int direction)
        {
            angle.Normalize();
            _newVelocity.Set(angle.x * velocity * direction, angle.y * velocity);
            SetFinalVelocity();
        }
        
        // We use it to apply a moving force to the object in a specific direction
        public void SetVelocity(float velocity, Vector2 direction)
        {
            _newVelocity = direction * velocity;
            SetFinalVelocity();
        }
        
        // We use it to apply a moving force to the object in the X axis
        public void SetVelocityX(float velocity)
        {
            _newVelocity.Set(velocity, CurrentVelocity.y);
            SetFinalVelocity();
        }
        
        // We use it to apply a moving force to the object in the Y axis
        public void SetVelocityY(float velocity)
        {
            _newVelocity.Set(CurrentVelocity.x, velocity);
            SetFinalVelocity();
        }
        
        // We use it to check if the direction of movement is the same as the direction in which the object is facing
        public void CheckIfShouldFlip(int xInput)
        {
            if(xInput != 0 && xInput != FacingDirection)
            {
                Flip();
            }
        }
    
        // We use it to reduce the gravity affecting the object
        public void ReduceGravityScale(float gravityScale)
        {
            RigidBody2D.gravityScale = gravityScale;
        }
        
        // We use it to reset the gravity affecting the object
        public void RestoreGravityScale(float gravityScale)
        {
            RigidBody2D.gravityScale = gravityScale;
        }
        
        // We use it to rotate the object sprite
        public void Flip()
        {
            FacingDirection *= -1;
            RigidBody2D.transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }
}
