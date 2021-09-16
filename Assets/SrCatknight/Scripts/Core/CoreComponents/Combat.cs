using SrCatknight.Scripts.Interfaces;
using UnityEngine;

namespace SrCatknight.Scripts.Core.CoreComponents
{
    public class Combat : CoreComponent, IDamageableObject, IKnockbackableObject
    {
        [SerializeField] private float maxKnockbackTime = 0.2f;
        
        // 
        private bool _isKnockbackActive;
        
        // 
        private float _knockbackStartTime;
        
        // 
        public void PhysicsUpdate()
        {
            CheckKnockBack();
        }
        
        // 
        public void TakeDamage(float damageAmount)
        {
            
        }
        
        // 
        public void KnockBack(Vector2 angle, float strength, int direction)
        {
            Core.Movement.SetVelocity(strength, angle, direction);
            Core.Movement.CanSetVelocity = false;
            _isKnockbackActive = true;
            _knockbackStartTime = Time.time;
        }
        
        // 
        private void CheckKnockBack()
        {
            if (_isKnockbackActive && Core.Movement.CurrentVelocity.y <= 0.01f &&
                (Core.CollisionSenses.Ground || Time.time >= _knockbackStartTime + maxKnockbackTime))
            {
                _isKnockbackActive = false;
                Core.Movement.CanSetVelocity = true;
            }
        }
    }
}
