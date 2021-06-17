using UnityEngine;

namespace Development.Scripts.Mauricio.Objects
{
    public class Sword_Pickup : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                GetComponent<SpriteRenderer>().enabled = false;
                gameObject.transform.GetChild(0).gameObject.SetActive(true);
                Destroy(gameObject, 0.8f);
            }
        }
    }
}
