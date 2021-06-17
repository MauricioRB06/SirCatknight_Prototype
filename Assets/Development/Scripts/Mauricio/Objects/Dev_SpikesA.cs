using UnityEngine;

namespace Development.Scripts.Mauricio.Objects
{
    public class Static_Spikes : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("Player"))
            {
                Debug.Log("Damage");
                Destroy(collision.gameObject);
            }
        }
    }
}
