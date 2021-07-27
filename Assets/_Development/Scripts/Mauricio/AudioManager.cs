using UnityEngine;
using UnityEngine.Audio;

/* The Purpose of this Script is:
 
 
 */

namespace _Development.Scripts.Mauricio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        
        [SerializeField] private AudioMixer worldMusicMixer;
        [SerializeField] private AudioMixer bossMusicMixer;
        [SerializeField] private AudioMixer sfxMixer;
        
        [SerializeField] private AudioSource levelMusic;
        [SerializeField] private AudioSource bossMusic;
        
        public AudioSource LevelMusic => levelMusic;
        public AudioSource BossMusic => bossMusic;

        private Animator _audioFadeOut;
        
        [Range(-80.0f, 20.0f)] public float allMusicVolume;
        [Range(-80.0f, 20.0f)] public float sfxVolume;
        
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

        public static void PlaySound(AudioSource sourceAudio) => sourceAudio.Play();
        public static void StopAudio(AudioSource sourceAudio) => sourceAudio.Stop();
        
        private void Start()
        {
            PlaySound(levelMusic);
        }

        private void Update()
        {
            BossMusicVolume();
            WorldMusicVolume();
            SfxVolume();
        }
        
        public void BossMusicVolume() => bossMusicMixer.SetFloat("BossMusicVolume", allMusicVolume);
        public void WorldMusicVolume() => worldMusicMixer.SetFloat("WorldMusicVolume", allMusicVolume);
        public void SfxVolume() => sfxMixer.SetFloat("SfxVolume", sfxVolume);
        
    }
}
