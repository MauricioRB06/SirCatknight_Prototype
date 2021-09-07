
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Development.Scripts.Mauricio.Managers
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;
        public delegate string LevelChangeDelegate(string sceneName);
        public event LevelChangeDelegate DelegatelevelChange;
        
        [SerializeField] private GameObject loadingCanvas;
        [SerializeField] private Image progressBar;
        private float _target;
        
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
        
        // 
        public string ChangeLevel(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
            DelegatelevelChange?.Invoke(sceneName);
            return sceneName;
        }
    }
}
