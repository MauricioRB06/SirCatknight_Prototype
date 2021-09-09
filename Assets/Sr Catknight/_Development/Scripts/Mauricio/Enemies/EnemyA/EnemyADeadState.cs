
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The purpose of this script is:
//
//  Set the actions that EnemyA will take, when it's dying.
// 
//  Documentation and References:
//
//  C# Read only: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/readonly
//  C# Polymorphism: https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/polymorphism

using Enemies;
using Enemies.Data;
using Enemies.EntityStates.BaseStates;
using StateMachine;

namespace _Development.Scripts.Mauricio.Enemies.EnemyA
{
    public class EnemyADeadState : EntityDeadState
    {
        // Reference to the entity to which we are going to associate the script.
        private readonly EnemyA enemyA;

        public EnemyADeadState(EntityController entityController, EntityStateMachine entityStateMachine,
            string animationBoolName, DataEntityDeadState dataEntityDeadState, EnemyA enemyA)
            : base(entityController, entityStateMachine, animationBoolName, dataEntityDeadState)
        {
            this.enemyA = enemyA;
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
