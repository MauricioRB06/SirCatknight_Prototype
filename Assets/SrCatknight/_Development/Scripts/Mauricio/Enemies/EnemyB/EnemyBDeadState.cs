using Enemies;
using Enemies.Data;
using SrCatknight.Scripts.Enemies.Data;
using SrCatknight.Scripts.Enemies.EntityStates.BaseStates;
using SrCatknight.Scripts.StateMachine;

namespace _Development.Scripts.Mauricio.Enemies.EnemyB
{
    public class EnemyBDeadState : EntityDeadState
    {
        // Reference to the entity to which we are going to associate the script.
        private readonly _Development.Scripts.Mauricio.Enemies.EnemyB.EnemyB enemyB;
        
        // Class constructor.
        public EnemyBDeadState(EntityController entityController, EntityStateMachine entityStateMachine,
            string animationBoolName, DataEntityDeadState dataEntityDeadState, _Development.Scripts.Mauricio.Enemies.EnemyB.EnemyB enemyB)
            : base(entityController, entityStateMachine, animationBoolName, dataEntityDeadState)
        {
            this.enemyB = enemyB;
        }

        protected override void DoChecks()
        {
            base.DoChecks();
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }
    }
}
