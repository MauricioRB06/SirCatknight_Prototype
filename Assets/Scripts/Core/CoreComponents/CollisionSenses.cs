using UnityEngine;

namespace Core.CoreComponents
{
    public class CollisionSenses : CoreComponent
    {
        //
        public Transform GroundCheck { get => groundCheck; private set => groundCheck = value; }
        public Transform WallCheck { get => wallCheck; private set => wallCheck = value; }
        public Transform LedgeCheck { get => ledgeCheck; private set => ledgeCheck = value; }
        public Transform CeilingCheck { get => ceilingCheck; private set => ceilingCheck = value; }
        
        // We use them to perform player status checks
        [SerializeField] private Transform groundCheck;
        [SerializeField] private Transform wallCheck;
        [SerializeField] private Transform ledgeCheck;
        [SerializeField] private Transform ceilingCheck;
        
        //
        public float GroundCheckRadius { get => groundCheckRadius; private set => groundCheckRadius = value; }
        public float CeilingCheckRadius { get => ceilingCheckRadius; private set => ceilingCheckRadius = value; }
        public float WallCheckDistance { get => wallCheckDistance; private set => wallCheckDistance = value; }
        public LayerMask LayerGroundWalls { get => layerGroundWalls; private set => layerGroundWalls = value; }
        
        // 
        [SerializeField] private float groundCheckRadius = 0.3f;
        [SerializeField] private float ceilingCheckRadius = 0.2f;
        [SerializeField] private float wallCheckDistance = 0.5f;
        [SerializeField] private LayerMask layerGroundWalls;
        
        // We use it to detect if we are touching a ceiling 
        public bool Ceiling => Physics2D.OverlapCircle(ceilingCheck.position, ceilingCheckRadius,
            layerGroundWalls);

        // We use it to check whether or not we are touching the ground
        public bool Ground => Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius,
            layerGroundWalls);

        // We use it to check whether or not we are touching a wall
        public bool WallFront => Physics2D.Raycast(wallCheck.position,
            Vector2.right * Core.Movement.FacingDirection, wallCheckDistance, layerGroundWalls);

        // We use it to check whether or not we are touching a wall behind the player
        public bool WallBack => Physics2D.Raycast(wallCheck.position,
            Vector2.right * -Core.Movement.FacingDirection, wallCheckDistance, layerGroundWalls);

        // We use it to detect if it is touching a ledge
        public bool Ledge => Physics2D.Raycast(ledgeCheck.position,
            Vector2.right * Core.Movement.FacingDirection, wallCheckDistance, layerGroundWalls);
    }
}
