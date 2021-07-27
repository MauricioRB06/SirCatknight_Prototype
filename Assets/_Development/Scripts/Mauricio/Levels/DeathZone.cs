using Player;
using UnityEngine;

namespace _Development.Scripts.Mauricio.Levels
{
    public class DeathZone : MonoBehaviour
    {
        [Header("Indicates the type of death for this zone")] [Space(5)]
        [Tooltip("Depending on the type of death, a different animation will be played")]
        [SerializeField] private string typeOfDeath;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(!collision.gameObject.CompareTag("Player")) return;
            
            if (typeOfDeath == null)
            {
                Debug.LogError("The type of death has not been specified in the zone correctly");
            }
            else
            {
                PlayerController.Die(typeOfDeath);
            }
        }
    }
}
