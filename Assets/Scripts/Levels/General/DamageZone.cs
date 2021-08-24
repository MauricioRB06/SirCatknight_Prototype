
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The Purpose Of This Script Is:
//
//  Generate a zone where constant damage is applied to the character as long as he remains in it.
//
//  Documentation and References:
//
//  Unity Awake: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
//  Unity Coroutines: https://docs.unity3d.com/Manual/Coroutines.html
//  Unity OnTriggerStay2D: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnTriggerStay2D.html

using System.Collections;
using Interfaces;
using Player;
using UnityEngine;

namespace Levels.General
{
    // Components required for this Script to work.
    [RequireComponent(typeof(PolygonCollider2D))]
    
    public class DamageZone : MonoBehaviour
    {
        [Header("Damage Settings")] [Space(5)]
        [Tooltip("Sets whether the zone is active or not")]
        [SerializeField] private bool enableDamageZone = true;
        [Tooltip("Damage will be applied consistently on every second")]
        [Range(1f, 10f)][SerializeField] private float damagePerSecond = 1f;
        [Space(15)]
        
        [Header("SFX Settings")] [Space(5)]
        [Tooltip("Insert An AudioPrefab Here")]
        [SerializeField] private GameObject sfxDamageZone;
        
        // To control the behaviour of collider.
        private PolygonCollider2D _damageZoneCollider;
        
        // Coroutine that makes the sprites of the damage zone appear smoothly.
        private static IEnumerator ChildFadeIn(Renderer childSpriteRenderer, Color childSpriteColor)
        {
            for (var childSpriteAlpha = 0.0f; childSpriteAlpha <= 1.0f; childSpriteAlpha += 0.1f)
            {
                childSpriteColor.a = childSpriteAlpha;
                childSpriteRenderer.material.color = childSpriteColor;
                yield return new WaitForSeconds(0.05f);
            }
            
            childSpriteColor.a = 1;
            childSpriteRenderer.material.color = childSpriteColor;
        }
        
        // Coroutine that makes the sprites in the damage area disappear smoothly.
        private static IEnumerator ChildFadeOut(Renderer childSpriteRenderer, Color childSpriteColor)
        {
            for (var childSpriteAlpha = 1.0f; childSpriteAlpha >= 0.0f; childSpriteAlpha -= 0.1f)
            {
                childSpriteColor.a = childSpriteAlpha;
                childSpriteRenderer.material.color = childSpriteColor;
                yield return new WaitForSeconds(0.05f);
            }
            
            childSpriteColor.a = 0;
            childSpriteRenderer.material.color = childSpriteColor;
        }
        
        // Sets the initial settings for the damage zone and verify that no components are missing.
        private void Awake()
        {
            _damageZoneCollider = GetComponent<PolygonCollider2D>();
            if (!_damageZoneCollider.isTrigger) _damageZoneCollider.isTrigger = true;
            
            if (transform.childCount == 0)
            {
                Debug.LogError("<color=#D22323><b>" +
                               "The damage zone must contain at least one object with a SpriteRenderer so" +
                               " that it can be viewed by the player, please add at least one</b></color>");
            }
            
            if (sfxDamageZone == null)
            {
                Debug.LogError("<color=#D22323><b>" +
                               "The damage zone sound effect is empty, please add one</b></color>");
            }
            else
            {
                var damageZone = transform;
                Instantiate(sfxDamageZone, damageZone.position, Quaternion.identity, damageZone);
            }
        }
        
        // As long as the player remains inside, damage is applied per second.
        private void OnTriggerStay2D(Collider2D collision)
        {
            if (!enableDamageZone) return;
            if(!collision.gameObject.CompareTag("Player")) return;
            
            collision.transform.GetComponent<Player.PlayerController>()
                .Core.Combat.TakeDamage(damagePerSecond * Time.deltaTime);
        }
        
        // Allows you to change the status of the zone, to enable or disable it.
        [ContextMenu("zona fuego")]
        public void ChangeDamageState()
        {
            if (enableDamageZone)
            {
                enableDamageZone = false;
                _damageZoneCollider.enabled = false;
                
                foreach (Transform child in transform)
                {
                    var childSpriteRenderer = child.GetComponent<SpriteRenderer>();
                    var childSpriteColor = childSpriteRenderer.material.color;
                    StartCoroutine(ChildFadeOut(childSpriteRenderer, childSpriteColor));
                }
            }
            else
            {
                enableDamageZone = true;
                _damageZoneCollider.enabled = true;
                
                foreach (Transform child in transform)
                {
                    var childSpriteRenderer = child.GetComponent<SpriteRenderer>();
                    var childSpriteColor = childSpriteRenderer.material.color;
                    StartCoroutine(ChildFadeIn(childSpriteRenderer, childSpriteColor));
                }
            }
        }
    }
}
