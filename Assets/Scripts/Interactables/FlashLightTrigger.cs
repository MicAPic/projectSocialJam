using Audio;
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

        [SerializeField]
        private GameObject monsterPrefab;

        [SerializeField]
        private Transform monsterPosition;

        [SerializeField]
        private AudioClip damageSound;


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
            var monster = Instantiate(monsterPrefab, monsterPosition);
            if (gameObject.name == "AtticFlashLightTrigger")
            {
                monster.GetComponent<MonsterController>().SetAnimatorDeath();
                Debug.Log("LastWord");
            }
            else
            {
                monster.GetComponent<MonsterController>().SetAnimatorTakeDamage();
                Debug.Log("Damage");
            }
            AudioManager.Instance.PlaySoundEffect(damageSound);
            _nextItem?.MakeUsable();
            canUse = false;
            GetComponent<BoxCollider2D>().enabled = false;
            //TODO:add effect monsterdamage + disapearedt
        }
    }
}
