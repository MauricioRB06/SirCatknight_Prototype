using Enemies;
using Enemies.Data;
using SrCatknight.Scripts.Enemies.Data;
using SrCatknight.Scripts.Enemies.EntityStates.BaseStates;
using SrCatknight.Scripts.StateMachine;

namespace _Development.Scripts.Mauricio.Enemies.EnemyB
{
    public class EnemyBLookForPlayerState : EntityLookForPlayerState
    {
        // Reference to the entity to which we are going to associate the script.
        private readonly EnemyB enemyB;
        
        // Class constructor.
        public EnemyBLookForPlayerState(EntityController entityController, EntityStateMachine entityStateMachine,
            string animationBoolName, DataEntityLookForPlayer dataEntityLookForPlayer, EnemyB enemyB)
            : base(entityController, entityStateMachine, animationBoolName, dataEntityLookForPlayer)
        {
            this.enemyB = enemyB;
        }
        
        // 
        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            // 
            if (IsPlayerInMinAgroRange)
            {
                EntityStateMachine.ChangeState(enemyB.PlayerDetectionState);
            }
            //
            else if (IsAllTurnsTimeDone)
            {
                EntityStateMachine.ChangeState(enemyB.MoveState);
            }
        }
    }
}
