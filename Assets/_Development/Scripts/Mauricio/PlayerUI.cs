using UnityEngine;

namespace _Development.Scripts.Mauricio
{
    public class PlayerUI : MonoBehaviour
    {
        public static PlayerUI Instance;
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
    }
}
