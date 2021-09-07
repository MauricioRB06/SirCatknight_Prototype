
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace _Development.Scripts.Mauricio.Managers
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        
        [SerializeField] private AudioMixer musicMixer;
        [SerializeField] private AudioMixer sfxMixer;
        
        // 
        private GameObject _objectLevelMusic;
        private GameObject _objectBossMusic;
        
        // 
        public AudioSource LevelMusic { get; private set; }
        public AudioSource BossMusic { get; private set; }
        
        private Animator _audioFadeOut;
        
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                
                if (SceneManager.GetActiveScene().name == "Credits" ||
                    SceneManager.GetActiveScene().name == "MainMenu")
                {
                    _objectLevelMusic =  GameObject.FindWithTag("Level Music");
                    _objectBossMusic = GameObject.FindWithTag("Level Music");
            
                    LevelMusic = _objectLevelMusic.GetComponent<AudioSource>();
                    BossMusic = _objectBossMusic.GetComponent<AudioSource>();
                }
                else
                {
                    _objectLevelMusic =  GameObject.FindWithTag("Level Music");
                    _objectBossMusic = GameObject.FindWithTag("Boss Music");
            
                    LevelMusic = _objectLevelMusic.GetComponent<AudioSource>();
                    BossMusic = _objectBossMusic.GetComponent<AudioSource>(); 
                }
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private IEnumerator PlayMusicInLevel()
        {
            yield return new WaitForSeconds(0.1f);
            _objectLevelMusic =  GameObject.FindWithTag("Level Music");
            _objectBossMusic = GameObject.FindWithTag("Boss Music");
            
            LevelMusic = _objectLevelMusic.GetComponent<AudioSource>();
            BossMusic = _objectBossMusic.GetComponent<AudioSource>();
            
            PlaySound(LevelMusic);
        }
        
        private IEnumerator PlayMusicInUI()
        {
            yield return new WaitForSeconds(0.1f);
            _objectLevelMusic =  GameObject.FindWithTag("Level Music");
            _objectBossMusic = GameObject.FindWithTag("Level Music");
            
            LevelMusic = _objectLevelMusic.GetComponent<AudioSource>();
            BossMusic = _objectBossMusic.GetComponent<AudioSource>();
            
            PlaySound(LevelMusic);
        }
        
        private string SceneTrigger(string sceneName)
        {
            if (sceneName == "Credits" || sceneName == "MainMenu")
            {
                StartCoroutine(PlayMusicInUI());
            }
            else
            {
                StartCoroutine(PlayMusicInLevel());
            }
            
            return sceneName;
        }
        
        private void OnDisable()
        {
            LevelManager.Instance.DelegatelevelChange -= SceneTrigger;
        }
        
        public static void PlaySound(AudioSource sourceAudio) => sourceAudio.Play();
        public static void StopAudio(AudioSource sourceAudio) => sourceAudio.Stop();

        private void Start()
        {
            LevelManager.Instance.DelegatelevelChange += SceneTrigger;
            PlaySound(LevelMusic);
        }

        // 
        public void MasterVolume(float sliderMasterVolume)
        {
            musicMixer.SetFloat("MusicMasterVolume", sliderMasterVolume);
            sfxMixer.SetFloat("SFXMasterVolume", sliderMasterVolume);
        }
        
        // 
        public void EnviromentMusicVolume(float sliderWorldMusicVolume)
        {
            musicMixer.SetFloat("MusicWorldVolume", sliderWorldMusicVolume);
        }
        
        // 
        public void BossMusicVolume(float sliderBossMusicVolume)
        {
            musicMixer.SetFloat("MusicBossFightVolume", sliderBossMusicVolume);
        }
        
        // 
        public void SfxVolume(float sliderSfxVolume)
        {
            sfxMixer.SetFloat("SFXPlayerVolume", sliderSfxVolume);
            sfxMixer.SetFloat("SFXEnviromentVolume", sliderSfxVolume);
            sfxMixer.SetFloat("SFXUIVolume", sliderSfxVolume);
            sfxMixer.SetFloat("SFXEnemiesVolume", sliderSfxVolume);
            sfxMixer.SetFloat("SFXFinalBossVolume", sliderSfxVolume);
        }
    }
}
