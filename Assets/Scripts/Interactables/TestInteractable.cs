using UnityEngine;

namespace Interactables
{
    public class TestInteractable : InteractableBase
    {
        protected override void Interact()
        {
            Debug.Log($"Interact called on {gameObject.name}");
        }
    }
}
