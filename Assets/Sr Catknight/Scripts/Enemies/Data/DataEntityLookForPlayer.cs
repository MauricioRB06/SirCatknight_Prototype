
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The Purpose Of This Script Is:
//
//  Allows to generate data assets of the entity's look for player state.
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

namespace Enemies.Data
{
    [CreateAssetMenu(fileName = "newEntityLookForPlayerStateData",
        menuName = "Sr.Catknight Data/Entity Data/6. Look For Player State", order = 6)]
    
    public class DataEntityLookForPlayer : ScriptableObject
    {
        [Header("Entity State Data Settings: Look For Player")] [Space(5)]
        [Tooltip("The number of times the entity will turn to look for the player")]
        [Range(2.0f, 4.0f)][SerializeField] private int amountOfTurns = 2;
        [Tooltip("Time between each turn, when looking for the player")]
        [Range(0.5f, 1.0f)][SerializeField] private float timeBetweenTurns = 0.75f;
        
        // Expression body properties
        public int AmountOfTurns => amountOfTurns;
        public float TimeBetweenTurns => timeBetweenTurns;
    }
}
