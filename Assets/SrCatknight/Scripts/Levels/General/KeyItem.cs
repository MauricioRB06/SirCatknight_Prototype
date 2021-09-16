
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

namespace SrCatknight.Scripts.Levels.General
{
    // Type of keys available.
    public enum KeyType
    {
        TutorialKey,
        ChocolateKey,
        CandyKey,
        SakuraKey,
        RockKey,
        CrystalKey,
        DarkKey,
        SecretKey
    }
    
    // Components required for this Script to work.
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PolygonCollider2D))]
    
    public class KeyItem: MonoBehaviour
    {
        [Header("Key Settings")] [Space(5)]
        [Tooltip("Select the type of key")]
        [SerializeField] private KeyType keyType;
        [Space(15)]
        
        [Header("SFX Settings")] [Space(5)]
        [Tooltip("Insert an AudioPrefab here")]
        [SerializeField] private GameObject sfxPickupKey;
        
        // To control the collection of the key.
        private SpriteRenderer _keySpriteRenderer;
        private PolygonCollider2D _keyCollider;
        
        // Check that the necessary components are properly configured.
        private void Awake()
        {
            _keySpriteRenderer = GetComponent<SpriteRenderer>();
            _keyCollider = GetComponent<PolygonCollider2D>();
            
            if (sfxPickupKey == null)
            {
                Debug.LogError("<color=#D22323><b>" +
                               $"The sfx of the {keyType.ToString()} cannot be empty, please add one," +
                               " the key will be deactivated</b></color>");
                _keyCollider.enabled = false;
            }
            else
            {
                if (!_keyCollider.enabled) _keyCollider.enabled = true;
                if (!_keyCollider.isTrigger) _keyCollider.isTrigger = true;
            }
        }
        
        //
        public KeyType GetKeyType()
        {
            return keyType;
        }
        
        //
        public void PickUpKey()
        {
            var keyTransform = transform;
            var keyPosition = keyTransform.position;

            _keySpriteRenderer.enabled = false;
            _keyCollider.enabled = false;
            Instantiate(sfxPickupKey, keyPosition, Quaternion.identity, keyTransform);
            Destroy(gameObject, 0.4f);
        }
    }
}
