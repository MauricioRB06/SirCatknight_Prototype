using Enemies;
using Enemies.Data;
using Enemies.EntityStates.BaseStates;
using StateMachine;

namespace _Development.Scripts.Mauricio.Enemies.EnemyB
{
    public class EnemyBIdleState : EntityIdleState
    {
        // Reference to the entity to which we are going to associate this script.
        private readonly EnemyB enemyB;
        
        // Class constructor.
        public EnemyBIdleState(EntityController entityController, EntityStateMachine entityStateMachine,
            string animationBoolName, DataEntityIdleState dataEntityIdleState, EnemyB enemyB)
            : base(entityController, entityStateMachine, animationBoolName, dataEntityIdleState)
        {
            this.enemyB = enemyB;
        }
        
        // 
        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsPlayerInMinAggroRange)
            {
                EntityStateMachine.ChangeState(enemyB.PlayerDetectionState);
            }
            else if (IsIdleTimeOver)
            {
                EntityStateMachine.ChangeState(enemyB.MoveState);
            }
        }
    }
}
