
using _Development.Scripts.Mauricio.Managers;
using UnityEngine;

namespace Levels.General
{
    public class PlayerUI : MonoBehaviour
    {
        public static PlayerUI Instance;
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
        
        private string SceneTrigger(string scenename)
        {
            Debug.Log($"player loaded in " + scenename);
            if (scenename == "Credits" || scenename == "MainMenu")
            {
                Destroy(gameObject, 0.1f);
            }
            return scenename;
        }
        
        private void OnEnable()
        {
            LevelManager.Instance.DelegatelevelChange += SceneTrigger;
        }
        
        private void OnDisable()
        {
            LevelManager.Instance.DelegatelevelChange -= SceneTrigger;
        }
    }
}
