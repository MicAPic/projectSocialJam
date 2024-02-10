using System;
using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioPlayer : MonoBehaviour
    {
        [Header("Settings")]
        public string exposedVolumeName;

        void Awake()
        {
            // audioMixer.SetFloat(exposedParam, Mathf.Log10(newVolume) * 20.0f);
        }
    }
}