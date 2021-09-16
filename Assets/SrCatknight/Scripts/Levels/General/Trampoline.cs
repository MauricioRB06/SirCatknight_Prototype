
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The Purpose Of This Script Is:
//
//  Generate an object that propels the player upward when the player collides with it.
//
//  Documentation and References:
//
//  Unity OnCollisionEnter2D: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnCollisionEnter2D.html
//  Unity Instantiate: https://docs.unity3d.com/ScriptReference/Object.Instantiate.html

using Player;
using SrCatknight.Scripts.Player;
using UnityEngine;

namespace SrCatknight.Scripts.Levels.General
{
    // Component required for this Script to Work.
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(BoxCollider2D))]
    
    public class Trampoline : MonoBehaviour
    {
        [Header("Trampoline Settings")] [Space(5)]
        [Tooltip("Force of impulse to be applied to the player")]
        [Range(5.0f, 30.0f)][SerializeField] private float trampolineForce = 10;
        [Space(15)]
        
        [Header("Audio Settings")] [Space(5)]
        [Tooltip("Insert here an AudioPrefab")]
        [SerializeField] private GameObject trampolineSfx;
        
        //
        private Animator _trampolineAnimator;
        
        //
        private static readonly int Jump = Animator.StringToHash("Jump");

        // If it detects a collision with the character, it applies an upward force to the character.
        private void Awake()
        {
            _trampolineAnimator = GetComponent<Animator>();
        }

        // If it detects a collision with the character, it applies an upward force to the character.
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.transform.CompareTag("Player")) return;
            
            if (trampolineSfx == null)
            {
                Debug.LogError("SFX Trampoline can not be empty");
            }
            else
            {
                _trampolineAnimator.SetTrigger(Jump);
                
                Instantiate(trampolineSfx, transform.position, Quaternion.identity);
                
                collision.gameObject.GetComponent<PlayerController>().
                    Core.Movement.SetVelocityY(trampolineForce);
            }
        }
    }
}
