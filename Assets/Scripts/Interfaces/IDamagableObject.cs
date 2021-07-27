
/* The purpose of this script is:
 
    Use interfaces to give the property of receiving damage to any object */

/* Documentation and References:
 * 
 * Interfaces: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/interface
 * Interfaces in C#: https://www.youtube.com/watch?v=A7qwuFnyIpM&ab_channel=IAmTimCorey
 * What are Interfaces?: https://www.youtube.com/watch?v=MZOrGXk4XFI&ab_channel=CodeMonkey
 * Course C# Interfaces: https://www.youtube.com/watch?v=K4JUqONrb8E [ Spanish ]
 * 
 */

namespace Interfaces
{
    public interface IDamageableObject
    {
        void Damage(float amount);
    }
}
