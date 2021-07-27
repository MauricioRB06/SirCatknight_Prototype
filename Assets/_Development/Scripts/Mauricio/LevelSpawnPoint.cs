using Player;
using UnityEngine;

namespace _Development.Scripts.Mauricio
{
    public class LevelSpawnPoint : MonoBehaviour
    {
        private GameObject player;
        private bool _isNewLevelLoading;
        
        private void Start()
        {
            _isNewLevelLoading = true;
        }
        
        private void Update()
        {
            if (!_isNewLevelLoading) return;
            
            player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerRespawn>().LoadNewLevel();
            _isNewLevelLoading = false;
        }
    }
}
