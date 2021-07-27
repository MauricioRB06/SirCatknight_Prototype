using UnityEngine;

namespace _Development.Scripts.Mauricio.LevelManager
{
    public class LevelChangeTrigger : MonoBehaviour
    {
        [Header("Name Scene To Move")] [Space(10)]
        [Tooltip("Here you must enter the name of the scene you are going to change to")]
        [SerializeField] private string sceneToMove;
        
        /* Dont Delete, This will be moved to the game menu, to assign it to a button.
         
         public void ChangeScene(string sceneName)
        {
            LevelManager.Instance.LoadScene(sceneName);
        }
        
        */

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(!collision.gameObject.CompareTag("Player")) return;

            if (sceneToMove == null) Debug.LogError("The level field is empty, please complete it");
            
            LevelManager.Instance.LoadScene(sceneToMove);
        }
    }
}
