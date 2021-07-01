using UnityEngine;

namespace Development.Scripts.Mauricio
{
    public class ParticlesController : MonoBehaviour
    {
        private void FinishAnimation()
        {
            Destroy(gameObject);
        }
    }
}