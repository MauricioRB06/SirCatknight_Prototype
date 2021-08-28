using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Development.Scripts.Mauricio
{
    public enum GameState
    {
        MainMenu,
        InGame,
        PauseMenu,
        GameOver
    }
    
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance;

        public GameState currentGameState = GameState.MainMenu;

        private Transform containerTest;
        
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
        }

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
            else if (newGameState == GameState.PauseMenu)
            {
                //TODO: Create PauseMenu State
            }
            else if (newGameState == GameState.GameOver)
            {
                SceneManager.LoadScene("MainMenu");
            }

            currentGameState = newGameState;
            Debug.Log("GameState: " + currentGameState);
        }
        
        public void InGame()
        {
            ChangeGameState(GameState.InGame);
        }
        
        public void GameOver()
        {
            ChangeGameState(GameState.GameOver);
        }
        
        public void MainMenu()
        {
            ChangeGameState(GameState.MainMenu);
        }
        
        public void PauseMenu()
        {
            ChangeGameState(GameState.PauseMenu);
        }
    }
}
