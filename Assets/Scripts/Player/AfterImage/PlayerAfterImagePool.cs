using System.Collections.Generic;
using UnityEngine;

/* Documentation:
 * 
 * Queue: https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1?view=net-5.0
 * Enqueue: https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1.enqueue?view=net-5.0
 * Dequeue: https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.queue-1.dequeue?view=net-5.0
 * Unity Object Pooling: https://www.youtube.com/watch?v=7UswSdevSpw
 * Singleton C#: https://www.youtube.com/watch?v=K902i_tsXl0&ab_channel=hdeleon.net
 * Instantiate: https://docs.unity3d.com/ScriptReference/Object.Instantiate.html
 * 
 */

namespace Player.AfterImage
{
    public class PlayerAfterImagePool : MonoBehaviour
    {
        [SerializeField]
        // We use it to store the reference to the prefab that we use for the AfterImage
        private GameObject afterImagePrefab;
        
        // We use it to store all the objects we have created, which are not currently active
        private readonly Queue<GameObject> _availableObjects = new Queue<GameObject>();
        
        // We use this singleton to access this script from other scripts
        public static PlayerAfterImagePool Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            
            GrowPool();
        }
        
        // We use them to create game objects for the group, we will only create a maximum of 10 objects
        private void GrowPool()
        {
            for (var i = 0; i < 10; i++)
            {
                var instanceToAdd = Instantiate(afterImagePrefab, transform, true);
                AddToPool(instanceToAdd);
            }
        }
        
        // we use it to hide the objects and add them to the queue
        public void AddToPool(GameObject objectInstance)
        {
            objectInstance.SetActive(false);
            _availableObjects.Enqueue(objectInstance);
        }
        
        // We use it to get an object from the group
        public GameObject GetFromPool()
        {
            // If we try to get an object and the queue is empty, we create new objects to fill it
            if(_availableObjects.Count == 0) GrowPool();

            var objectInstance = _availableObjects.Dequeue();
            objectInstance.SetActive(true);
            return objectInstance;
        }
    }
}
