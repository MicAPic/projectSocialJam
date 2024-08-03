using Audio;
using Monster;
using UniTools.Extensions;
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
                monster.GetComponent<MonsterAnimationController>().SetAnimatorDeath();
                Debug.Log("LastWord");
            }
            else
            {
                monster.GetComponent<MonsterAnimationController>().SetAnimatorTakeDamage();
                Debug.Log("Damage");
            }
            AudioManager.Instance.PlaySoundEffect(damageSound);

            if (_nextItem.IsNotNull())
                _nextItem.MakeUsable();
            
            canUse = false;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
