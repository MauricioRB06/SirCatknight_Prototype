
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The Purpose Of This Script Is:
//
//  Set the operation of a pendulum type object, with different customization options.
//
//  Documentation and References:
//
//  Unity Awake: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
//  Unity Update: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
//  Unity RotateAround: https://docs.unity3d.com/ScriptReference/Transform.RotateAround.html
//  Unity OnCollisionEnter2D: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnCollisionEnter2D.html
//  Unity CompareTag: https://docs.unity3d.com/ScriptReference/Component.CompareTag.html
//  Unity Instantiate: https://docs.unity3d.com/ScriptReference/Object.Instantiate.html
//  Unity Destroy: https://docs.unity3d.com/ScriptReference/Object.Destroy.html

using Interfaces;
using UnityEngine;

namespace Levels.General
{
    // Component required for this Script to work.
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(PolygonCollider2D))]
    
    public class PendulumObject : MonoBehaviour
    {
        [Header("Oscillation Settings")] [Space(5)]
        [Tooltip("Sets whether the object oscillates or not")]
        [SerializeField] private bool isItOscillating = true;
        [Tooltip("Sets the oscillation speed")]
        [Range(50f, 250f)][SerializeField] private float oscillationSpeed = 100f;
        [Tooltip("Sets the left limit of the oscillation angle")]
        [Range(0.3f, 0.8f)][SerializeField] private float oscillationLeftLimit = 0.8f;
        [Tooltip("Sets the right limit of the oscillation angle")]
        [Range(-0.3f, -0.8f)][SerializeField] private float oscillationRightLimit= -0.8f;
        [Space(15)]
        
        [Header("Damage Settings")] [Space(5)]
        [Tooltip("Sets the amount of damage it causes when it collides")]
        [Range(5f, 50f)][SerializeField] private float damageToGive = 10f;
        [Tooltip("If the damage applied is greater than this strength, a HighKnockback will be applied to the player")]
        [Range(1.0F, 10.0f)][SerializeField] private float kockbackForce = 5;
        [Space(15)]
        
        [Header("VFX Settings")] [Space(5)]
        [Tooltip("Determine the pivot point of rotation of the pendulum")]
        [SerializeField] private Transform rotationAxis;
        [Tooltip("Insert an ParticlesPrefab here, they will be used when the pendulum stops, to simulate damage")]
        [SerializeField] private GameObject vfxParticles;
        [Space(15)]
        
        [Header("SFX Settings")] [Space(5)]
        [Tooltip("Determine the pivot point where the pendulum sound effect will be located")]
        [SerializeField] private Transform sfxAxis;
        [Tooltip("Insert an AudioPrefab here, will be used to give the pendulum swing sound effect")]
        [SerializeField] private GameObject sfxPendulum;
        
        private static readonly int LowKnockback = Animator.StringToHash("LowKnockback");
        private static readonly int HighKnockback = Animator.StringToHash("HighKnockback");
        
        // Check that the components necessary for the operation are not empty and instantiate the sound effect.
        private void Awake()
        {
            if (rotationAxis == null)
            {
                Debug.LogError(
                    "<color=#D22323><b>The pendulum rotation axis cannot be empty, please add one</b></color>");
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
            else if (sfxPendulum == null)
            {
                Debug.LogError(
                    "<color=#D22323><b>The pendulum sound effect cannot be empty, please add one</b></color>");
            }
            else
            {
                Instantiate(sfxPendulum, sfxAxis.position, Quaternion.identity, sfxAxis);
            }
        }
        
        // While the pendulum is running, it performs a rotation of the pendulum based on the limits and the set speed.
        private void Update()
        {
            if (!isItOscillating) return;
            
            transform.RotateAround(rotationAxis.position, Vector3.forward,
                oscillationSpeed * Time.deltaTime);
            
            if (transform.rotation.z > oscillationLeftLimit)
            {
                oscillationSpeed *= -1;
            }
            else if (transform.rotation.z < oscillationRightLimit)
            {
                oscillationSpeed *= -1;
            }
        }
        
        // Check if it collided with the player, to cause the established damage.
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("Player")) return;
            
            collision.transform.GetComponent<Player.PlayerController>().Core.Combat.TakeDamage(damageToGive);
            collision.transform.GetComponent<Player.PlayerController>().PlayerHealth.TakeDamage(damageToGive);
            
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
        
        // Allows you to change the pendulum state, to stop or resume its oscillation.
        public void ChangeOscillationState()
        {
            if (isItOscillating)
            {
                isItOscillating = false;
                Instantiate(vfxParticles, rotationAxis.position, Quaternion.identity, rotationAxis);
                Destroy(sfxAxis.GetChild(0).gameObject);
            }
            else
            {
                isItOscillating = true;
                Instantiate(sfxPendulum, sfxAxis.position, Quaternion.identity, sfxAxis);
                Destroy(rotationAxis.GetChild(0).gameObject);
            }
        }
    }
}
