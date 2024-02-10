using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public static class FadeMixerGroup 
    {
        public static IEnumerator StartFade(AudioMixer audioMixer, string exposedParam, float duration, float targetVolume)
        {
            var currentTime = 0.0f;
            audioMixer.GetFloat(exposedParam, out var currentVolume);
            currentVolume = Mathf.Pow(10.0f, currentVolume / 20.0f);
            var targetValue = Mathf.Clamp(targetVolume, 0.0001f, 1.0f);
        
            while (currentTime < duration)
            {
                currentTime += Time.unscaledDeltaTime;
                var newVolume = Mathf.Lerp(currentVolume, targetValue, currentTime / duration);
                audioMixer.SetFloat(exposedParam, Mathf.Log10(newVolume) * 20.0f);
                yield return null;
            }
        }
    }
}