
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
//  Unity Update: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
//  Unity OnCollisionEnter2D: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnCollisionEnter2D.html
//  Unity CompareTag: https://docs.unity3d.com/ScriptReference/Component.CompareTag.html
//  Unity Instantiate: https://docs.unity3d.com/ScriptReference/Object.Instantiate.html
//  Unity Destroy: https://docs.unity3d.com/ScriptReference/Object.Destroy.html

using Player;
using UnityEngine;

namespace Levels.General
{
    // Components required for this Script to work
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CircleCollider2D))]
    
    public class HealthItem : MonoBehaviour
    {
        [Header("Health Item Settings")][Space(5)]
        [Tooltip("Amount of life the player will gain")]
        [Range(1.0f, 99.0f)][SerializeField] private float healthToGive = 5;
        [Space(15)]
        
        [Header("VFX Settings")][Space(5)]
        [Tooltip("Insert an ParticlesPrefab here ")]
        [SerializeField] private GameObject vfxItemPickUp;
        [Space(15)]
        
        [Header("SFX Settings")][Space(5)]
        [Tooltip("Insert an AudioPrefab here ")]
        [SerializeField] private GameObject sfxItemPickUp;
        
        // To control the behavior of the collider.
        private CircleCollider2D _healthItemCollider;
        
        // Set the initial settings for the item.
        private void Awake()
        {
            _healthItemCollider = GetComponent<CircleCollider2D>();
            
            if (!_healthItemCollider.isTrigger) _healthItemCollider.isTrigger = true;
            
            if (vfxItemPickUp == null)
            {
                Debug.LogError(
                    "<color=#D22323><b>The particles effect of the health item is empty, please add one.</b></color>");
            }
            
            if (sfxItemPickUp == null)
            {
                Debug.LogError(
                    "<color=#D22323><b>The sound effect of the health item is empty, please add one.</b></color>");
            }
        }
        
        // Check if he collided with the player, to increase his life points.
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;

            var healthItemTransform = transform;
            var healthItemPosition = healthItemTransform.position;
            
            healthItemTransform.GetComponent<SpriteRenderer>().enabled = false;
            _healthItemCollider.enabled = false;
            
            Instantiate(sfxItemPickUp, healthItemPosition, Quaternion.identity, healthItemTransform);
            Instantiate(vfxItemPickUp, healthItemPosition, Quaternion.identity);
            
            collision.GetComponent<PlayerHealth>().CureHealth(healthToGive);
            Destroy(gameObject, 0.5f);
        }
    }
}
