
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Development.Scripts.Mauricio.Managers
{
    public enum GameState
    {
        MainMenu,
        InGame,
        PauseGame,
        ResumeGame,
        GameOver
    }
    
    public class GameManager : MonoBehaviour
    {
        public enum CursorStates
        {
            BasicCursor,
            ClickableCursor
        }
        
        [SerializeField] private CursorStates currentCursor = CursorStates.BasicCursor;
        [SerializeField] private Texture2D[] cursors;
        [SerializeField] private GameObject pauseCanvas;
        
        public static GameManager Instance;
        
        public GameState currentGameState = GameState.MainMenu;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            pauseCanvas.SetActive(false);
            Cursor.SetCursor(cursors[(int)currentCursor], Vector2.zero, CursorMode.Auto);
            Cursor.lockState = CursorLockMode.Confined;
        }
        
        // 
        public void ChangeCursor(CursorStates newCursorState)
        {
            Cursor.SetCursor(cursors[(int)newCursorState], Vector2.zero, CursorMode.Auto);
        }
        
        // 
        public void InGame() => ChangeGameState(GameState.InGame);
        
        // 
        public void GameOver() => ChangeGameState(GameState.GameOver);

        // 
        public void MainMenu() => ChangeGameState(GameState.MainMenu);
        
        // 
        public void PauseGame() => ChangeGameState(GameState.PauseGame);
        
        // 
        public void ResumeGame() => ChangeGameState(GameState.ResumeGame);
        
        // 
        private void ChangeGameState(GameState newGameState)
        {
            if (newGameState == GameState.MainMenu)
            {
                //TODO: Create MainMenu State
            }
            else if (newGameState == GameState.InGame)
            {
                //TODO: Create InGame State
            }
            else if (newGameState == GameState.PauseGame)
            {
                pauseCanvas.SetActive(true);
                Time.timeScale = 0;
                AudioListener.pause = true;
            }
            else if (newGameState == GameState.ResumeGame)
            {
                pauseCanvas.SetActive(false);
                Time.timeScale = 1;
                AudioListener.pause = false;
            }
            else if (newGameState == GameState.GameOver)
            {
                SceneManager.LoadScene("MainMenu");
            }

            currentGameState = newGameState;
            Debug.Log("GameState: " + currentGameState);
        }
    }
}
