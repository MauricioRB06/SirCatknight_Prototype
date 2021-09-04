
using _Development.Scripts.Mauricio;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/* Documentation:
* 
* Scripting Localization: https://docs.unity3d.com/Packages/com.unity.localization@1.0/manual/Scripting.html
* 
*/

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Toggle fullScreen;
        [SerializeField] private TMP_Dropdown resolution;
        [SerializeField] private TMP_Dropdown antialiasing;
        [SerializeField] private Slider masterVolume;
        [SerializeField] private Slider worldMusicVolume;
        [SerializeField] private Slider bossMusicVolume;
        [SerializeField] private Slider sfxVolume;
        
        // 
        private bool _fullScreen;
        
        // 
        private int _antialiasing;
        
        // 
        private int _resolutionIndex;
        
        //
        private Resolution[] resolutionList;
        
        //
        private float _masterVolume;
        
        //
        private float _musicVolume;
        
        //
        private float _sfxVolume;
        
        //
        private void OnEnable()
        {
            // 
            resolutionList = Screen.resolutions;
            
            // 
            fullScreen.onValueChanged.AddListener(delegate { SettingsScreenMode(); });
            resolution.onValueChanged.AddListener(delegate { SettingsResolution(); });
            antialiasing.onValueChanged.AddListener(delegate { SettingsAntialiasing(); });
            masterVolume.onValueChanged.AddListener(delegate { SettingsMasterVolume(); });
            worldMusicVolume.onValueChanged.AddListener(delegate { SettingsWorldMusicVolume(); });
            bossMusicVolume.onValueChanged.AddListener(delegate { SettingsBossMusicVolume(); });
            sfxVolume.onValueChanged.AddListener(delegate { SettingsSfxVolume(); });
        }
        
        // 
        public void NewGame()
        {
            SceneManager.LoadScene("Level_0");
        }
        
        // 
        public void Continue()
        {
            Debug.Log("Continue Game File Doesn't exist");
        }
        
        // 
        public void Credits()
        {
            SceneManager.LoadScene("Credits");
        }
        
        // 
        public void Exit()
        {
            Application.Quit();
        }
        
        // 
        public void LanguageEnglish()
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[0];
        }
        
        // 
        public void LanguageSpanish()
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
        }
        
        // 
        public void LanguageFrench()
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[2];
        }
        
        // 
        public void LanguageJapanese()
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[3];
        }
        
        // 
        private void SettingsScreenMode()
        {
            Screen.fullScreen = fullScreen.isOn;
        }
        
        // 
        private void SettingsResolution()
        {
            
        }
        
        // 
        private void SettingsAntialiasing()
        {
            QualitySettings.antiAliasing = (int)Mathf.Pow(2,antialiasing.value);
        }
        
        // 
        private void SettingsMasterVolume()
        {
            AudioManager.Instance.MasterVolume(masterVolume.value);
        }
        
        // 
        private void SettingsWorldMusicVolume()
        {
            AudioManager.Instance.WorldMusicVolume(worldMusicVolume.value);
        }
        
        // 
        private void SettingsBossMusicVolume()
        {
            AudioManager.Instance.BossMusicVolume(bossMusicVolume.value);
        }
        
        // 
        private void SettingsSfxVolume()
        {
            AudioManager.Instance.SfxVolume(sfxVolume.value);
        }
    }
}
