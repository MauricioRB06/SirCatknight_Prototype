
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The purpose of this script is:
//
//  Generate a behavior for the entity, that allows it to melee attack the player.
//  
//  Documentation and References:
//  
//  C# Read only: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/readonly
//  C# Polymorphism: https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/polymorphism

using Enemies.Data;
using Interfaces;
using StateMachine;
using UnityEngine;

namespace Enemies.EntityStates.EntityAttackState
{
    public class EntityMeleeAttackState : BaseStates.EntityAttackState
    {
        // Reference to melee attack state data.
        private readonly DataEntityMeleeAttack dataEntityMeleeAttack;
        
        // Class constructor.
        protected EntityMeleeAttackState(EntityController entityController, EntityStateMachine entityStateMachine,
            string animationBoolName, Transform attackPosition, DataEntityMeleeAttack dataEntityMeleeAttack)
            : base(entityController, entityStateMachine, animationBoolName, attackPosition)
        {
            this.dataEntityMeleeAttack = dataEntityMeleeAttack;
        }
        
        // When the entity initiates the attack.
        public override void TriggerAttack()
        {
            base.TriggerAttack();
            
            // Stores all objects that were detected by the collider when attacking.
            var detectedObjects = Physics2D.OverlapCircleAll(AttackPosition.position,
                dataEntityMeleeAttack.AttackRadius, dataEntityMeleeAttack.LayerPlayer);
            
            // Scroll through all the objects detected in the collider of the attack.
            foreach (var collider in detectedObjects)
            {
                // If it detects within the object a component that may receive damage.
                var damageable = collider.GetComponent<IDamageableObject>();
                damageable?.TakeDamage(dataEntityMeleeAttack.AttackDamage);
                
                // If it detects within the object a component that is knockbackable.
                var knockbackable = collider.GetComponent<IKnockbackableObject>();
                knockbackable?.KnockBack(dataEntityMeleeAttack.KnockbackAngle,
                    dataEntityMeleeAttack.KnockbackStrength, Core.Movement.FacingDirection);
            }
        }
    }
}
