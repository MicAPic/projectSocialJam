using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "SfxHolder", menuName = "ScriptableObject/SfxHolder", order = 2)]
    public class StaircaseSfxHolder : ScriptableObject
    {
        [SerializeField]
        private AudioClip[] staircaseAudioClips;

        public AudioClip GetRandomStaircaseClip() => staircaseAudioClips[Random.Range(0, staircaseAudioClips.Length)];
    }
}