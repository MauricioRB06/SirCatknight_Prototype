using Cinemachine;
using UnityEngine;

namespace SrCatknight.Scripts.Levels.General
{
    public class FindPlayerCamera : MonoBehaviour
    {
        private CinemachineVirtualCamera _virtualCamera;

        private void Awake()
        {
            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        }
        
        private void Update()
        {
            if (_virtualCamera.Follow != null) return;
            _virtualCamera.Follow = GameObject.FindWithTag("Player").transform;
        }
    }
}
