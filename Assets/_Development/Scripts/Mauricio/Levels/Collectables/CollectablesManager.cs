using TMPro;
using UnityEngine;

/* The purpose of this Script is:

  Insert Here

  Documentation and References:

  
 
 */

namespace _Development.Scripts.Mauricio.Levels.Collectables
{
    public class CollectablesManager : MonoBehaviour
    {
        public static CollectablesManager Instance;
        
        [SerializeField] private TextMeshProUGUI totalPiecesOfTheCollectible;
        [SerializeField] private TextMeshProUGUI piecesCollectedFromTheCollectible;

        private int _numberTotalPiecesOfTheCollectible;

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
        }
        
        private void Start()
        {
            _numberTotalPiecesOfTheCollectible = transform.childCount;
        }
        
        private void Update()
        {
            totalPiecesOfTheCollectible.text = _numberTotalPiecesOfTheCollectible.ToString();
            piecesCollectedFromTheCollectible.text = (_numberTotalPiecesOfTheCollectible - transform.childCount).ToString();
        }
        
        public void RemainingCollectibles()
        {
            if (transform.childCount == 1)
            {
                Debug.Log("All Items Collectables");
            }
        }
    }
}
