using Managers;
using ScriptableObjects;
using UnityEngine;

namespace Interactables
{
    public class DialogueInteractable : InteractableBase
    {
        [SerializeField]
        private DialogueInfo dialogueInfo; 
            
        protected override void Interact()
        {
            DialogueManager.Instance.Show(dialogueInfo);
        }
    }
}
