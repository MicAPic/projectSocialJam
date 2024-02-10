using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interactables
{
    public class FlashLightTrigger : InteractableBase
    {
        [SerializeField]
        private InteractableBase _nextItem;

        [SerializeField]
        private MonsterManager _monsterManager;

        protected override void OnTriggerEnter2D(Collider2D col)
        {
            if (canUse)
            {
                if (!col.CompareTag("Player")) return;
                Interact();
            }
        }
        protected override void Interact()
        {
            _monsterManager.MoveNextRoom();
            _nextItem?.MakeUsable();
            canUse = false;
            gameObject.SetActive(false);
        }
    }
}