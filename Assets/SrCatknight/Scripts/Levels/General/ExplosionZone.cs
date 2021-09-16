
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

using Enemies;
using Player;
using SrCatknight.Scripts.Player;
using UnityEngine;

namespace SrCatknight.Scripts.Levels.General
{
    // Component required for this Script to work.
    [RequireComponent(typeof(CircleCollider2D))]
    
    public class ExplosionZone: MonoBehaviour
    {
        private CircleCollider2D _explosionZone;
        [SerializeField] private bool damageEntitys;
        [Space(15)]
        [Range(1.0F, 10.0f)][SerializeField] private float knockbackForce = 5;
        private static readonly int LowKnockback = Animator.StringToHash("LowKnockback");
        private static readonly int HighKnockback = Animator.StringToHash("HighKnockback");
        
        
        // Set the explosion zone and check if it is a trigger.
        private void Awake()
        {
            _explosionZone = GetComponent<CircleCollider2D>();
            if (!_explosionZone.isTrigger) _explosionZone.isTrigger = true;
        }
        
        // Check if it collided with the player, to cause the established damage.
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.transform.GetComponent<PlayerController>()
                    .Core.Combat.TakeDamage(GetComponentInParent<ExplosiveObject>().DamageToGive);
                collision.transform.GetComponent<PlayerController>().
                    PlayerHealth.TakeDamage(GetComponentInParent<ExplosiveObject>().DamageToGive);
            
                if (GetComponentInParent<ExplosiveObject>().DamageToGive <= knockbackForce)
                {
                    collision.transform.GetComponent<PlayerController>()
                        .PlayerAnimator.SetTrigger(LowKnockback);
                    collision.transform.GetComponent<PlayerController>().Core.Combat.KnockBack(
                        new Vector2(1,1),10,
                        -collision.transform.GetComponent<PlayerController>().Core.Movement.FacingDirection);
                }
                else
                {
                    collision.transform.GetComponent<PlayerController>()
                        .PlayerAnimator.SetTrigger(HighKnockback);
                    collision.transform.GetComponent<PlayerController>().Core.Combat.KnockBack(
                        new Vector2(1,2),15,
                        -collision.transform.GetComponent<PlayerController>().Core.Movement.FacingDirection);
                }
            }
            else if (collision.transform.CompareTag("Enemy") && damageEntitys)
            {
                collision.transform.GetComponent<EntityController>()
                    .Core.Combat.TakeDamage(GetComponentInParent<ExplosiveObject>().DamageToGive);
            }
        }
    }
}
