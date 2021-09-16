using UnityEngine;

namespace SrCatknight.Scripts.Interfaces
{
    public interface IKnockbackableObject
    {
        // 
        void KnockBack(Vector2 angle, float strength, int direction);
    }
}
