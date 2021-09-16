
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The purpose of this script is:
//
//   Generate a behavior for the entity, that allows it to die.
//  
//  Documentation and References:
//  
//  C# Read only: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/readonly
//  C# Polymorphism: https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/polymorphism

using Enemies;
using SrCatknight.Scripts.Enemies.Data;
using SrCatknight.Scripts.StateMachine;
using UnityEngine;

namespace SrCatknight.Scripts.Enemies.EntityStates.BaseStates
{
    public class EntityDeadState : EntityState
    {
        // Reference to the dead state data.
        private readonly DataEntityDeadState _dataEntityDeadState;
        
        // Class constructor.
        protected EntityDeadState(EntityController entityController, EntityStateMachine entityStateMachine,
            string animationBoolName, DataEntityDeadState dataEntityDeadState)
            : base(entityController, entityStateMachine, animationBoolName)
        {
            this._dataEntityDeadState = dataEntityDeadState;
        }
        
        // 
        public override void Enter()
        {
            base.Enter();
            
            var entityPosition = EntityController.transform.position;
            
            Object.Instantiate(_dataEntityDeadState.DeathBloodParticle,
                entityPosition, _dataEntityDeadState.DeathBloodParticle.transform.rotation);
            
            Object.Instantiate(_dataEntityDeadState.DeathChunkParticle,
                entityPosition, _dataEntityDeadState.DeathChunkParticle.transform.rotation);
            
            // Change This in final build, this is only for testing, it should be destroyed :v
            EntityController.gameObject.SetActive(false);
        }
    }
}
