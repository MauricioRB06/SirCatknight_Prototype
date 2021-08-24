
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

using System.Collections;
using UnityEngine;

namespace Levels.General
{
    // Components required for this Script to work.
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(BoxCollider2D))]
    
    public class LauncherObject: MonoBehaviour
    {
        [Header("Launcher Settings")] [Space(5)]
        [Tooltip("Sets whether the launcher is active or not")]
        [SerializeField] private bool launcherIsEnable = true;
        [Space(15)]
        
        [Header("Launch Settings")] [Space(5)]
        [Tooltip("Sets the direction of the projectile, based on the rotation of the launcher")]
        public Transform launcherRotationAxis;
        [Tooltip("Projectile launching point")]
        public Transform projectileLaunchPoint;
        [Tooltip("Projectile to be fired (ProjectilePrefab)")]
        public GameObject[] projectile;
        [Tooltip("Projectile travel limit (It must be in a straight line with the launching point)")]
        public Transform projectileLimit;
        [Space(15)]
        
        [Header("Projectile Settings")] [Space(5)]
        [Tooltip("Waiting time between each attack (in seconds)")]
        [Range(2.0f, 8.0f)][SerializeField] private float launchAttackTime = 3.0f;
        [Tooltip("Projectile launch velocity")]
        [Range(2.0f, 10.0f)][SerializeField] private float projectileSpeed = 2.0f;
        [Space(15)]
        
        [Header("SFX Settings")] [Space(5)]
        [Tooltip("Launching sound effect axis.")]
        [SerializeField] private Transform sfxLaunchPoint;
        [Tooltip("Insert Here An AudioPrefab")]
        [SerializeField] private GameObject sfxProjectileShot;
        [Tooltip("Insert Here An AudioPrefab")]
        [Range(0.1f, 5.0f)][SerializeField] private float lifetimeShotSfx = 0.5f;
        
        // It tells the launcher how often to shoot.
        private float waitedTime;
        
        // To control states of animator.
        private Animator launcherAnimator;
        
        // ID Parameters for animator
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Reload = Animator.StringToHash("Reload");
        
        // To check in which direction to shoot.
        public int LauncherRotation { get; private set; }
        
        // So that the speed at which the projectiles will be fired can be accessed.
        public float ProjectileSpeed => projectileSpeed;
        
        // Coroutine that destroys the firing sounds after completion.
        private IEnumerator DestroySound()
        {
            yield return new WaitForSeconds(lifetimeShotSfx);
            Destroy(sfxLaunchPoint.GetChild(0).gameObject);
        }
        
        // Check that the necessary components are configured correctly.
        private void Awake()
        {
            if (launcherRotationAxis == null)
            {
                Debug.LogError("<color=#D22323><b>" +
                               "The axis of rotation of the launcher cannot be empty, the axis of rotation must be " +
                               "the parent element of the object</b></color>");
            }
            else if (projectileLaunchPoint == null)
            {
                Debug.LogError("<color=#D22323><b>" +
                               "The projectile launching point is empty, please add one</b></color>");
            }
            else if (projectile.Length == 0)
            {
                Debug.LogError("<color=#D22323><b>The projectile field is empty, please add one</b></color>");
            }
            else if (projectileLimit == null)
            {
                Debug.LogError("<color=#D22323><b>" +
                               "The life limit point of the projectiles is empty, please add one</b></color>");
            }
            else if (sfxLaunchPoint == null)
            {
                Debug.LogError("<color=#D22323><b>The launch sound axis is empty, please add one</b></color>");
            }
            else if (sfxProjectileShot == null)
            {
                Debug.LogError("<color=#D22323><b>The launch sound effect is empty, please add one</b></color>");
            }
            else
            {
                LauncherRotation = (int)launcherRotationAxis.transform.rotation.y;
                launcherAnimator = GetComponent<Animator>();
            }
        }
        
        // Sets the launch time between projectiles.
        private void Start()
        {
            waitedTime = launchAttackTime;
        }
        
        // Checks if the launcher is active and if it should fire.
        private void Update()
        {
            if (!launcherIsEnable) return;
            
            if (waitedTime <= 0)
            {
                waitedTime = launchAttackTime;
                launcherAnimator.SetTrigger(Attack);
            }
            else
            {
                waitedTime -= Time.deltaTime;
            }
        }
        
        // Changes the status of the launcher, between active and inactive.
        public void ChangeLauncherState()
        {
            launcherIsEnable = !launcherIsEnable;
        }
        
        // Activated from the animator, it tells the launcher at which point in the animation to fire.
        private void AnimationTrigger()
        {
            Instantiate(sfxProjectileShot, sfxLaunchPoint.position, Quaternion.identity, sfxLaunchPoint);
            StartCoroutine(DestroySound());
            Instantiate(projectile[Random.Range(0, projectile.Length - 1)], projectileLaunchPoint.position, Quaternion.identity, transform);
        }
        
        // Activated from the animator, it indicates to the launcher that he has fired and must wait.
        private void AnimationFinishTrigger() => launcherAnimator.SetTrigger(Reload);
    }
}
