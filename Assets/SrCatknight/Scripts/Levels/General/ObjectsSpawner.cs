
using UnityEngine;

namespace SrCatknight.Scripts.Levels.General
{
    public class ObjectsSpawner : MonoBehaviour
    {
        [Range(1.0f, 30.0f)][SerializeField] private float timeToSpawnObject = 1;
        [SerializeField] private GameObject objectToSpawn;
        private float _startTime;
        private void Awake()
        {
            _startTime = Time.time;
            objectToSpawn.SetActive(false);
        }
        
        private void Update()
        {
            if (!(Time.time > _startTime + timeToSpawnObject)) return;
            
            objectToSpawn.SetActive(true);
            Destroy(gameObject);
        }
    }
}
