using _Development.Scripts.Mauricio;
using _Development.Scripts.Mauricio.Managers;
using UnityEngine;

/* The purpose of this Script is:
 
   Insert Here

  Documentation and References:

  PlayerPrefs: https://docs.unity3d.com/ScriptReference/PlayerPrefs.html
  
 */

namespace Player
{
    public class PlayerRespawn : MonoBehaviour, ISavableObject
    {
        private float _checkPointPositionX, _checkPointPositionY;
        private bool loadNewLevel;
        public int Collectables { get; private set; }
        public void LoadNewLevel() => loadNewLevel = true;
        private GameObject _levelSpawnPoint;
        private Vector3 _levelSpawnPointPosition;

        private void Start()
        {
            _levelSpawnPoint = GameObject.FindGameObjectWithTag("LevelSpawn");
            _levelSpawnPointPosition = _levelSpawnPoint.transform.position;
            
            if (loadNewLevel)
            {
                transform.position = new Vector2(_levelSpawnPointPosition.x, _levelSpawnPointPosition.y);
                loadNewLevel = false;
            }
            else if (PlayerPrefs.GetFloat("_positionLastCheckPointX") != 0)
            {
                transform.position = new Vector2(PlayerPrefs.GetFloat("_positionLastCheckPointX"),
                    PlayerPrefs.GetFloat("_positionLastCheckPointY"));
            }
        }
        
        // 
        public static void ReachedCheckpoint(float x, float y)
        {
            PlayerPrefs.SetFloat("_positionLastCheckPointX", x);
            PlayerPrefs.SetFloat("_positionLastCheckPointY", y);
        }

        /*public void LoadNewLevel(float x, float y)
        {
            transform.position = new Vector2(x, y);
        }*/
        
        private void Update()
        {
            if (!loadNewLevel) return;
            
            _levelSpawnPoint = GameObject.FindGameObjectWithTag("LevelSpawn");
            _levelSpawnPointPosition = _levelSpawnPoint.transform.position;
            transform.position = new Vector2(_levelSpawnPointPosition.x, _levelSpawnPointPosition.y);
            loadNewLevel = false;
        }
        
        public void Save(int saveSlot)
        {
            var json = JsonUtility.ToJson(transform.position);
            PlayerPrefs.SetString(saveSlot + "PlayerPosition", json);
            PlayerPrefs.SetInt(saveSlot + "PlayerCollectables",Collectables);
        }
        
        public void Load(int saveSlot)
        {
            transform.position = JsonUtility.FromJson<Vector3>(PlayerPrefs.GetString(saveSlot + "PlayerPosition"));
            Collectables = PlayerPrefs.GetInt(saveSlot + "PlayerCollectables");
        }
    }
}
