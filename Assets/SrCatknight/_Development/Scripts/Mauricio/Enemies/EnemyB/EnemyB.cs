using Enemies;
using Enemies.Data;
using SrCatknight.Scripts.Enemies.Data;
using UnityEngine;

namespace _Development.Scripts.Mauricio.Enemies.EnemyB
{
    public class EnemyB : EntityController
    {   
        [Space(15)]
        [Header("Entity Configuration Data")] [Space(5)]
        [Tooltip("Insert an entity idle state data asset here")]
        [SerializeField] private DataEntityIdleState idleStateData;
        [Tooltip("Insert an entity move state data asset here")]
        [SerializeField] private DataEntityMoveState moveStateData;
        [Tooltip("Insert an entity player detected state data asset here")]
        [SerializeField] private DataEntityPlayerDetection playerDetectionStateData;
        [Tooltip("Insert an entity look for player state data asset here")]
        [SerializeField] private DataEntityLookForPlayer lookForPlayerStateData;
        [Tooltip("Insert an entity dodge state data asset here")]
        [SerializeField] private DataEntityDodgeState dodgeStateData;
        [Tooltip("Insert an entity stun state data asset here")]
        [SerializeField] private DataEntityStunState stunStateData;
        [Tooltip("Insert an entity dead state data asset here")]
        [SerializeField] private DataEntityDeadState deadStateData;
        [Space(15)]
        
        [Header("Entity Melee Attack Data")] [Space(5)]
        [Tooltip("Position from where the projectiles will be launched")]
        [SerializeField] private Transform meleeAttackPosition;
        [Tooltip("Insert an entity melee attack state data asset here")]
        [SerializeField] private DataEntityMeleeAttack meleeAttackStateData;
        [Space(15)]
        
        [Header("Entity Ranged Attack Data")] [Space(5)]
        [Tooltip("x")]
        [SerializeField] private Transform rangedAttackPosition;
        [Tooltip("Insert an entity ranged attack state data asset here")]
        [SerializeField] private DataEntityRangedAttackState rangedAttackStateData;
        
        // So that other states can access the dodge state data.
        public DataEntityDodgeState DodgeStateData => dodgeStateData;
        
        // These are the different states that this enemy will have.
        public EnemyBIdleState IdleState { get; private set; }
        public EnemyBMoveState MoveState { get; private set; }
        public EnemyBPlayerDetectionState PlayerDetectionState { get; private set; }
        public EnemyBLookForPlayerState LookForPlayerState { get; private set; }
        public EnemyBDodgeState DodgeState { get; private set; }
        public EnemyBStunState StunState { get; private set; }
        public EnemyBDeadState DeadState { get; private set; }
        public EnemyBMeleeAttackState MeleeAttackState { get; private set; }
        public EnemyBRangeAttackState RangeAttackState { get; private set; }
        
        // Instantiate the objects that will contain the states of this enemy.
        public override void Awake()
        {
            base.Awake();
            
            MoveState = new EnemyBMoveState(this, EntityStateMachine, 
                "Move", moveStateData, this);
            
            IdleState = new EnemyBIdleState(this, EntityStateMachine,
                "Idle", idleStateData, this);
            
            PlayerDetectionState = new EnemyBPlayerDetectionState(this, EntityStateMachine,
                "PlayerDetected", playerDetectionStateData, this);
            
            MeleeAttackState = new EnemyBMeleeAttackState(this, EntityStateMachine,
                "MeleeAttack", meleeAttackPosition, meleeAttackStateData, this);
            
            LookForPlayerState = new EnemyBLookForPlayerState(this, EntityStateMachine,
                "LookForPlayer", lookForPlayerStateData, this);
            
            StunState = new EnemyBStunState(this, EntityStateMachine,
                "Stun", stunStateData, this);
            
            DeadState = new EnemyBDeadState(this, EntityStateMachine,
                "Dead", deadStateData, this);
            
            DodgeState = new EnemyBDodgeState(this, EntityStateMachine,
                "Dodge", dodgeStateData, this);
            
            RangeAttackState = new EnemyBRangeAttackState(this, EntityStateMachine,
                "RangedAttack", rangedAttackPosition, rangedAttackStateData, this);
        }
        
        // After being created, this enemy initializes its state machine.
        private void Start()
        {
            EntityStateMachine.Initialize(MoveState);
        }
        
        // (Delete this in the final build) Reference gizmos to see the parameters of this enemy.
        public override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            
            Gizmos.DrawWireSphere(meleeAttackPosition.position, meleeAttackStateData.AttackRadius);
        }
    }
}
