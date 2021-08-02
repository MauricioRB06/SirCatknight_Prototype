using UnityEngine;

namespace _Development.Scripts.Mauricio
{
    public class MessageTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject message;
        
        private void Start()
        {
            message.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            message.SetActive(true);
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            message.SetActive(false);
        }
    }
}
