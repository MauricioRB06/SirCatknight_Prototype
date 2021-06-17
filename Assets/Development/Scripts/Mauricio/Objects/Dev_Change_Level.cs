using UnityEngine;
using UnityEngine.SceneManagement;

namespace Development.Scripts.Mauricio.Objects
{
    public class Change_Level : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}
