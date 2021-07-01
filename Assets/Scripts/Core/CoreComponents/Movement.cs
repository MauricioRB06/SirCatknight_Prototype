using UnityEngine;

/* The purpose of our MovementScript is to have a set of functions that set the speed of our rigid body */

/* Documentation:
 * 
 *
 * 
 */

namespace Core.CoreComponents
{
    public class Movement : CoreComponent
    {
        // We use them to access the player components
        private Rigidbody2D RigidBody2D { get; set; }
        
        //
        public int FacingDirection { get; private set; }
        
        // We use it to not call the RigidBody2D and ask for the speed in 'Y' at all times
        public Vector2 CurrentVelocity { get; private set; }
    
        // We use it so we don't have to create a new vector every time the character moves
        private Vector2 _newVelocity;
        
        protected override void Awake()
        {
            base.Awake();

            RigidBody2D = GetComponentInParent<Rigidbody2D>();
            
            // The character always starts looking to the right
            FacingDirection = 1;
        }

        public void LogicUpdate()
        {
            CurrentVelocity = RigidBody2D.velocity;
        }
        
        // we use it to set the player's speed to 0
        public void SetVelocityZero()
        {
            RigidBody2D.velocity = Vector2.zero;
            CurrentVelocity = Vector2.zero;
        }
        
        // We use it to apply a speed in a wall jump
        public void SetVelocity(float velocity, Vector2 angle, int direction)
        {
            angle.Normalize();
            _newVelocity.Set(angle.x * velocity * direction, angle.y * velocity);
            RigidBody2D.velocity = _newVelocity;
            CurrentVelocity = _newVelocity;
        }
        
        // We use it to apply a speed in the DashState
        public void SetVelocity(float velocity, Vector2 direction)
        {
            _newVelocity = direction * velocity;
            RigidBody2D.velocity = _newVelocity;
            CurrentVelocity = _newVelocity;
        }
        
        // We use it to apply a moving force on the X axis
        public void SetVelocityX(float velocity)
        {
            _newVelocity.Set(velocity, CurrentVelocity.y);
            RigidBody2D.velocity = _newVelocity;
            CurrentVelocity = _newVelocity;
        }
        
        // We use it to apply a moving force on the Y axis
        public void SetVelocityY(float velocity)
        {
            _newVelocity.Set(CurrentVelocity.x, velocity);
            RigidBody2D.velocity = _newVelocity;
            CurrentVelocity = _newVelocity;
        }
        
        // Check if we should rotate the character
        public void CheckIfShouldFlip(int xInput)
        {
            if(xInput != 0 && xInput != FacingDirection)
            {
                Flip();
            }
        }
        
        // Rotate the character according to the direction of movement
        private void Flip()
        {
            FacingDirection *= -1;
            RigidBody2D.transform.Rotate(0.0f, 180.0f, 0.0f);
        }
    }
}
