using Player;
using UniRx;
using UnityEngine;

namespace Interactables
{
    [RequireComponent(typeof(Collider2D))]
    public abstract class InteractableBase : MonoBehaviour
    {
        public IReadOnlyReactiveProperty<bool> IsActive => _isActive;
        protected ReactiveProperty<bool> _isActive = new();

        protected virtual void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.CompareTag("Player")) return;
            col.GetComponent<PlayerController>().OnInteractPressed += Interact;
            _isActive.Value = true;
        }
        
        protected virtual void OnTriggerExit2D(Collider2D col)
        {
            if (!col.CompareTag("Player")) return;
            col.GetComponent<PlayerController>().OnInteractPressed -= Interact;
            _isActive.Value = false;
        }

        protected abstract void Interact();
    }
}
