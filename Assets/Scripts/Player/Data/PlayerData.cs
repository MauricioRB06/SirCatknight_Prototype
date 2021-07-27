using UnityEngine;

// The purpose of this Script is:
// Insert Here.

/* Documentation and References:
 * 
 * Attributes: https://docs.unity3d.com/Manual/Attributes.html
 * ScriptableObject: https://docs.unity3d.com/Manual/class-ScriptableObject.html
 * CreateAssetMenu: https://docs.unity3d.com/ScriptReference/CreateAssetMenuAttribute.html
 * 
 */

namespace Player.Data
{
    [CreateAssetMenu(fileName ="newPlayerData", menuName ="Data/Entity Data/Base Data")]
    public class PlayerData : ScriptableObject
    {
        [Header("Idle State")]
        public float sleepTime = 10f;
        
        [Header("Move State")]
        public float runVelocity = 6f;
        public float walkVelocity = 3f;

        [Header("Jump State")]
        public float jumpForce = 15f;
        [Range(0f, 2f)] public int amountOfJumps = 1;

        [Header(("Dodge Roll"))]
        public float dodgeRollImpulse = 30f;
        public float dodgeRollLifeTime = 0.4f;

        [Header("Wall Jump State")]
        public float wallJumpVelocity = 20;
        public Vector2 wallJumpAngle = new Vector2(1, 2);
        
        // We use it to avoid that after jumping the player can return to the wall immediately after.
        public float wallJumpTime = 0.4f;
        
        [Header("In Air State")]  
        public float coyoteTime = 0.2f;
        // We use it to limit the jump the moment the player releases the button before completing a full jump.
        public float jumpHeightLimiter = 0.5f;

        [Header("Wall Slide State")]  
        public float wallSlideVelocity = 4f;

        [Header("Wall Climb State")]  
        public float wallClimbVelocity = 3f;

        [Header("Ledge Climb State")] 
        public Vector2 startOffset;
        public Vector2 stopOffset;

        [Header("Dash State")] 
        public float dashCooldown = 5f;
        
        /* We use it to modify the time scale of the game and make the effect of slow motion, as well as to know
         the maximum time that we will be in that weaponState */
        public float dashMaxHoldTime = 1f;
        public float dashHoldTimeScale = 0.25f;
        
        // It is the life time of the Dash movement, once the timeout is reached or the player releases the button.
        public float dashLifeTime = 0.2f;
        
        // The speed that we are going to apply to the player.
        public float dashImpulseVelocity = 30f;
        
        // We use it to limit the distance traveled by the Dash on the Y axis.
        public float dashEndYLimiter = 0.2f;
        
        // We use it to separate the images from the AfterImagePool.
        public float distanceBetweenAfterImages = 0.5f;
        
        [Header("Crouch States")]
        public float crouchMovementVelocity = 3f;
        public float crouchColliderHeight = 1.3f;
        public float normalColliderHeight = 1.6f;

        [Header("Attack State")]
        public float decreaseGravityScale = 0.75f;
        public float restoreGravityScale = 3.0f;
    }
}
