
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The purpose of this script is:
//
//  Generate a behavior for the entity, that allows it to range attack the player.
//  
//  Documentation and References:
//  
//  C# Read only: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/readonly
//  C# Polymorphism: https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/polymorphism

using Enemies;
using SrCatknight.Scripts.Enemies.Data;
using SrCatknight.Scripts.StateMachine;
using UnityEngine;

namespace SrCatknight.Scripts.Enemies.EntityStates.EntityAttackState
{
    public class EntityRangeAttackState : SrCatknight.Scripts.Enemies.EntityStates.BaseStates.EntityAttackState
    {
        // Reference to range attack state data.
        private readonly DataEntityRangedAttackState _dataEntityRangedAttackState;
        
        // 
        protected GameObject Projectile;
        
        // 
        protected EntityProjectile ProjectileScript;
        
        // Class constructor.
        protected EntityRangeAttackState(EntityController entityController, EntityStateMachine entityStateMachine,
            string animationBoolName, Transform attackPosition, DataEntityRangedAttackState dataEntityRangedAttackState)
            : base(entityController, entityStateMachine, animationBoolName, attackPosition)
        {
            this._dataEntityRangedAttackState = dataEntityRangedAttackState;
        }
        
        // 
        public override void TriggerAttack()
        {
            base.TriggerAttack();

            Projectile = Object.Instantiate(_dataEntityRangedAttackState.Projectile, AttackPosition.position,
                AttackPosition.rotation);
            
            ProjectileScript = Projectile.GetComponent<EntityProjectile>();
            
            ProjectileScript.FireProjectile(_dataEntityRangedAttackState.ProjectileSpeed,
                _dataEntityRangedAttackState.ProjectileTravelDistance, _dataEntityRangedAttackState.ProjectileDamage);
        }
    }
}
