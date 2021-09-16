using Player;
using SrCatknight.Scripts.Managers;
using SrCatknight.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

namespace SrCatknight.Scripts.Levels.General
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private Image healthImage;
        [SerializeField] private Image shieldImage;
        
        // 
        public static PlayerUI Instance;
        
        // 
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                shieldImage.fillAmount = 0;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        // 
        private void OnEnable()
        {
            LevelManager.LevelChangeDelegate += SceneChange;
            PlayerHealth.PlayerHealthDelegate += Player;
            PlayerHealth.PlayerShieldDelegate += Player;
        }
        
        // 
        private void SceneChange(string sceneName)
        {
            if (sceneName == "Credits" || sceneName == "MainMenu")
            {
                Destroy(gameObject, 0.1f);
            }
        }
        
        // 
        private void Player(float healthAmount)
        {
            healthImage.fillAmount = healthAmount / 100;
        }
        
        // 
        private void Player(int shieldAmount)
        {
            shieldImage.fillAmount = shieldAmount switch
            {
                0 => 0f,
                1 => 0.318f,
                2 => 0.658f,
                3 => 1f,
                _ => shieldImage.fillAmount
            };
        }
        
        // 
        private void OnDisable()
        {
            LevelManager.LevelChangeDelegate -= SceneChange;
            PlayerHealth.PlayerHealthDelegate -= Player;
            PlayerHealth.PlayerShieldDelegate -= Player;
        }
    }
}
