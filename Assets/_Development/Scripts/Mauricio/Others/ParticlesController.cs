using UnityEngine;

/* The purpose of this Script is:
 
  Insert Here

  Documentation and References:

  Destroy: https://docs.unity3d.com/ScriptReference/Object.Destroy.html
 
 */

namespace _Development.Scripts.Mauricio.Others
{
    public class ParticlesController : MonoBehaviour
    {
        private void FinishAnimation()
        {
            Destroy(gameObject);
        }
    }
}
