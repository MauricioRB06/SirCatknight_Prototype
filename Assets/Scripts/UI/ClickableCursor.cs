
using _Development.Scripts.Mauricio.Managers;
using UnityEngine;

namespace UI
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
