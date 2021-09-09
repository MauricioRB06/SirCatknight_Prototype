
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The Purpose Of This Script Is:
//
//  Generates a shield item, which increases the character's defense points when collected.
//
//  Documentation and References:
//
//  Unity Awake: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
//  Unity Update: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
//  Unity OnCollisionEnter2D: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnCollisionEnter2D.html
//  Unity CompareTag: https://docs.unity3d.com/ScriptReference/Component.CompareTag.html
//  Unity Instantiate: https://docs.unity3d.com/ScriptReference/Object.Instantiate.html
//  Unity Destroy: https://docs.unity3d.com/ScriptReference/Object.Destroy.html.

using Player;
using UnityEngine;

namespace Levels.General
{
    // Components required for this Script to work
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(CircleCollider2D))]
    
    public class ShieldItem: MonoBehaviour
    {
        [Header("Shield Settings")][Space(5)]
        [Tooltip("Amount of shielding the player will earn")]
        [Range(1.0f, 3.0f)][SerializeField] private int shieldAmountToGive = 1;
        [Space(15)]
        
        [Header("SFX Settings")][Space(5)]
        [Tooltip("Insert here an AudioPrefab")]
        [SerializeField] private GameObject sfxItemPickUp;
        
        // To control the behavior of the collider.
        private CircleCollider2D _shieldItemCollider;
        
        // Set the initial settings for the item.
        private void Awake()
        {
            _shieldItemCollider = GetComponent<CircleCollider2D>();
            
            if (!_shieldItemCollider.isTrigger) _shieldItemCollider.isTrigger = true;
            
            if (sfxItemPickUp == null)
            {
                Debug.LogError("<color=#D22323><b>" +
                               "The sound effect of the shield item is empty, please add one.</b></color>");
            }
        }
        
        // Check if he collided with the player, to increase his shield points.
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            
            var shieldItemTransform = transform;
            var shieldItemPosition = shieldItemTransform.position;
            
            shieldItemTransform.GetComponent<SpriteRenderer>().enabled = false;
            _shieldItemCollider.enabled = false;
            
            Instantiate(sfxItemPickUp, shieldItemPosition, Quaternion.identity, shieldItemTransform);

            collision.GetComponent<PlayerHealth>().TakeShield(shieldAmountToGive);
            Destroy(gameObject, 0.5f);
        }
    }
}
