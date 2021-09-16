using Enemies;
using Enemies.Data;
using SrCatknight.Scripts.Enemies.Data;
using SrCatknight.Scripts.Enemies.EntityStates.BaseStates;
using SrCatknight.Scripts.StateMachine;

namespace _Development.Scripts.Mauricio.Enemies.EnemyB
{
    public class EnemyBDodgeState : EntityDodgeState
    {
        // Reference to the entity to which we are going to associate the script.
        private readonly EnemyB enemyB;
        
        // Class constructor.
        public EnemyBDodgeState(EntityController entityController, EntityStateMachine entityStateMachine,
            string animationBoolName, DataEntityDodgeState dataEntityDodgeState, EnemyB enemyB)
            : base(entityController, entityStateMachine, animationBoolName, dataEntityDodgeState)
        {
            this.enemyB = enemyB;
        }
        
        // 
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            // 
            if (IsDodgeOver)
            {
                // 
                if (IsPlayerInMaxAgroRange && PerformCloseRangeAction)
                {
                    EntityStateMachine.ChangeState(enemyB.MeleeAttackState);
                }
                // 
                else if (IsPlayerInMaxAgroRange && !PerformCloseRangeAction)
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
}
