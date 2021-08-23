using Interfaces;
using UnityEngine;

namespace Core.CoreComponents
{
    public class Combat : CoreComponent, IDamageableObject, IKnockbackableObject
    {
        [SerializeField] private float maxKnockbackTime = 0.2f;
        
        // 
        private bool isKnockBackActive;
        
        // 
        private float knockBackStartTime;
        
        // 
        public void LogicUpdate()
        {
            CheckKnockBack();
        }
        
        // 
        public void TakeDamage(float amount)
        {
            Debug.Log(Core.transform.parent.name + " Damaged!");
        }
        
        // 
        public void KnockBack(Vector2 angle, float strength, int direction)
        {
            Core.Movement.SetVelocity(strength, angle, direction);
            Core.Movement.CanSetVelocity = false;
            isKnockBackActive = true;
            knockBackStartTime = Time.time;
        }
        
        // 
        private void CheckKnockBack()
        {
            if (isKnockBackActive && Core.Movement.CurrentVelocity.y <= 0.01f &&
                (Core.CollisionSenses.Ground || Time.time >= knockBackStartTime + maxKnockbackTime))
            {
                isKnockBackActive = false;
                Core.Movement.CanSetVelocity = true;
            }
        }
    }
}
