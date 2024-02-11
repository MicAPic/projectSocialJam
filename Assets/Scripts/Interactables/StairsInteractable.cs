using Audio;
using ScriptableObjects;
using UnityEngine;

namespace Interactables
{
    public class StairsInteractable : InteractableBase
    {
        [SerializeField]
        private Transform destination;
        [SerializeField]
        private StaircaseSfxHolder sfxHolder;
        
        private Transform _player;
        
        protected override void OnTriggerEnter2D(Collider2D col)
        {
            base.OnTriggerEnter2D(col);
            _player = col.transform;
        }
        
        protected override void Interact()
        {
            _player.position = destination.position;
            AudioManager.Instance.PlaySoundEffect(sfxHolder.GetRandomStaircaseClip());
        }
    }
}
