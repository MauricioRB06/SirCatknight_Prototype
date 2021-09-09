
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

using System.Collections.Generic;
using Levels.General;
using UnityEngine;
using Weapons;

// The purpose of this Script is:
/* At the moment it will store the player's weapon, but will be expanded in the future */

namespace Player.Inventory
{
    public class PlayerInventory : MonoBehaviour
    {
        // 
        public Weapon[] weapons;
        
        // 
        [SerializeField] private List<KeyType> keyList;
        
        // 
        private void Awake()
        {
            keyList = new List<KeyType>();
        }
        
        // 
        private void AddKey(KeyType keyType)
        {
            Debug.Log($"Pickup key: {keyType}");
            keyList.Add(keyType);
        }
        
        // 
        private void RemoveKey(KeyType keyType)
        {
            keyList.Remove(keyType);
        }
        
        // 
        private bool CheckKey(KeyType keyType)
        {
            return keyList.Contains(keyType);
        }
        
        // 
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var key = collision.GetComponent<KeyItem>();
            if (key != null)
            {
                AddKey(key.GetKeyType());
                key.PickUpKey();
            }

            var door = collision.GetComponent<Door>();
            if (door != null)
            {
                if (CheckKey(door.GetDoorType()))
                {
                    RemoveKey(door.GetDoorType());
                    door.OpenDoor();
                }
            }
        }
    }
}
