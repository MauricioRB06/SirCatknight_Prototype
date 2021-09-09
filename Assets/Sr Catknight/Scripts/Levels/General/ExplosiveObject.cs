
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The Purpose Of This Script Is:
//
//  Generate a customizable bomb-type object that can cause damage to the player.
//
//  Documentation and References:
//
//  Unity Awake: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
//  Unity Coroutines: https://docs.unity3d.com/Manual/Coroutines.html
//  Unity OnCollisionEnter2D: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnCollisionEnter2D.html
//
//  C# Expression Bodies: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/expression-bodied-members

using System.Collections;
using UnityEngine;
using Controllers;

namespace Levels.General
{
    // Components required for this Script to work.
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PolygonCollider2D))]
    
    public class ExplosiveObject : MonoBehaviour
    {
        [Header("Explosion Settings")] [Space(5)]
        [Tooltip("The time it takes for the bomb to explode after activation")]
        [Range(2.0f, 6.0f)][SerializeField] private float timeToExplosion = 5.0f;
        [Space(15)]
        
        [Header("Damage Settings")] [Space(5)]
        [Tooltip("Sets the amount of damage caused by detonation")]
        [Range(5.0f, 50.0f)][SerializeField] private float damageToGive = 5.0f;
        [Space(15)]
        
        [Header("VFX Settings")] [Space(5)]
        [Tooltip("Insert here an ParticlesPrefab")]
        [SerializeField] private GameObject vfxExplosion;
        [Space(15)]
        
        [Header("SFX Settings")] [Space(5)]
        [Tooltip("Insert here an AudioPrefab")]
        [SerializeField] private GameObject sfxBombActivated;
        [Tooltip("Insert here an AudioPrefab")]
        [SerializeField] private GameObject sfxExplosion;
        [Tooltip("Sets the lifetime of the explosion sound effect")]
        [Range(0.1f, 5.0f)][SerializeField] private float lifetimeExplosionSfx = 0.5f;
        
        // To control behaviour of collider.
        private PolygonCollider2D bombActivationZone;
        
        // Object that will be created when the bomb explodes.
        private GameObject explosionZone;
        
        // To control the explosion animations according to the state of the bomb.
        private Animator boomAnimator;
        
        // Other objects can know the damage the bomb will cause, but not modify it.
        public float DamageToGive => damageToGive;
        
        // ID Parameters for the bomb animator.
        private static readonly int BombActivated = Animator.StringToHash("BombActivated");

        // Coroutine that controls the explosion.
        private IEnumerator Explosion()
        {
            var objectTransform = transform;
            var objectPosition = objectTransform.position;
            
            yield return new WaitForSeconds(timeToExplosion);

            objectTransform.GetComponent<SpriteRenderer>().enabled = false;
            Instantiate(vfxExplosion, objectPosition, Quaternion.identity);
            Instantiate(sfxExplosion, objectPosition, Quaternion.identity, objectTransform);
            objectTransform.GetChild(1).gameObject.GetComponent<SoundController>().Destroy(lifetimeExplosionSfx);
            explosionZone.SetActive(true);
            Destroy(gameObject, lifetimeExplosionSfx);
        }
        
        // Initialize the bomb components and verify that a explosion zone is designated.
        private void Awake()
        {
            boomAnimator = GetComponent<Animator>();
            bombActivationZone = GetComponent<PolygonCollider2D>();
            
            if (transform.GetChild(0).gameObject == null)
            {
                Debug.LogError("<color=#D22323><b>The child component Explosion Zone is missing.</b></color>");
            }
            else
            {
                explosionZone = transform.GetChild(0).gameObject;
                bombActivationZone.isTrigger = true;
                explosionZone.SetActive(false);  
            }
        }

        // Check if it collided with the player, to initiate the detonation of the bomb.
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("Player")) return;

            var bombTransform = transform;
            boomAnimator.SetTrigger(BombActivated);
            bombActivationZone.enabled = false;
            Instantiate(sfxBombActivated, bombTransform.position, Quaternion.identity, bombTransform);
            bombTransform.GetChild(1).gameObject.GetComponent<SoundController>().Destroy(timeToExplosion - 0.1f);
            StartCoroutine(Explosion());
        }

        // Changes the status of the bomb, between active and inactive.
        public void ChangeBombStatus()
        {
            bombActivationZone.enabled = !bombActivationZone.enabled;
        }
    }
}
