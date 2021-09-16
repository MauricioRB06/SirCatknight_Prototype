using SrCatknight.Scripts.Managers;
using UnityEngine;

namespace SrCatknight.Scripts.UI
{
    public class ClickableCursor : MonoBehaviour
    {
        [SerializeField] private GameManager.CursorStates cursorMode = GameManager.CursorStates.ClickableCursor;
        
        // 
        private void OnMouseEnter()
        {
            GameManager.Instance.ChangeCursor(cursorMode);
            Debug.Log("Detected");
        }
        
        // 
        private void OnMouseExit()
        {
            GameManager.Instance.ChangeCursor(GameManager.CursorStates.BasicCursor);
        }
    }
}
