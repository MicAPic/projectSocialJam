using Audio;
using Managers;
using UnityEngine;

namespace Interactables
{
    public class KeyItem : StoryItem
    {
        [SerializeField]
        private AudioClip keyClip;
        
        protected override void Interact()
        {
            base.Interact();
            FearManager.Instance.PlayerFoundFlashLight();
        }

        protected override void PlaySfx()
        {
            AudioManager.Instance.PlaySoundEffect(keyClip);
        }
    }
}