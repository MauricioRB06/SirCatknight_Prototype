
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Development.Scripts.Mauricio.Managers
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;
        public delegate string LevelChangeDelegate(string sceneName);
        public event LevelChangeDelegate DelegatelevelChange;
        
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
