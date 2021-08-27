
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The Purpose Of This Script Is:
//
//  Set the operation of a chained rotating object, with different customization options.
//
//  Documentation and References:
//
//  Unity Awake: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
//  Unity Update: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
//  Unity OnCollisionEnter2D: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnCollisionEnter2D.html
//  Unity CompareTag: https://docs.unity3d.com/ScriptReference/Component.CompareTag.html
//  Unity Instantiate: https://docs.unity3d.com/ScriptReference/Object.Instantiate.html
//  Unity Destroy: https://docs.unity3d.com/ScriptReference/Object.Destroy.html
//
//  C# Properties: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/properties

using Interfaces;
using UnityEngine;

namespace Levels.General
{
    // Component required for this Script to work.
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(CircleCollider2D))]

    public class RotatingChainedObject : MonoBehaviour
    {
        [Header("Rotation Settings")] [Space(5)] 
        [Tooltip("Sets whether the object rotates or not")]
        [SerializeField] private bool isItRotating = true;
        [Tooltip("True to rotate left and False to rotate right")]
        [SerializeField] private bool rotationDirection;
        [Tooltip("Sets the rotation speed")]
        [Range(100f, 300f)] [SerializeField] private float rotationSpeed = 100f;
        [Space(15)]
        
        [Header("Damage Settings")] [Space(5)]
        [Tooltip("Sets the amount of damage it causes when it collides")]
        [Range(5f, 50f)] [SerializeField] private float damageToGive = 10f;
        [Range(1.0F, 10.0f)][SerializeField] private float kockbackForce = 5;
        [Space(15)]
        [Space(15)]
        
        [Header("VFX Settings")] [Space(5)]
        [Tooltip("Determine the pivot point of rotation of the pendulum.")]
        [SerializeField] private Transform rotationAxis;
        [Tooltip("Insert an ParticlesPrefab here, they will be used when the pendulum stops, to simulate damage")]
        [SerializeField] private GameObject vfxParticles;
        [Space(15)]
        
        [Header("SFX Settings")] [Space(5)]
        [Tooltip("Determine the pivot point for instantiating an audio in case you have")]
        [SerializeField] private Transform sfxAxis;
        [Tooltip("Insert here an AudioPrefab")]
        [SerializeField] private GameObject sfxChainedObject;
        
        // 
        private Vector3 currentRotationDirection;
        
        // ID parameters for the animator
        private static readonly int LowKnockback = Animator.StringToHash("LowKnockback");
        private static readonly int HighKnockback = Animator.StringToHash("HighKnockback");
        
        // Check that the components necessary for the operation are not empty and instantiate the sound effect.
        private void Awake()
        {
            if (rotationAxis == null)
            {
                Debug.LogError(
                    "<color=#D22323><b>The chained object axis cannot be empty, please add one</b></color>");
            }
            else if (vfxParticles == null)
            {
                Debug.LogError(
                    "<color=#D22323><b>The particle system cannot be empty, please add one</b></color>");
            }
            else if (sfxAxis == null)
            {
                Debug.LogError(
                    "<color=#D22323><b>The sound effects axis cannot be empty, please add one</b></color>");
            }
            else if (sfxChainedObject == null)
            {
                Debug.LogError(
                    "<color=#D22323><b>The chained object sound effect cannot be empty, please add one</b></color>");
            }
            else
            {
                Instantiate(sfxChainedObject, sfxAxis.position, Quaternion.identity, sfxAxis);
            }
        }

        // While the object is rotating, it spins around its axis of rotation at the set speed.
        private void Update()
        {
            if (!isItRotating) return;

            currentRotationDirection = rotationDirection ? Vector3.forward : Vector3.back;
            transform.RotateAround(rotationAxis.position, currentRotationDirection,
                rotationSpeed * Time.deltaTime);
        }

        // Check if it collided with the player, to cause the established damage.
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("Player")) return;
            
            collision.transform.GetComponent<Player.PlayerController>().Core.Combat.TakeDamage(damageToGive);
            
            if (damageToGive <= kockbackForce)
            {
                collision.transform.GetComponent<Player.PlayerController>()
                    .PlayerAnimator.SetTrigger(LowKnockback);
                collision.transform.GetComponent<Player.PlayerController>().Core.Combat.KnockBack(
                    new Vector2(1,1),10,
                    -collision.transform.GetComponent<Player.PlayerController>().Core.Movement.FacingDirection);
            }
            else
            {
                collision.transform.GetComponent<Player.PlayerController>()
                    .PlayerAnimator.SetTrigger(HighKnockback);
                collision.transform.GetComponent<Player.PlayerController>().Core.Combat.KnockBack(
                    new Vector2(1,2),15,
                    -collision.transform.GetComponent<Player.PlayerController>().Core.Movement.FacingDirection);
            }
        }

        // Changes the direction of rotation of the object.
        public void ChangeRotationDirection() => rotationDirection = !rotationDirection;

        
        // Allows you to change the state of the rotating object, to stop or resume rotation.
        public void ChangeRotationState()
        {
            if (isItRotating)
            {
                isItRotating = false;
                Instantiate(vfxParticles, rotationAxis.position, Quaternion.identity, rotationAxis);
                Destroy(sfxAxis.transform.GetChild(0).gameObject);
            }
            else
            {
                isItRotating = true;
                Instantiate(sfxChainedObject, sfxAxis.position, Quaternion.identity, sfxAxis);
                Destroy(rotationAxis.GetChild(0).gameObject);
            }
        }
    }
}
