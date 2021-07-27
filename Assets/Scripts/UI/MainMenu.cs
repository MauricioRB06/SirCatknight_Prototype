using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;

/* Documentation:
* 
* Scripting Localization: https://docs.unity3d.com/Packages/com.unity.localization@1.0/manual/Scripting.html
* 
*/

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        public void NewGame()
        {
            SceneManager.LoadScene("Mauricio");
        }

        public void Enen()
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0] ;
        }

        public void Eses()
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[2];
        }

        public void Jpja()
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
        }
        public void Credits()
        {
            SceneManager.LoadScene("Credits");
        }

        public void Exit()
        {
            Application.Quit();
        }
    }
}
