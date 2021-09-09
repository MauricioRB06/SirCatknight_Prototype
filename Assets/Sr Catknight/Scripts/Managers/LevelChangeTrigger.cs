using UnityEngine;

namespace _Development.Scripts.Mauricio.Managers
{
    public class LevelChangeTrigger : MonoBehaviour
    {
        private enum Levels{
            MainMenu,
            Credits,
            Level_0,
            Level_1,
            Level_2,
            Level_3,
            Level_4,
            Level_5
        }
        
        [Header("Name Scene To Move")] [Space(10)]
        [Tooltip("Here you must enter the name of the scene you are going to change to")]
        [SerializeField] private Levels sceneToMove;
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.CompareTag("Player")) return;

            LevelManager.Instance.ChangeLevel(sceneToMove.ToString());
        }
    }
}
