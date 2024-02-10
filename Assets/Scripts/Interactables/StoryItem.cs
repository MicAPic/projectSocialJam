using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class StoryItem : DialogueInteractable
    {
        [SerializeField]
        private InteractableBase _nextItem;

        [SerializeField]
        private MonsterManager _monsterManager;

        private void Start()
        {
            if(gameObject.name == "ParentsBed")
                canUse = true;
        }

        protected override void OnTriggerEnter2D(Collider2D col)
        {
            if (canUse)
            {
                if (!col.CompareTag("Player")) return;
                col.GetComponent<PlayerController>().OnInteractPressed += Interact;
                _isActive.Value = true;
            }
        }

        protected override void Interact()
        {
            base.Interact();
            if(TryGetComponent(out SpriteRenderer spriteRenderer))
            {
                spriteRenderer.enabled = false;
            }
            _nextItem?.MakeUsable();
            canUse = false;
            _monsterManager.MoveNextRoom();
            //TODO:диалог: что делать дальше
        }

        
    }
}
