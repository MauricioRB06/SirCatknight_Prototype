using UnityEngine;

/* The purpose of this script is:
  
  Insert Here.

  Documentation and References:

  

*/

namespace _Development.Scripts.Mauricio.Levels
{
    [RequireComponent(typeof(BoxCollider2D))]
   public class SimpleDamageObject : MonoBehaviour
    {
        public float damageToGive = 10;
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.transform.CompareTag("Player")) return;
            
            collision.transform.GetComponent<Player.PlayerController>().Damage(damageToGive);
        }
    }
}
