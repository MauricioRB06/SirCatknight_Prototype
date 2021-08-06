
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The Purpose Of This Script Is:
//
//  Generates a health item, which increases the character's life points when collected.
//
//  Documentation and References:
//
//  Unity Awake: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
//  Unity Start: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html
//  Unity Update: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
//  Unity Coroutines: https://docs.unity3d.com/Manual/Coroutines.html
//  Unity Instantiate: https://docs.unity3d.com/ScriptReference/Object.Instantiate.html
//  Unity Destroy: https://docs.unity3d.com/ScriptReference/Object.Destroy.html
//
//  C# Expression Bodies: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/expression-bodied-members
//  C# Properties: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/properties

using UnityEngine;
    
namespace Levels.General
{
    // Components required for this Script to work.
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(BoxCollider2D))]
    
    public class Door: MonoBehaviour

    {
        [Header("Type of key needed to open the door")] [Space(5)]
        [Tooltip("Select the type of door")]
        [SerializeField] private KeyType keyType;
        //[Space(15)]
        
        // 
        private Animator _doorAnimator;
        private BoxCollider2D _doorCollider;
        
        // 
        private static readonly int OpenTheDoor = Animator.StringToHash("OpenDoor");
        
        // 
        private void Awake()
        {
            _doorAnimator = GetComponent<Animator>();
            _doorCollider= GetComponent<BoxCollider2D>();
        }
        
        // 
        public KeyType GetDoorType()
        {
            return keyType;
        }
        
        // 
        public void OpenDoor()
        {
            _doorAnimator.SetTrigger(OpenTheDoor);
        }
        
        // 
        private void AnimationFinishTrigger()
        {
            Debug.Log("Door is Open");
            _doorCollider.enabled = false;
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}
