using DG.Tweening;
using Player;
using UnityEngine;

namespace Interactables
{
    public class StoryItem : DialogueInteractable
    {
        [SerializeField]
        private InteractableBase _nextItem;
        [SerializeField]
        private MonsterManager _monsterManager;
        
        [SerializeField] 
        private float fadeDuration = .2f;
        [SerializeField] 
        private Vector3 fadeOffset = Vector3.down;

        protected override void Start()
        {
            base.Start();
            if (gameObject.name == "ParentsBed")
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
        private bool InteractFirstTime = false;
        protected override void Interact()
        {
            base.Interact();
            if (!InteractFirstTime)
            {
                if (TryGetComponent(out SpriteRenderer spriteRenderer))
                {
                    spriteRenderer.transform.DOMove(transform.position - fadeOffset, fadeDuration);
                    spriteRenderer
                        .DOColor(Color.clear, fadeDuration)
                        .OnComplete(() => spriteRenderer.enabled = false);
                }
                _nextItem?.MakeUsable();
                _monsterManager.MoveNextRoom();
                InteractFirstTime = true;

            }
            //if(gameObject.name == "Key")
            //{

            //}
        }

        
    }
}
