
using System;
using _Development.Scripts.Mauricio.Managers;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace Levels.General
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private Image healthImage;
        [SerializeField] private Image shieldImage;
        
        public static PlayerUI Instance;
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
        
        private string SceneTrigger(string scenename)
        {
            if (scenename == "Credits" || scenename == "MainMenu")
            {
                Destroy(gameObject, 0.1f);
            }
            return scenename;
        }
        
        private float PlayerHealthChange(float healthAmount)
        {
            healthImage.fillAmount = healthAmount / 100;
            return healthAmount;
        }
        
        private float PlayerShieldChange(int shieldAmount)
        {
            shieldImage.fillAmount = shieldAmount switch
            {
                0 => 0f,
                1 => 0.318f,
                2 => 0.658f,
                3 => 1f,
                _ => shieldImage.fillAmount
            };
            
            return shieldAmount;
        }
        
        private void Start()
        {
            LevelManager.Instance.DelegatelevelChange += SceneTrigger;
            PlayerController.Instance.PlayerHealth.DelegateHealthChange += PlayerHealthChange;
            PlayerController.Instance.PlayerHealth.DelegateShieldChange += PlayerShieldChange;
        }

        
        private void OnDisable()
        {
            LevelManager.Instance.DelegatelevelChange -= SceneTrigger;
            PlayerController.Instance.PlayerHealth.DelegateHealthChange -= PlayerHealthChange;
            PlayerController.Instance.PlayerHealth.DelegateShieldChange -= PlayerShieldChange;
        }
    }
}
