using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [SerializeField]
        private AudioMixer audioMixer;
        
        [Header("Settings")]
        [SerializeField]
        private string masterVolumeName;
        [SerializeField]
        private int ambianceCrossFadeDuration;
        [SerializeField]
        private AudioPlayer[] ambiancePlayers = new AudioPlayer[2];

        private const float MaxVolume = 0.0f;
        private const float MinVolume = 1.0f;
        private int _currentAmbianceIndex;

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(Instance.gameObject);
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        // private void Update()
        // {
        //     if (Keyboard.current[Key.Space].wasPressedThisFrame)
        //     {
        //         CrossFadeAmbiances();
        //     }
        // }

        public void CrossFadeAmbiances()
        {
            // fade out current ambiance source
            FadeOut(ambiancePlayers[_currentAmbianceIndex].exposedVolumeName, ambianceCrossFadeDuration);

            // fade in next ambiance source
            var nextAmbianceIndex = (_currentAmbianceIndex + 1) % ambiancePlayers.Length;
            FadeIn(ambiancePlayers[nextAmbianceIndex].exposedVolumeName, ambianceCrossFadeDuration);

            _currentAmbianceIndex = nextAmbianceIndex;
        }

        private void FadeIn(string exposedVolumeName, float duration)
        {
            StartCoroutine(FadeMixerGroup.StartFade(
                audioMixer,
                exposedVolumeName,
                duration,
                MaxVolume
            ));
        }

        private void FadeOut(string exposedVolumeName, float duration)
        {
            StartCoroutine(FadeMixerGroup.StartFade(
                audioMixer,
                exposedVolumeName,
                duration,
                MinVolume
            ));
        }
    }
}