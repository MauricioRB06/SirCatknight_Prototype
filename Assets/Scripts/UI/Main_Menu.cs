using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Localization.Settings;

/* Documentación Usada:
* 
* Scripting Localization: https://docs.unity3d.com/Packages/com.unity.localization@1.0/manual/Scripting.html
* 
*/

public class Main_Menu : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("Tutorial");
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
