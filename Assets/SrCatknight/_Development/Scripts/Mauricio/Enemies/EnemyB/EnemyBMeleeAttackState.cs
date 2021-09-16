using Enemies;
using Enemies.Data;
using SrCatknight.Scripts.Enemies.Data;
using SrCatknight.Scripts.Enemies.EntityStates.EntityAttackState;
using SrCatknight.Scripts.StateMachine;
using UnityEngine;

namespace _Development.Scripts.Mauricio.Enemies.EnemyB
{
    public class EnemyBMeleeAttackState : EntityMeleeAttackState
    {
        private readonly EnemyB enemyB;
        public EnemyBMeleeAttackState(EntityController entity, EntityStateMachine stateMachine,
            string animationBoolName, Transform attackPosition, DataEntityMeleeAttack stateData, EnemyB enemyB)
            : base(entity, stateMachine, animationBoolName, attackPosition, stateData)
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
                else if (!IsPlayerInMinimumAgroRange)
                {
                    EntityStateMachine.ChangeState(enemyB.LookForPlayerState);
                }
            }
        }
    }
}
