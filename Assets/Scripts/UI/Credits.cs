using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/* Documentation
 *
 * Coroutine: https://docs.unity3d.com/Manual/Coroutines.html
 * 
 */

namespace UI
{
    public class Credits : MonoBehaviour
    {
        public FadePanel fadePanel;
        private void Start()
        {
            fadePanel.FadeIn();
            StartCoroutine(FinishCredits());
        }

        private IEnumerator FinishCredits()
        {
            yield return new WaitForSecondsRealtime(207f);
            fadePanel.FadeOut();
            yield return new WaitForSecondsRealtime(3f);
            SceneManager.LoadScene("Main_Menu");
        }
    }
}