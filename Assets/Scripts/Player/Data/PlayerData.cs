using UnityEngine;
using UnityEngine.Serialization;

/* Documentation:
 * 
 * Attributes: https://docs.unity3d.com/Manual/Attributes.html
 * ScriptableObject: https://docs.unity3d.com/Manual/class-ScriptableObject.html
 * CreateAssetMenu: https://docs.unity3d.com/ScriptReference/CreateAssetMenuAttribute.html
 * 
 */

namespace Player.Data
{
    [CreateAssetMenu(fileName ="newPlayerData", menuName ="Data/Player Data/Base Data")]
    public class PlayerData : ScriptableObject
    {
        [Header("Move State")]
        public float movementVelocity = 10f;

        [Header("Jump State")]
        public float jumpForce = 15f;
        [Range(0f, 2f)]
        public int amountOfJumps;

        [Header("Wall Jump State")]
        public float wallJumpVelocity = 20;
        public float wallJumpTime = 0.4f;
        public Vector2 wallJumpAngle = new Vector2(1, 2);

        [Header("In Air State")]  
        public float coyoteTime = 0.2f;
        public float jumpHeightLimiter = 0.5f;

        [Header("Wall Slide State")]  
        public float wallSlideVelocity = 5f;

        [Header("Wall Climb State")]  
        public float wallClimbVelocity = 4f;

        [Header("Ledge Climb State")] 
        public Vector2 startOffset;
        public Vector2 stopOffset;

        [Header("Dash State")] 
        public float dashCooldown = 0.5f;
        public float maxHoldTime = 1f;
        public float holdTimeScale = 0.25f;
        public float dashTime = 0.2f;
        public float dashVelocity = 30f;
        public float drag = 10f;
        public float dashEndYMultiplier = 0.2f;
        public float distBetweenAfterImages = 0.5f;
        
        [Header("Crouch States")]
        public float crouchMovementVelocity = 5f;
        public float crouchColliderHeight = 0.8f;
        public float standColliderHeight = 1.6f;
        
        // Change the properties to detect the floor and walls
        [Header("Check Variables")]
        public float groundCheckRadius = 0.3f;
        public float wallCheckDistance = 0.5f;
        public LayerMask layerGroundAndWalls;

        [Header("AttackSword")]
        public int amountOfAttacks = 1;
    }
}
