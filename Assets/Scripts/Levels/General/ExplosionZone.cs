
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The Purpose Of This Script Is:
//
//  Generate an impact zone where damage can be caused to the player.
//
//  Documentation and References:
//
//  Unity Awake: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
//  Unity OnCollisionEnter2D: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnCollisionEnter2D.html

using UnityEngine;

namespace Levels.General
{
    // Component required for this Script to work.
    [RequireComponent(typeof(CircleCollider2D))]
    
    public class ExplosionZone: MonoBehaviour
    {
        private CircleCollider2D explosionZone;
        
        // Set the explosion zone and check if it is a trigger.
        private void Awake()
        {
            explosionZone = GetComponent<CircleCollider2D>();
            if (!explosionZone.isTrigger) explosionZone.isTrigger = true;
        }
        
        // Check if it collided with the player, to cause the established damage.
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("Player")) return;
            
            collision.transform.GetComponent<Player.PlayerController>()
                .Damage(GetComponentInParent<ExplosiveObject>().DamageToGive);
        }
    }
}
