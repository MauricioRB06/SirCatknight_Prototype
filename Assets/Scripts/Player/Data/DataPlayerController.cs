using UnityEngine;

// The purpose of this Script is:
// Insert Here.

/* Documentation and References:
 * 
 * Attributes: https://docs.unity3d.com/Manual/Attributes.html
 * ScriptableObject: https://docs.unity3d.com/Manual/class-ScriptableObject.html
 * CreateAssetMenu: https://docs.unity3d.com/ScriptReference/CreateAssetMenuAttribute.html
 * https://docs.unity3d.com/ScriptReference/CreateAssetMenuAttribute.html
 */

namespace Player.Data
{
    [CreateAssetMenu(fileName ="newPlayerControllerData",
        menuName = "Sr.Catknight Data/Player Data/1. Controller Data", order = 1)]
    
    public class DataPlayerController : ScriptableObject
    {
        [Header("Settings Idle State")][Space(3)]
        public float sleepTime = 10f;
        [Space(10)]
        
        [Header("Settings Move State")][Space(3)]
        public float runVelocity = 6f;
        public float walkVelocity = 3f;
        [Space(10)]
        
        [Header("Settings Jump State")][Space(3)]
        public float jumpForce = 15f;
        [Range(0f, 2f)] public int amountOfJumps = 1;
        [Space(10)]
        
        [Header(("Settings Dodge Roll State"))][Space(3)]
        public float dodgeRollImpulse = 100f;
        public float dodgeRollInputMaxTime = 1f;
        public float dodgeRollCooldown = 1f;
        public float dodgeRollLifeTime = 0.002f;
        [Space(10)]
        
        [Header(("Settings Interact State "))][Space(3)]
        public float interactionRadius = 0.3f;
        public LayerMask interactableLayer;  
        [Space(10)]
        
        [Header("Settings Wall Jump State")][Space(3)]
        public float wallJumpVelocity = 20;
        public Vector2 wallJumpAngle = new Vector2(1, 2);
        
        // We use it to avoid that after jumping the player can return to the wall immediately after.
        public float wallJumpTime = 0.4f;
        [Space(10)]
        
        [Header("Settings In Air State")][Space(3)]
        public float coyoteTime = 0.2f;
        // We use it to limit the jump the moment the player releases the button before completing a full jump.
        public float jumpHeightLimiter = 0.5f;
        [Space(10)]
        
        [Header("Settings Wall Slide State")][Space(3)] 
        public float wallSlideVelocity = 4f;
        [Space(10)]
        
        [Header("Settings Wall Climb State")][Space(3)]
        public float wallClimbVelocity = 3f;
        [Space(10)]
        
        [Header("Settings Ledge Climb State")][Space(3)]
        public Vector2 startOffset;
        public Vector2 stopOffset;
        [Space(10)]
        
        [Header("Settings Dash State")][Space(3)]
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
        [Space(10)]
        
        [Header("Settings Crouch States")][Space(3)]
        public float crouchMovementVelocity = 3f;
        public float crouchColliderHeight = 1.3f;
        public float normalColliderHeight = 1.6f;
        [Space(10)]
        
        [Header("Settings Attack State")][Space(3)]
        public float decreaseGravityScale = 0.75f;
        public float restoreGravityScale = 3.0f;
    }
}
