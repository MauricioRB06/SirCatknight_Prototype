using UnityEngine;

namespace _Development.Scripts.Mauricio
{
    public class PlayerCamera : MonoBehaviour
    {
        private static PlayerCamera _instance;
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
