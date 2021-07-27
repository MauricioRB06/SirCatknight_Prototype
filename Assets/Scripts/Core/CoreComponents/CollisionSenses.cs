using UnityEngine;

/* The purpose of this script is:

    Having a set of properties that perform various collision checks

    Documentation and References:

    SerializeField: https://docs.unity3d.com/ScriptReference/SerializeField.html
    C# Encapsulate Fields: https://docs.microsoft.com/en-us/visualstudio/ide/reference/encapsulate-field?view=vs-2019
    C# Encapsulation: https://www.youtube.com/watch?v=_eyFoySmHPk&list=PLU8oAlHdN5BmpIQGDSHo5e1r4ZYWQ8m4B&index=30 [Spanish]
    C# Expression Body: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/expression-bodied-members
    C# Properties: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/properties
    C# Properties: https://www.youtube.com/watch?v=MAMOyX59pNo&list=PLU8oAlHdN5BmpIQGDSHo5e1r4ZYWQ8m4B&index=56 [Spanish]
    Physics2D.OverlapCircle: https://docs.unity3d.com/ScriptReference/Physics2D.OverlapCircle.html
    Physics2D.Raycast: https://docs.unity3d.com/ScriptReference/Physics2D.Raycast.html
 
 */

namespace Core.CoreComponents
{
    public class CollisionSenses : CoreComponent
    {
        // We use them to store the references to the objects that are responsible for performing the checks
        [SerializeField] private Transform groundCheck;
        [SerializeField] private Transform wallCheck;
        [SerializeField] private Transform ledgeCheck;
        [SerializeField] private Transform ceilingCheck;
        
        // We use them to store the values that will be used to perform the checks
        [SerializeField] private float groundCheckRadius = 0.3f;
        [SerializeField] private float ceilingCheckRadius = 0.2f;
        [SerializeField] private float wallCheckDistance = 0.5f;
        [SerializeField] private LayerMask layerGroundWalls;
        
        // We use them to access these properties from the outside, while maintaining the original private properties
        public Transform GroundCheck => groundCheck;
        public Transform WallCheck => wallCheck;
        public Transform LedgeCheck => ledgeCheck;
        public Transform CeilingCheck => ceilingCheck;
        public float GroundCheckRadius => groundCheckRadius;
        public float CeilingCheckRadius => ceilingCheckRadius;
        public float WallCheckDistance => wallCheckDistance;
        public LayerMask LayerGroundWalls => layerGroundWalls;
        
        // We use it to detect if the object is touching a ceiling
        public bool Ceiling => Physics2D.OverlapCircle(ceilingCheck.position, ceilingCheckRadius, 
            layerGroundWalls);

        // We use it to detect if the object is touching the ground
        public bool Ground => Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius,
            layerGroundWalls);

        // We use it to detect if the object is touching a wall head-on
        public bool WallFront => Physics2D.Raycast(wallCheck.position,
            Vector2.right * Core.Movement.FacingDirection, wallCheckDistance, layerGroundWalls);

        // We use it to detect if the object is touching a wall from behind
        public bool WallBack => Physics2D.Raycast(wallCheck.position,
            Vector2.right * -Core.Movement.FacingDirection, wallCheckDistance, layerGroundWalls);

        // We use it to detect if the object is touching a ledge
        public bool Ledge => Physics2D.Raycast(ledgeCheck.position,
            Vector2.right * Core.Movement.FacingDirection, wallCheckDistance, layerGroundWalls);
    }
}
