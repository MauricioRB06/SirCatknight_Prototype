using UnityEngine;

namespace Player
{
    public class PlayerSounds : MonoBehaviour
    {
        [SerializeField] private AudioSource playerJump;
        [SerializeField] private AudioSource playerRun;
        
        public AudioSource PlayerJump => playerJump;
        public AudioSource PlayerRun => playerRun;
    }
}
