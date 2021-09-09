using Enemies;
using Enemies.Data;
using Enemies.EntityStates.BaseStates;
using StateMachine;
using UnityEngine;

namespace _Development.Scripts.Mauricio.Enemies.EnemyB
{
    public class EnemyBPlayerDetectionState : EntityPlayerDetectionState
    {
        // Reference to the entity to which we are going to associate the script.
        private readonly EnemyB enemyB;
        
        // Class constructor.
        public EnemyBPlayerDetectionState(EntityController entityController, EntityStateMachine entityStateMachine,
            string animationBoolName, DataEntityPlayerDetection dataEntityPlayerDetection, EnemyB enemyB)
            : base(entityController, entityStateMachine, animationBoolName, dataEntityPlayerDetection)
        {
            this.enemyB = enemyB;
        }
        
        // 
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            // 
            if (PerformCloseRangeAction)
            {
                // 
                if(Time.time >= enemyB.DodgeState.StartTime + enemyB.DodgeStateData.DodgeCooldown)
                {
                    EntityStateMachine.ChangeState(enemyB.DodgeState);
                }
                // 
                else
                {
                    EntityStateMachine.ChangeState(enemyB.MeleeAttackState);
                }            
            }
            // 
            else if (PerformLongRangeAction)
            {
                EntityStateMachine.ChangeState(enemyB.RangeAttackState);
            }
            // 
            else if (!IsPlayerInMaxAgroRange)
            {
                EntityStateMachine.ChangeState(enemyB.LookForPlayerState);
            }
        }
    }
}
