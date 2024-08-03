using DG.Tweening;
using Managers;
using Player;
using UniTools.Extensions;
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

        [SerializeField]
        private BoxCollider2D _leftStairs;
        [SerializeField]
        private BoxCollider2D _rightStairs;

        [SerializeField]
        private PlayerView _playerView;


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
        
        private bool interactFirstTime;
        protected override void Interact()
        {
            base.Interact();
            if (!interactFirstTime)
            {
                if (TryGetComponent(out SpriteRenderer spriteRenderer))
                {
                    spriteRenderer.transform.DOMove(transform.position - fadeOffset, fadeDuration);
                    spriteRenderer
                        .DOColor(Color.clear, fadeDuration)
                        .OnComplete(() => spriteRenderer.enabled = false);
                }
                
                if (_nextItem.IsNotNull())
                    _nextItem.MakeUsable();
                
                _monsterManager.MoveNextRoom();
                interactFirstTime = true;
                if (gameObject.name == "Key")
                {
                    _leftStairs.enabled = true;
                    _rightStairs.enabled = true;
                    FearManager.Instance.PlayerFoundFlashLight();
                }
                if (gameObject.name == "Flashlight")
                {
                    _playerView.SetTriggerFoundFlashLight();
                }
            }
        }
    }
}
