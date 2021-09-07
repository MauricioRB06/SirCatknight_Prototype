using Player;
using UnityEngine;

namespace Levels.General
{
    public class LevelSpawnPoint : MonoBehaviour
    {
        private GameObject _player;
        private bool _isNewLevelLoading;
        
        private void Start()
        {
            _isNewLevelLoading = true;
        }
        
        private void Update()
        {
            if (!_isNewLevelLoading) return;
            
            _player = GameObject.FindGameObjectWithTag("Player");
            _player.GetComponent<PlayerRespawn>().LoadNewLevel();
            _isNewLevelLoading = false;
        }
    }
}
