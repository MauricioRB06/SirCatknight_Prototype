using System.Collections;
using SrCatknight.Scripts.Managers;
using UnityEngine;

/* Documentation
 *
 * Coroutine: https://docs.unity3d.com/Manual/Coroutines.html
 * 
 */

namespace SrCatknight.Scripts.UI
{
    public class Credits : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(FinishCredits());
        }

        private IEnumerator FinishCredits()
        {
            yield return new WaitForSecondsRealtime(122f);
            yield return new WaitForSecondsRealtime(2.5f);
            LevelManager.Instance.ChangeLevel("MainMenu");
        }
    }
}
