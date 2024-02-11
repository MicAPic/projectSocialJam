using System.Linq;
using Rooms;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [SerializeField]
        private AudioMixer audioMixer;
        
        [Header("Audio Sources")]
        [SerializeField]
        private AudioSource bgmPlayer;
        [SerializeField]
        private AudioSource sfxPlayer;
        
        [Header("Settings")]
        [SerializeField]
        private string masterVolumeName;
        [SerializeField]
        private string monsterVolumeName;
        [SerializeField]
        private string bgmVolumeName;
        [SerializeField]
        private float transitionFadeDuration = 1.0f;
        [SerializeField]
        private float monsterFadeDuration = 0.15f;
        [SerializeField]
        private int ambianceCrossFadeDuration;
        [SerializeField]
        private AudioPlayer[] ambiancePlayers = new AudioPlayer[2];

        private const float MaxVolume = 1.0f;
        private const float MinVolume = 0.0f;
        private int _currentAmbianceIndex;
        private int _nextAmbianceIndex;
        private Coroutine _fadeInCoroutine;
        private Coroutine _fadeOutCoroutine;

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(Instance.gameObject);
            }

            Instance = this;
        }

        public void Initialize()
        {
            FadeIn(masterVolumeName, transitionFadeDuration);
            bgmPlayer.Play();
        }

        public void StopBGM()
        {
            bgmPlayer.Stop();
        }

        public void PlaySoundEffect(AudioClip sfx)
        {
            sfxPlayer.PlayOneShot(sfx);
        }

        public void ToggleFootsteps(bool state)
        {
            if (state)
            {
                sfxPlayer.time = 0.0f;
                sfxPlayer.Play();
            }
            else
                sfxPlayer.Stop();
        }

        public void SwitchAmbiances(PlayerEnterEventArgs playerEnterEventArgs)
        {
            var firstPlayer = ambiancePlayers.First();
            if (firstPlayer.AudioSource.clip == null)
            {
                firstPlayer.AudioSource.clip = playerEnterEventArgs.ambient;
                firstPlayer.AudioSource.Play();
                return;
            }
            
            _nextAmbianceIndex = (_currentAmbianceIndex + 1) % ambiancePlayers.Length;
            ambiancePlayers[_nextAmbianceIndex].AudioSource.clip = playerEnterEventArgs.ambient;
            ambiancePlayers[_nextAmbianceIndex].AudioSource.Play();
            CrossFadeAmbiances();
            
            _currentAmbianceIndex = _nextAmbianceIndex;
        }

        public void ToggleMonsterSounds(bool state)
        {
            if (state)
            {
                FadeIn(monsterVolumeName, monsterFadeDuration);
                FadeOut(bgmVolumeName, monsterFadeDuration);
            }
            else
            {
                FadeIn(bgmVolumeName, monsterFadeDuration);
                FadeOut(monsterVolumeName, monsterFadeDuration);
            }
        }

        private void CrossFadeAmbiances()
        {
            // fade out current ambiance source
            if (_fadeOutCoroutine != null)
                StopCoroutine(_fadeOutCoroutine);
            Debug.Log($"1) {_currentAmbianceIndex}");
            FadeOut(ambiancePlayers[_currentAmbianceIndex].exposedVolumeName, ambianceCrossFadeDuration);

            // fade in next ambiance source
            if (_fadeInCoroutine != null)
                StopCoroutine(_fadeInCoroutine);
            Debug.Log($"2) {_nextAmbianceIndex}");
            FadeIn(ambiancePlayers[_nextAmbianceIndex].exposedVolumeName, ambianceCrossFadeDuration);
        }

        private void FadeIn(string exposedVolumeName, float duration)
        {
            _fadeInCoroutine = StartCoroutine(FadeMixerGroup.StartFade(
                audioMixer,
                exposedVolumeName,
                duration,
                MaxVolume
            ));
        }

        private void FadeOut(string exposedVolumeName, float duration)
        {
            _fadeOutCoroutine = StartCoroutine(FadeMixerGroup.StartFade(
                audioMixer,
                exposedVolumeName,
                duration,
                MinVolume
            ));
        }
    }
}