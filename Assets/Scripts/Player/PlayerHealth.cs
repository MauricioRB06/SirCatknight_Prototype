using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float healthAmount;
        [SerializeField] private int shieldAmount;
        [SerializeField] private float maxHealth;
        [SerializeField] private Image healthImage;
        [SerializeField] private Image shieldImage;
        
        private void Start()
        {
            healthAmount = maxHealth;
        }
        
        private void Update()
        {
            healthImage.fillAmount = healthAmount / 100;

            shieldImage.fillAmount = shieldAmount switch
            {
                0 => 0f,
                1 => 0.318f,
                2 => 0.658f,
                3 => 1f,
                _ => shieldImage.fillAmount
            };

            if (healthAmount > maxHealth) healthAmount = maxHealth;

            if (!(healthAmount <= 0)) return;
            
            healthAmount = 0;
            Debug.Log("Player is Death :(");
        }

        public void TakeDamage (float damageAmount)
        {
            if (shieldAmount > 0)
            {
                if (damageAmount <= 10)
                {
                    shieldAmount--;
                }
                else if (damageAmount < 30)
                {
                    shieldAmount -= 2;
                    if (shieldAmount >= 0) return;
                    
                    shieldAmount = 0;
                    healthAmount -= 10;
                }
                else
                {
                    shieldAmount -= 3;
                    
                    if (shieldAmount >= 0)
                    {
                        if (shieldAmount == -1)
                        {
                            healthAmount -= 10;
                        }
                        else
                        {
                            healthAmount -= 20; 
                        }
                    }
                    
                    shieldAmount = 0;
                }
            }
            else
            {
                healthAmount -= damageAmount;
            }
        }
        
        public void CureHealth (float healthToGive)
        {
            healthAmount += healthToGive;
        }

        public void TakeShield(int shieldValue)
        {
            shieldAmount += shieldValue;
        }

        public void ChangeMaxHealth (float newMaxHealth)
        {
            maxHealth = newMaxHealth;
        }
    }
}
