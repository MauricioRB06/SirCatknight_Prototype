
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The Purpose Of This Script Is:
//
//  Give an object the ability to do damage to the player and to be able to move.
//
//  Documentation and References:
//
//  Unity Awake: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
//  Unity Update: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Update.html
//  Unity MoveTowards: https://docs.unity3d.com/ScriptReference/Vector2.MoveTowards.html
//  Unity Distance: https://docs.unity3d.com/ScriptReference/Vector2.Distance.html
//  Unity OnCollisionEnter2D: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnCollisionEnter2D.html
//
//  C# Properties: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/properties

using UnityEngine;

namespace Levels.General
{
    // Components required for this Script to work.
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(PolygonCollider2D))]
    
   public class DamageObject : MonoBehaviour
    {
        [Header("Movement Settings")] [Space(5)]
        [Tooltip("Sets whether the object will move or not")]
        [SerializeField] private bool isMovableDamageObject;
        [Tooltip("The waiting time between each movement")]
        [Range(0.1F, 5F)] [SerializeField] private float movementWaitingTime = 1;
        [Tooltip("Object movement speed")]
        [Range(1F, 10F)] [SerializeField] private float movementSpeed = 1;
        [Space(15)]
        
        [Header("Movement Points Route")] [Space(5)]
        [Tooltip("There must be at least 2 points on the route")]
        [SerializeField] private Transform[] movementPoints;
        [Space(15)]
        
        [Header("Damage Settings")] [Space(5)] 
        [Tooltip("If it is not a platform, it may cause damage to the player")]
        [Range(1.0F, 30.0f)][SerializeField] private float damageToGive = 5;
        [Tooltip("If the damage applied is greater than this strength, a HighKnockback will be applied to the player")]
        [Range(1.0F, 10.0f)][SerializeField] private float knockbackForce = 5;
        [Space(15)]
        
        [Header("SFX Settings")] [Space(5)]
        [Tooltip("If left empty, it will not reproduce anything")]
        [SerializeField] private GameObject sfxDamageObject;
        
        // Checks how much time has elapsed at one point before switching to the next point.
        private float _currentWaitTime;
        
        // Navigates through the movementPoints to indicate to the object to which it should move.
        private int _movementPointIterator;
        private static readonly int LowKnockback = Animator.StringToHash("LowKnockback");
        private static readonly int HighKnockback = Animator.StringToHash("HighKnockback");
        
        // Sets the initial settings of the object.
        private void Awake()
        {
            if (isMovableDamageObject && movementPoints.Length < 2)
            {
                Debug.LogError($"<color=#D22323><b>The object: {gameObject.name} has been configured" +
                               " as movable, please add at least 2 points in the movement route.</b></color>");
            }
            if (sfxDamageObject != null)
            {
                var objectTransform = transform;
                Instantiate(sfxDamageObject, objectTransform.position, Quaternion.identity, objectTransform);
            }
            
            _currentWaitTime = movementWaitingTime;
        }
        
        // Checks if the object is movable and makes it scroll through the list of movement points.
        private void Update()
        {
            if (!isMovableDamageObject) return;
            
            transform.position = Vector2.MoveTowards(transform.position, 
                movementPoints[_movementPointIterator].transform.position, 
                movementSpeed * Time.deltaTime);
            
            if (!(Vector2.Distance(transform.position, 
                movementPoints[_movementPointIterator].transform.position) < 0.1f)) return;
            
            if (_currentWaitTime <= 0) 
            {
                if (movementPoints[_movementPointIterator] != movementPoints[movementPoints.Length - 1])
                {
                    _movementPointIterator++;
                }
                else
                {
                    _movementPointIterator = 0;
                }

                _currentWaitTime = movementWaitingTime;
            }
            else
            {
                _currentWaitTime -= Time.deltaTime; 
            }
        }
        
        // Check if the object collided with the player to apply damage.
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.transform.CompareTag("Player")) return;
            
            collision.transform.GetComponent<Player.PlayerController>().Core.Combat.TakeDamage(damageToGive);
            collision.transform.GetComponent<Player.PlayerController>().PlayerHealth.TakeDamage(damageToGive);
            
            if (damageToGive <= knockbackForce)
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
        
        // Sets the object as static.
        public bool SetAsStaticDamageObject => isMovableDamageObject = false;
        
        // Sets the object as movable.
        public bool SetAsMovableDamageObject => isMovableDamageObject = true;
    }
}
