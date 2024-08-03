using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : MonoBehaviour
    {
        [Header("Settings")]
        public string exposedVolumeName;

        public AudioSource AudioSource { get; private set; }

        void Awake()
        {
            AudioSource = GetComponent<AudioSource>();
        }
    }
}