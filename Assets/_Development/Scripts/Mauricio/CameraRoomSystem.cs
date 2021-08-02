using UnityEngine;
using UnityEngine.U2D;

namespace _Development.Scripts.Mauricio
{
    public class CameraRoomSystem : MonoBehaviour
    {
        public GameObject mainCamera;
        public GameObject virtualCamera;
        public int ppuCamera;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("Player") && !collision.isTrigger) return;
            virtualCamera.SetActive(true);
            mainCamera.GetComponent<PixelPerfectCamera>().assetsPPU = ppuCamera;
        }
        
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("Player") && !collision.isTrigger) return;
            virtualCamera.SetActive(false);
        }
    }
}
