using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Development.Scripts.Mauricio.LevelManager
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance;

        [SerializeField] private GameObject loadingCanvas;
        [SerializeField] private Image progressBar;
        private float _target;
            
        /* Dont Delete This, Work in Progress
         
        [SerializeField] private string sceneName;
        [SerializeField] private Animator levelMusicAnimator;
        private static readonly int FadeOut = Animator.StringToHash("FadeOut");

        private IEnumerator ChangeScene()
        {
            levelMusicAnimator.SetTrigger(FadeOut);
            yield return new WaitForSeconds(3.0f);
            SceneManager.LoadScene(sceneName);
        }
        */
        
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

        public async void LoadScene(string sceneName)
        {
            _target = 0;
            progressBar.fillAmount = 0;
            
            var scene = SceneManager.LoadSceneAsync(sceneName);
            scene.allowSceneActivation = false;

            loadingCanvas.SetActive(true);

            do
            {
                await Task.Delay(100);
                _target = scene.progress;

            } while(scene.progress < 0.9f);
            
            //await Task.Delay(3000);
            
            scene.allowSceneActivation = true;
            loadingCanvas.SetActive(false);
        }

        private void Update()
        {
            progressBar.fillAmount = Mathf.MoveTowards(progressBar.fillAmount, _target, 2 * Time.deltaTime);
        }
    }
}
