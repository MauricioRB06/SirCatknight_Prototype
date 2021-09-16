using System;
using UnityEngine;

namespace SrCatknight.Scripts.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float healthAmount;
        [SerializeField] private int shieldAmount;
        [SerializeField] private float maxHealth;

        public static event Action<float> PlayerHealthDelegate;
        public static event Action<int> PlayerShieldDelegate;

        // 
        private void Start()
        {
            healthAmount = maxHealth;
        }
        
        // 
        private void Update()
        {
            if (healthAmount > maxHealth) healthAmount = maxHealth;

            if (!(healthAmount <= 0)) return;
            
            healthAmount = 0;
            Debug.Log("Player is Death :(");
        }

        // 
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
            
            PlayerHealthDelegate?.Invoke(healthAmount);
            PlayerShieldDelegate?.Invoke(shieldAmount);
        }
        
        // 
        public void CureHealth (float healthToGive)
        {
            healthAmount += healthToGive;
            PlayerHealthDelegate?.Invoke(healthAmount);
        }

        // 
        public void TakeShield(int shieldValue)
        {
            shieldAmount += shieldValue;
            PlayerShieldDelegate?.Invoke(shieldAmount);
        }

        // 
        public void ChangeMaxHealth (float newMaxHealth)
        {
            maxHealth = newMaxHealth;
        }
    }
}
