using UnityEngine;

namespace _Development.Scripts.Mauricio
{
    public class SpecialCamerasSystem : MonoBehaviour
    {
        public GameObject virtualCamera;

        private void Awake()
        {
            virtualCamera.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("Player") && !collision.isTrigger) return;
            virtualCamera.SetActive(true);
        }
        
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("Player") && !collision.isTrigger) return;
            virtualCamera.SetActive(false);
        }
    }
}
