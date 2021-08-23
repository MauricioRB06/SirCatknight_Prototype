using Enemies;
using Enemies.Data;
using Enemies.EntityStates.BaseStates;
using StateMachine;

namespace _Development.Scripts.Mauricio.Enemies.EnemyB
{
    public class EnemyBMoveState : EntityMoveState
    {
        // Reference to the entity to which we are going to associate this script.
        private readonly EnemyB enemyB;
        
        // Class constructor.
        public EnemyBMoveState(EntityController entityController, EntityStateMachine entityStateMachine,
            string animationBoolName, DataEntityMoveState dataEntityMoveState, EnemyB enemyB)
            : base(entityController, entityStateMachine, animationBoolName, dataEntityMoveState)
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
            else if(IsDetectingWall || !IsDetectingLedge)
            {
                enemyB.IdleState.SetFlipAfterIdleState(true);
                EntityStateMachine.ChangeState(enemyB.IdleState);
            }
        }
    }
}
