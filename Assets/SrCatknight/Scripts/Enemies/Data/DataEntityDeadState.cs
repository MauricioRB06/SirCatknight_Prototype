
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The Purpose Of This Script Is:
//
//  Allows to generate data assets of the entity's dead state.
// 
//  Documentation and References:
//
//  Unity ScriptableObject: https://docs.unity3d.com/ScriptReference/ScriptableObject.html
//  Unity Header: https://docs.unity3d.com/ScriptReference/HeaderAttribute.html
//  Unity Tooltip: https://docs.unity3d.com/ScriptReference/TooltipAttribute.html
//  Unity Space: https://docs.unity3d.com/ScriptReference/SpaceAttribute.html
//  Unity SerializeField: https://docs.unity3d.com/2021.1/Documentation/ScriptReference/SerializeField.html
//  Unity CreateAssetMenu: https://docs.unity3d.com/2021.1/Documentation/ScriptReference/CreateAssetMenuAttribute.html
// 
//  C# Properties: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/properties

using UnityEngine;

namespace SrCatknight.Scripts.Enemies.Data
{
    [CreateAssetMenu(fileName = "newEntityDeadStateData",
        menuName = "Sr.Catknight Data/Entity Data/4. Dead State", order = 4)]
    
    public class DataEntityDeadState : ScriptableObject
    {
        [Header("Entity Dead State Settings")] [Space(5)]
        [Tooltip("x")]
        [SerializeField] private GameObject deathChunkParticle;
        [Tooltip("x")]
        [SerializeField] private GameObject deathBloodParticle;
        
        // Expression body properties.
        public GameObject DeathChunkParticle => deathChunkParticle;
        public GameObject DeathBloodParticle => deathBloodParticle;
    }
}
