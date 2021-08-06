
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The Purpose Of This Script Is:
//
//  Create a configurable projectile from a launcher, which can damage the player.
//
//  Documentation and References:
//
//  Unity Awake: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
//  Unity Update: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
//  Unity OnCollisionEnter2D: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnCollisionEnter2D.html
//  Unity CompareTag: https://docs.unity3d.com/ScriptReference/Component.CompareTag.html
//  Unity Instantiate: https://docs.unity3d.com/ScriptReference/Object.Instantiate.html
//  Unity Destroy: https://docs.unity3d.com/ScriptReference/Object.Destroy.html

using UnityEngine;

namespace Levels.General
{
    // Components required for this Script to work.
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(CircleCollider2D))]
    
    public class Projectile : MonoBehaviour
    {
        [Header("SFX Settings")] [Space(5)]
        [Tooltip("Reproduces when the projectile explodes")]
        [SerializeField] private GameObject projectileCrashSfx;
        
        // To control the animations of the projectile, based on its state.
        private Animator projectileAnimator;
        
        // To configure the projectile properties, based on the launcher settings.
        private float projectileSpeed;
        private int orientationProjectile;
        private Vector2 projectileLimit;
        
        // To know when the projectile has crash.
        private bool _projectileCrash;
        
        // ID Parameters for animator.
        private static readonly int Crash = Animator.StringToHash("Crash");
        
        // Sets the initial configurations for the components and the behavior of the projectile.
        private void Awake()
        {
            if (projectileCrashSfx == null)
            {
                Debug.LogError("<color=#D22323><b>" +
                               "The Projectile Crash Sfx cannot be empty, please add one</b></color>");
            }
            else
            {
                projectileSpeed = GetComponentInParent<LauncherObject>().ProjectileSpeed;
                orientationProjectile = GetComponentInParent<LauncherObject>().LauncherRotation;
                projectileLimit = GetComponentInParent<LauncherObject>().projectileLimit.transform.position;
                projectileAnimator = GetComponent<Animator>();
            }
        }
        
        // Checks if the projectile has not crashed and moves it at the set speed.
        private void Update()
        {
            if (_projectileCrash) return;
            
            switch (orientationProjectile)
            {
                case 1:
                {
                    transform.Translate(Vector2.left * (projectileSpeed * Time.deltaTime));
                
                    if (!(Vector2.Distance(transform.position, projectileLimit) < 0.1f)) return;
                    projectileAnimator.SetTrigger(Crash);
                    break;
                }
                case 0:
                {
                    transform.Translate(Vector2.right * (projectileSpeed * Time.deltaTime));
                
                    if (!(Vector2.Distance(transform.position, projectileLimit) < 0.1f)) return;
                    projectileAnimator.SetTrigger(Crash);
                    break;
                }
            }
        }
        
        // Checks if it has collided with the player to cause damage.
        private void OnCollisionEnter2D(Collision2D collision)
        {
            projectileAnimator.SetTrigger(Crash);
            _projectileCrash = true;

            if (collision.gameObject.CompareTag("Player"))
            {
                collision.transform.GetComponent<Player.PlayerController>().Damage(10);
            }
        }
        
        // Activated from the animator, it disables the projectile collider.
        private void AnimationTrigger()
        {
            var projectileTransform = transform;
            
            Instantiate(projectileCrashSfx, projectileTransform.position, Quaternion.identity, projectileTransform);
            GetComponent<CircleCollider2D>().enabled = false;
        }
        
        // It is activated from the animator, when the destruction animation ends, it destroys the object.
        private void AnimationFinishTrigger() => Destroy(gameObject);
    }
}
