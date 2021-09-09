
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public delegate float HealtChangeDelegate(float sceneName);
        public event HealtChangeDelegate DelegateHealthChange;
        
        
        public delegate float ShieldChangeDelegate(int sceneName);
        public event ShieldChangeDelegate DelegateShieldChange;
        
        
        
        [SerializeField] private float healthAmount;
        [SerializeField] private int shieldAmount;
        [SerializeField] private float maxHealth;
        
        private void Start()
        {
            healthAmount = maxHealth;
        }
        
        private void Update()
        {
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
            
            DelegateHealthChange?.Invoke(healthAmount);
            DelegateShieldChange?.Invoke(shieldAmount);
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
