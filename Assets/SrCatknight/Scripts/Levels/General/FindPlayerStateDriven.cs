
using Cinemachine;
using UnityEngine;

namespace SrCatknight.Scripts.Levels.General
{
    public class FindPlayerStateDriven : MonoBehaviour
    {
        private CinemachineStateDrivenCamera _stateDrivenCamera;

        private void Awake()
        {
            _stateDrivenCamera = GetComponent<CinemachineStateDrivenCamera>();
        }
        
        private void Update()
        {
            if (_stateDrivenCamera.Follow != null) return;
            _stateDrivenCamera.Follow = GameObject.FindWithTag("Player").transform;
        }
    }
}
