using UnityEngine;

namespace Interactables
{
    public class StairsInteractable : InteractableBase
    {
        [SerializeField]
        private Transform destination;
        private Transform _player;
        
        protected override void OnTriggerEnter2D(Collider2D col)
        {
            base.OnTriggerEnter2D(col);
            _player = col.transform;
        }
        
        protected override void Interact()
        {
            _player.position = destination.position;
        }
    }
}
