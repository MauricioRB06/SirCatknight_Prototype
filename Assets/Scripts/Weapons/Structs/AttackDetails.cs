
// The purpose of this Script is:
/* Insert Here */

/* Documentation and References:
 * 
 * Structs: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/language-specification/structs
 * Struct types: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/struct
 * Course C# Structs: https://www.youtube.com/watch?v=ljiWzVa-vA4 [ Spanish ]
 * 
 */


using UnityEngine;

namespace Weapons.Structs
{
    [System.Serializable]
    public struct WeaponAttackDetails
    {
        // This is not necessary, but we will use it to make it more convenient to find the properties in the inspector
        public string attackName;
        
        public float movementSpeed;
        public float damageAmount;
        public float cooldown;

        public float knockBackStrenght;
        public Vector2 knockBackAngle;
    }
}
