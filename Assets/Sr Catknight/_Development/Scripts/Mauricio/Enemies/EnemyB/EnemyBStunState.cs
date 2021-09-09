using Enemies;
using Enemies.Data;
using Enemies.EntityStates.BaseStates;
using StateMachine;

namespace _Development.Scripts.Mauricio.Enemies.EnemyB
{
    public class EnemyBStunState : EntityStunState
    {
        // Reference to the entity to which we are going to associate the script.
        private readonly _Development.Scripts.Mauricio.Enemies.EnemyB.EnemyB enemyB;
        
        // Class constructor.
        public EnemyBStunState(EntityController entityController, EntityStateMachine entityStateMachine,
            string animationBoolName, DataEntityStunState dataEntityStunState, _Development.Scripts.Mauricio.Enemies.EnemyB.EnemyB enemyB)
            : base(entityController, entityStateMachine, animationBoolName, dataEntityStunState)
        {
            this.enemyB = enemyB;
        }
        
        // 
        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (IsStunTimeOver)
            {
                if (IsPlayerInMinAgroRange)
                {
                    EntityStateMachine.ChangeState(enemyB.PlayerDetectionState);
                }
                else
                {
                    EntityStateMachine.ChangeState(enemyB.LookForPlayerState);
                }
            }
        }
    }
}
