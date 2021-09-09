
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The Purpose Of This Script Is:
//
//  Allows to generate data assets of the entity's player detected state.
// 
//  Documentation and References:
//
//  Unity ScriptableObject: https://docs.unity3d.com/ScriptReference/ScriptableObject.html
//  Unity Header: https://docs.unity3d.com/ScriptReference/HeaderAttribute.html
//  Unity Tooltip: https://docs.unity3d.com/ScriptReference/TooltipAttribute.html
//  Unity Space: https://docs.unity3d.com/ScriptReference/SpaceAttribute.html
//  Unity SerializeField: https://docs.unity3d.com/2021.1/Documentation/ScriptReference/SerializeField.html
//  Unity Range: https://docs.unity3d.com/ScriptReference/RangeAttribute.html
//  Unity CreateAssetMenu: https://docs.unity3d.com/2021.1/Documentation/ScriptReference/CreateAssetMenuAttribute.html
// 
//  C# Properties: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/properties

using UnityEngine;

namespace Enemies.Data
{
    [CreateAssetMenu(fileName = "newEntityPlayerDetectedStateData",
        menuName = "Sr.Catknight Data/Entity Data/5. Player Detected State", order = 5)]
    
    public class DataEntityPlayerDetection : ScriptableObject
    {
        [Header("Entity Player Detected Settings")] [Space(5)]
        [Tooltip("Waiting time from when the player is detected in the maximum aggro range to change state")]
        [Range(1.0f, 3.0f)][SerializeField] private float longRangeActionTime = 1.5f;
        
        // Expression body properties
        public float LongRangeActionTime => longRangeActionTime;
    }
}
