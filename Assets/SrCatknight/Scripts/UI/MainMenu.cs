using System.Collections;
using SrCatknight.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

/* Documentation:
* 
* Scripting Localization: https://docs.unity3d.com/Packages/com.unity.localization@1.0/manual/Scripting.html
* 
*/

namespace SrCatknight.Scripts.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Toggle fullScreenToggle;
        [SerializeField] private TMP_Dropdown resolutionDropdown;
        [SerializeField] private TMP_Dropdown antialiasingDropdown;
        [SerializeField] private Slider masterVolumeSlider;
        [SerializeField] private Slider enviromentMusicVolumeSlider;
        [SerializeField] private Slider bossMusicVolumeSlider;
        [SerializeField] private Slider sfxVolumeSlider;
        
        // 
        private bool _fullScreen;
        
        // 
        private int _antialiasing;
        
        // 
        private int _resolutionIndex;
        
        //
        private Resolution[] _resolutionList;
        
        //
        private float _masterVolume;
        
        //
        private float _musicVolume;
        
        //
        private float _sfxVolume;
        
        // 
        private static IEnumerator LoadLanguageSettings()
        {
            yield return new WaitForSeconds(0.1f);
            LoadLanguage();
        }
        
        //
        private void OnEnable()
        {
            // 
            _resolutionList = Screen.resolutions;
            
            // 
            fullScreenToggle.onValueChanged.AddListener(delegate { SettingsScreenMode(); });
            resolutionDropdown.onValueChanged.AddListener(delegate { SettingsResolution(); });
            antialiasingDropdown.onValueChanged.AddListener(delegate { SettingsAntialiasing(); });
            masterVolumeSlider.onValueChanged.AddListener(delegate { SettingsMasterVolume(); });
            enviromentMusicVolumeSlider.onValueChanged.AddListener(delegate { SettingsEnviromentMusicVolume(); });
            bossMusicVolumeSlider.onValueChanged.AddListener(delegate { SettingsBossMusicVolume(); });
            sfxVolumeSlider.onValueChanged.AddListener(delegate { SettingsSfxVolume(); });

            foreach (var resolution in _resolutionList)
            {
                resolutionDropdown.options.Add(new TMP_Dropdown.OptionData(resolution.ToString()));
            }

            if (!PlayerPrefs.HasKey("DefaultSettings") || PlayerPrefs.GetString("DefaultSettings") != "Default")
            {
                DefaultSettings();
                LoadSettings();
            }
            else
            {
                LoadSettings();
            }
        }
        
        // 
        public void NewGame()
        {
            LevelManager.Instance.ChangeLevel("Level_0");
            //TODO crear nuevo archivo de guardado y borrar el anterior si existe
            // borrar preferencias
        }
        
        // 
        public void Continue()
        {
            Debug.Log("Continue Game File Doesn't exist");
            //TODO cargar la partida del archivo de guardado
        }
        
        // 
        public void Credits()
        {
            LevelManager.Instance.ChangeLevel("Credits");
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
            PlayerPrefs.SetInt("Language", 0);
        }
        
        // 
        public void LanguageSpanish()
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[1];
            PlayerPrefs.SetInt("Language", 1);
        }
        
        // 
        public void LanguageFrench()
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[2];
            PlayerPrefs.SetInt("Language", 2);
        }
        
        // 
        public void LanguageJapanese()
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[3];
            PlayerPrefs.SetInt("Language", 3);
        }
        
        // 
        private void SettingsScreenMode()
        {
            Screen.fullScreen = fullScreenToggle.isOn;
            PlayerPrefs.SetInt("FullScreen", fullScreenToggle.isOn ? 1 : 0);
        }
        
        // 
        private void SettingsResolution()
        {
            if (_resolutionList == null) return;
            
            Screen.SetResolution(_resolutionList[resolutionDropdown.value].width,
                _resolutionList[resolutionDropdown.value].height, Screen.fullScreen);
            PlayerPrefs.SetInt("ScreenResolution", resolutionDropdown.value);
        }
        
        // 
        private void SettingsAntialiasing()
        {
            QualitySettings.antiAliasing = (int)Mathf.Pow(2,antialiasingDropdown.value);
            PlayerPrefs.SetInt("AntiAliasing", QualitySettings.antiAliasing);
        }
        
        // 
        private void SettingsMasterVolume()
        {
            AudioManager.Instance.MasterVolume(masterVolumeSlider.value);
            PlayerPrefs.SetFloat("MasterVolume", masterVolumeSlider.value);
        }
        
        // 
        private void SettingsEnviromentMusicVolume()
        {
            AudioManager.Instance.EnviromentMusicVolume(enviromentMusicVolumeSlider.value);
            PlayerPrefs.SetFloat("EnviromentMusicVolume", enviromentMusicVolumeSlider.value);
        }
        
        // 
        private void SettingsBossMusicVolume()
        {
            AudioManager.Instance.BossMusicVolume(bossMusicVolumeSlider.value);
            PlayerPrefs.SetFloat("FinalBossMusicVolume", bossMusicVolumeSlider.value);
        }
        
        // 
        private void SettingsSfxVolume()
        {
            AudioManager.Instance.SfxVolume(sfxVolumeSlider.value);
            PlayerPrefs.SetFloat("SfxVolume", sfxVolumeSlider.value);
        }
        
        // 
        private void DefaultSettings()
        {
            PlayerPrefs.SetString("DefaultSettings","Default");
            PlayerPrefs.SetFloat("MasterVolume", 1);
            PlayerPrefs.SetFloat("EnviromentMusicVolume", 1);
            PlayerPrefs.SetFloat("FinalBossMusicVolume", 1);
            PlayerPrefs.SetFloat("SfxVolume", 1);
            PlayerPrefs.SetInt("AntiAliasing", 4);
            PlayerPrefs.SetInt("FullScreen", 1);
            PlayerPrefs.SetInt("ScreenResolution", _resolutionList.Length - 1);
            PlayerPrefs.SetInt("Language", 0);
        }
        
        // 
        private void LoadSettings()
        {
            var screenMode = PlayerPrefs.GetInt("FullScreen", 1) == 1;
            fullScreenToggle.isOn = PlayerPrefs.GetInt("FullScreen", 1) == 1;
            
            if (_resolutionList != null)
            {
                Screen.SetResolution(
                    _resolutionList[
                        PlayerPrefs.GetInt("ScreenResolution", _resolutionList.Length - 1)].width,
                    _resolutionList[
                        PlayerPrefs.GetInt("ScreenResolution", _resolutionList.Length - 1)].height,
                    Screen.fullScreen = screenMode);

                resolutionDropdown.value = PlayerPrefs.GetInt("ScreenResolution", 
                    _resolutionList.Length - 1);
            }
            
            QualitySettings.antiAliasing = PlayerPrefs.GetInt("AntiAliasing", 4);
            
            antialiasingDropdown.value = QualitySettings.antiAliasing switch
            {
                0 => 0,
                2 => 1,
                4 => 2,
                _ => antialiasingDropdown.value
            };

            StartCoroutine(LoadLanguageSettings());
            
            //AudioManager.Instance.MasterVolume(PlayerPrefs.GetFloat("MasterVolume", 1));
            //AudioManager.Instance.EnviromentMusicVolume(
            //    PlayerPrefs.GetFloat("EnviromentMusicVolume", 1));
            //AudioManager.Instance.BossMusicVolume(PlayerPrefs.GetFloat("FinalBossMusicVolume", 1));
            //AudioManager.Instance.SfxVolume(PlayerPrefs.GetFloat("SfxVolume", 1));
        }

        private static void LoadLanguage()
        {
            LocalizationSettings.SelectedLocale = 
                LocalizationSettings.AvailableLocales.Locales[PlayerPrefs.GetInt("Language", 0)];
        }
    }
}
