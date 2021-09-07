
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Development.Scripts.Mauricio.Managers
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;
        public delegate string LevelChangeDelegate(string sceneName);
        public event LevelChangeDelegate DelegatelevelChange;

        [SerializeField] private GameObject fadePanel;

        private Animator _fadePanelAnimator;
        
        private float _target;
        private static readonly int FadeOut = Animator.StringToHash("FadeOut");
        private static readonly int FadeIn = Animator.StringToHash("FadeIn");
        
        // 
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                _fadePanelAnimator = fadePanel.GetComponent<Animator>();
                fadePanel.SetActive(false);
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        // 
        private IEnumerator FadePanel(string sceneName)
        {
            fadePanel.SetActive(true);
            _fadePanelAnimator.SetTrigger(FadeIn);
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadScene(sceneName);
            DelegatelevelChange?.Invoke(sceneName);
            AudioManager.StopAudio(AudioManager.Instance.LevelMusic);
            yield return new WaitForSeconds(0.5f);
            _fadePanelAnimator.SetTrigger(FadeOut);
            yield return new WaitForSeconds(1f);
            fadePanel.SetActive(false);
        }
        
        // 
        public void ChangeLevel(string sceneName)
        {
            StartCoroutine(FadePanel(sceneName));
        }
    }
}
