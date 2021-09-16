
/* The purpose of this script is:

    Having a set of properties that perform various collision checks

    Documentation and References:

    SerializeField: https://docs.unity3d.com/ScriptReference/SerializeField.html
    C# Encapsulate Fields: https://docs.microsoft.com/en-us/visualstudio/ide/reference/encapsulate-field?view=vs-2019
    C# Expression Body: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/expression-bodied-members
    C# Properties: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/properties
    Physics2D.OverlapCircle: https://docs.unity3d.com/ScriptReference/Physics2D.OverlapCircle.html
    Physics2D.Raycast: https://docs.unity3d.com/ScriptReference/Physics2D.Raycast.html
 
 */

using SrCatknight.Scripts.Generics;
using UnityEngine;

namespace SrCatknight.Scripts.Core.CoreComponents
{
    public class CollisionSenses : CoreComponent
    {
        // We use them to store the references to the objects that are responsible for performing the checks
        [SerializeField] private Transform groundCheck;
        [SerializeField] private Transform wallCheck;
        [SerializeField] private Transform ledgeCheckHorizontal;
        [SerializeField] private Transform ledgeCheckVertical;
        [SerializeField] private Transform ceilingCheck;
        
        // We use them to store the values that will be used to perform the checks
        [SerializeField] private float groundCheckRadius = 0.3f;
        [SerializeField] private float ceilingCheckRadius = 0.2f;
        [SerializeField] private float wallCheckDistance = 0.5f;
        [SerializeField] private LayerMask layerGroundWalls;
        [SerializeField] private LayerMask layerBlockingVolume;
        
        // 
        public Transform GroundCheck
        {
            get => GenericNotImplementedError<Transform>.TryGet(groundCheck, Core.transform.parent.name);
            private set => groundCheck = value;
        }
        
        public Transform WallCheck
        {
            get => GenericNotImplementedError<Transform>.TryGet(wallCheck, Core.transform.parent.name);
            private set => wallCheck = value;
        }
        
        public Transform CeilingCheck
        {
            get => GenericNotImplementedError<Transform>.TryGet(ceilingCheck, Core.transform.parent.name);
            private set => ceilingCheck = value;
        }
        
        public Transform LedgeCheckHorizontal
        {
            get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckHorizontal, Core.transform.parent.name);
            private set => ledgeCheckHorizontal = value;
        }
        
        public Transform LedgeCheckVertical
        {
            get => GenericNotImplementedError<Transform>.TryGet(ledgeCheckVertical, Core.transform.parent.name);
            private set => ledgeCheckVertical = value;
        }
        
        // We use them to access these properties from the outside, while maintaining the original private properties
        public float GroundCheckRadius => groundCheckRadius;
        public float WallCheckDistance => wallCheckDistance;
        public float CeilingCheckRadius => ceilingCheckRadius;
        public LayerMask LayerGroundWalls => layerGroundWalls;
        public LayerMask LayerBlockingVolume => layerBlockingVolume;
        
        // We use it to detect if the object is touching a ceiling
        public bool Ceiling => Physics2D.OverlapCircle(CeilingCheck.position, ceilingCheckRadius, 
            layerGroundWalls | layerBlockingVolume);

        // We use it to detect if the object is touching the ground
        public bool Ground => Physics2D.OverlapCircle(GroundCheck.position, groundCheckRadius,
            layerGroundWalls);

        // We use it to detect if the object is touching a wall head-on
        public bool WallFront => Physics2D.Raycast(WallCheck.position,
            Vector2.right * Core.Movement.FacingDirection, wallCheckDistance, layerGroundWalls);

        // We use it to detect if the object is touching a wall from behind
        public bool WallBack => Physics2D.Raycast(WallCheck.position,
            Vector2.right * -Core.Movement.FacingDirection, wallCheckDistance, layerGroundWalls);
        
        // 
        public bool LedgeHorizontal => Physics2D.Raycast(LedgeCheckHorizontal.position,
                Vector2.right * Core.Movement.FacingDirection, wallCheckDistance, layerGroundWalls);
        
        // 
        public bool LedgeVertical =>  Physics2D.Raycast(LedgeCheckVertical.position,
                Vector2.down, wallCheckDistance, layerGroundWalls);
    }
}
