using UnityEngine;
using UnityEngine.Audio;

/* The Purpose of this Script is:
 
 
 */

namespace _Development.Scripts.Mauricio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        
        [SerializeField] private AudioMixer musicMixer;
        [SerializeField] private AudioMixer sfxMixer;
        
        // 
        private GameObject objectLevelMusic;
        private GameObject objectBossMusic;
        
        // 
        public AudioSource LevelMusic { get; private set; }
        public AudioSource BossMusic { get; private set; }
        
        
        private Animator _audioFadeOut;
        
        [Range(-80.0f, 20.0f)] public float allMusicVolume;
        [Range(-80.0f, 20.0f)] public float sfxVolume;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                
                objectLevelMusic =  GameObject.FindWithTag("Level Music");
                objectBossMusic = GameObject.FindWithTag("Boss Music");
            
                LevelMusic = objectLevelMusic.GetComponent<AudioSource>();
                BossMusic = objectBossMusic.GetComponent<AudioSource>();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public static void PlaySound(AudioSource sourceAudio) => sourceAudio.Play();
        public static void StopAudio(AudioSource sourceAudio) => sourceAudio.Stop();
        
        private void Start()
        {
            PlaySound(LevelMusic);
        }

        private void Update()
        {
            WorldMusicVolume();
            SfxVolume();
        }
        
        public void WorldMusicVolume() => musicMixer.SetFloat("MusicMasterVolume", allMusicVolume);
        public void SfxVolume() => sfxMixer.SetFloat("SFXMasterVolume", sfxVolume);
        
    }
}
