
// Copyright (c) 2021 MauricioRB06 <https://github.com/MauricioRB06>
// MIT License < Please Read LICENSE.md >
//
//  The Purpose Of This Script Is:
//
//  Generate a death zone, when the player falls into it the death event is activated.
//
//  Documentation and References:
//
//  Unity Awake: https://docs.unity3d.com/ScriptReference/MonoBehaviour.Awake.html
//  Unity OnTriggerEnter2D: https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnTriggerEnter2D.html

using Player;
using SrCatknight.Scripts.Managers;
using SrCatknight.Scripts.Player;
using UnityEngine;

namespace SrCatknight.Scripts.Levels.General
{
    public enum TypeOfDeath{
        Fall,
        Lava
    }
    
    // Component required for this Script to work.
    [RequireComponent(typeof(BoxCollider2D))]
    
    public class DeathZone : MonoBehaviour
    {
        [Header("Indicates the type of death for this zone")] [Space(5)]
        [Tooltip("Depending on the type of death, a different animation will be played, default by -Fall-")]
        [SerializeField] private TypeOfDeath typeOfDeath;
        
        // To control the behaviour of collider.
        private BoxCollider2D _deathZoneCollider;
        
        // Sets the initial settings.
        private void Awake()
        {
            _deathZoneCollider = GetComponent<BoxCollider2D>();
            if (!_deathZoneCollider.isTrigger) _deathZoneCollider.isTrigger = true;
        }
        
        // Checks if the player has fallen into it and triggers the death event.
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(!collision.gameObject.CompareTag("Player")) return;
            
            PlayerController.Die(typeOfDeath.ToString());
            GameManager.Instance.GameOver();
        }
    }
}
