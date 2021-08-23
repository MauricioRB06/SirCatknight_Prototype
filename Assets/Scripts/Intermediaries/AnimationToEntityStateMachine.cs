
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The purpose of this script is:
//
//  Serve as an intermediary to call functions within the entity.
//  
//  Documentation and References:
//  
//  C# Read only: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/readonly
//  C# Polymorphism: https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/object-oriented/polymorphism

using Enemies.EntityStates.BaseStates;
using UnityEngine;

namespace Intermediaries
{
    public class AnimationToEntityStateMachine : MonoBehaviour
    {
        // Reference to the entity's attack.
        public EntityAttackState AttackState;
        
        // (It is used from the entity animator) Call the TriggerAttack within the entity attack.
        private void TriggerAttack()
        {
            AttackState.TriggerAttack();
        }
        
        // (It is used from the entity animator) Call the FinishAttack within the entity attack.
        private void FinishAttack()
        {
            AttackState.FinishAttack();
        }
    }
}
