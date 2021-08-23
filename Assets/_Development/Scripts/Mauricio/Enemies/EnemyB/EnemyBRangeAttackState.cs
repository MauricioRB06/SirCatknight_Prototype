using Enemies;
using Enemies.Data;
using Enemies.EntityStates.EntityAttackState;
using StateMachine;
using UnityEngine;

namespace _Development.Scripts.Mauricio.Enemies.EnemyB
{
    public class EnemyBRangeAttackState : EntityRangeAttackState
    {
        // Reference to the entity to which we are going to associate this script.
        private readonly EnemyB enemyB;
        
        // Class constructor.
        public EnemyBRangeAttackState(EntityController entityController, EntityStateMachine entityStateMachine,
            string animationBoolName, Transform attackPosition,
            DataEntityRangedAttackState dataEntityRangedAttackState, EnemyB enemyB)
            : base(entityController, entityStateMachine, animationBoolName, attackPosition, dataEntityRangedAttackState)
        {
            this.enemyB = enemyB;
        }
        
        // 
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            // 
            if (IsAnimationFinished)
            {
                // 
                if (IsPlayerInMinimumAgroRange)
                {
                    EntityStateMachine.ChangeState(enemyB.PlayerDetectionState);
                }
                // 
                else
                {
                    EntityStateMachine.ChangeState(enemyB.LookForPlayerState);
                }
            }
        }
    }
}
