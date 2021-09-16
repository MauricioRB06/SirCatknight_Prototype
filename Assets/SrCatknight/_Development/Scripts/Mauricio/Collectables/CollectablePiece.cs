using UnityEngine;

/* The purpose of this Script is:

  Insert Here

  Documentation and References:

  GetChild: https://docs.unity3d.com/ScriptReference/Transform.GetChild.html
  Destroy: https://docs.unity3d.com/ScriptReference/Object.Destroy.html
 
 */

namespace _Development.Scripts.Mauricio.Levels.Collectables
{
    public class CollectablePiece : MonoBehaviour
    { 
        [Header("Name To Identify This Collectible")][Space(5)]
        [Tooltip("Uses 2 characters, the level number and a letter in alphabetical order")]
        [SerializeField] private string collectableID;
        [Space(15)]
        
        [Header("SFX When Pickup This Collectible")][Space(5)]
        [Tooltip("Put An AudioSource Prefab here")]
        [SerializeField] private GameObject collectableSfx;
        [Space(15)]
        
        [Header("VFX When Pickup This Collectible")] [Space(5)]
        [Tooltip("Put An Particles Prefab here")]
        [SerializeField] private GameObject collectableParticles;
        
        [SerializeField]
            //private string itemName;
            private CollectableItemSet collectableItemSet;
            private UniqueID uniqueID;
            
        //public string CollectablePickedUpKey => $"Collectable_{collectableID}_PikedUp";
        //public CollectablesData CollectableData { get; private set; } = new CollectablesData();

        private void Start()
        {
            uniqueID = GetComponent<UniqueID>();
            collectableItemSet = FindObjectOfType<CollectableItemSet>();
            if (collectableItemSet.CollectedItems.Contains(uniqueID.ID))
            {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            
            collectableItemSet.CollectedItems.Add(uniqueID.ID);
            GetComponent<SpriteRenderer>().enabled = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            FindObjectOfType<CollectablesManager>().RemainingCollectibles();
            gameObject.SetActive(false);
            //CollectableData.wasPickedUp = true;

            //AudioManager.PlaySound(collectableSfx);
            //Destroy(gameObject, 0.2f);
        }
/*
        public void Load(CollectablesData collectableData)
        {
            CollectableData = collectableData;
            gameObject.SetActive(!CollectableData.wasPickedUp);
            
            //var wasPickedUp = PlayerPrefs.GetInt(saveSlot + CollectablePickedUpKey);
            //GetComponent<SpriteRenderer>().enabled = wasPickedUp == 0;
        }*/
    }
}
