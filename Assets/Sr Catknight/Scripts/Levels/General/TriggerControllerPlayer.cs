
using UnityEngine;
using Player.Input;

namespace Levels.General
{
    [RequireComponent(typeof(BoxCollider2D))]
    
    public class TriggerControllerPlayer : MonoBehaviour
    {
        private enum ControllerType
        { 
            CanMove,
            CanCrouch,
            CanJump,
            CanAttack,
            CanDash,
            CanDodgeRoll,
            CanGrab,
            CanWallSlide,
            LedgeClimb
        }
        
        [Header("Key Settings")] [Space(5)]
        [Tooltip("Select the type of key")]
        [SerializeField] private ControllerType controllerType;
        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.transform.CompareTag("Player")) return;

            switch (controllerType.ToString())
            {
                case "CanMove":
                    collision.GetComponent<PlayerInputHandler>().ChangeControllerCanMove();
                    break;
                case "CanCrouch":
                    collision.GetComponent<PlayerInputHandler>().ChangeControllerCanCrouch();
                    break;
                case "CanJump":
                    collision.GetComponent<PlayerInputHandler>().ChangeControllerCanJump();
                    break;
                case "CanAttack":
                    collision.GetComponent<PlayerInputHandler>().ChangeControllerCanAttack();
                    break;
                case "CanDash":
                    collision.GetComponent<PlayerInputHandler>().ChangeControllerCanDash();
                    break;
                case "CanDodgeRoll":
                    collision.GetComponent<PlayerInputHandler>().ChangeControllerCanDodgeRoll();
                    break;
                case "CanGrab":
                    collision.GetComponent<PlayerInputHandler>().ChangeControllerCanGrab();
                    break;
                case "CanWallSlide":
                    collision.GetComponent<PlayerInputHandler>().ChangeControllerCanWallSlide();
                    break;
                case "LedgeClimb":
                    collision.GetComponent<PlayerInputHandler>().ChangeControllerCanLedgeClimb();
                    break;
            }
            
            Destroy(gameObject);
        }
    }
}
