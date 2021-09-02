
using System.Collections;
using Player;
using UnityEngine;

namespace Levels.General
{
    // Components required for this script to work.
    [RequireComponent(typeof(BoxCollider2D))]
    
    public class RestoreZone : MonoBehaviour
    {
        [Header("Destination Settings")] [Space(5)]
        [Tooltip("Sets the destination to which the player will be transported")]
        [SerializeField] private GameObject restorePoint;

        private BoxCollider2D _restoreZoneCollider;
        
        // Coroutine that makes the player's sprites appear smoothly and Teleports the player.
        private IEnumerator PlayerFadeIn(SpriteRenderer playerSpriteRenderer, Color playerSpriteColor)
        {
            for (var playerSpriteAlpha = 0.0f; playerSpriteAlpha <= 1.0f; playerSpriteAlpha += 0.1f)
            {
                playerSpriteColor.a = playerSpriteAlpha;
                playerSpriteRenderer.color = playerSpriteColor;
                yield return new WaitForSeconds(0.05f);
            }
            
            playerSpriteColor.a = 1;
            playerSpriteRenderer.color = playerSpriteColor;
        }
        
        // Coroutine that makes the sprites of player disappear smoothly.
        private IEnumerator PlayerFadeOut(SpriteRenderer playerSpriteRenderer,
            Color playerSpriteColor, GameObject player)
        {
            for (var playerSpriteAlpha = 1.0f; playerSpriteAlpha >= 0.0f; playerSpriteAlpha -= 0.1f)
            {
                playerSpriteColor.a = playerSpriteAlpha;
                playerSpriteRenderer.color = playerSpriteColor;
                yield return new WaitForSeconds(0.05f);
            }
            
            playerSpriteColor.a = 0;
            playerSpriteRenderer.color = playerSpriteColor;
            
            StartCoroutine(PlayerFadeIn(playerSpriteRenderer, playerSpriteColor));
            
            player.transform.position = restorePoint.transform.position;
            player.GetComponent<PlayerController>().Core.Movement.SetVelocityZero();
            player.GetComponent<PlayerController>().RestorePlayer();
        }
        
        // 
        private void Awake()
        {
            if (restorePoint == null)
            {
                Debug.LogError("<color=#D22323><b>" +
                               "The restore point cannot be empty, please add one</b></color>");
            }
            else
            {
                _restoreZoneCollider = GetComponent<BoxCollider2D>();
                if (!_restoreZoneCollider.isTrigger) _restoreZoneCollider.isTrigger = true;
            }
        }
        
        // Check if it is active, if it collided with the character and if it should be destroyed after being used.
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.CompareTag("Player")) return;

            var playerSpriteRenderer = collision.GetComponent<SpriteRenderer>();
            var playerSpriteColor = playerSpriteRenderer.color;
            var teleportTransform = transform;
            
            StartCoroutine(PlayerFadeOut(playerSpriteRenderer, playerSpriteColor, collision.gameObject));
            collision.GetComponent<PlayerController>().Core.Movement.SetVelocityZero();
        }
    }
}
