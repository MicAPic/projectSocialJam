using Audio;
using DG.Tweening;
using Managers;
using Player;
using UnityEngine;

namespace Interactables
{
    public class HearingAidItem : DialogueInteractable
    {
        [SerializeField]
        private GameObject sideItemsHolder;
        [SerializeField]
        private GameObject hearingAidOnPlayer;
        [SerializeField]
        private GameObject exitWallBlock;
        
        [Header("Animation")]
        [SerializeField] 
        private Vector3 loopOffset = Vector3.up;
        [SerializeField] 
        private float loopDuration = 1.0f;
        [SerializeField] 
        private float fadeDuration = .2f;
        [SerializeField] 
        private Vector3 fadeOffset = Vector3.down;
        
        private Tween loopAnimation;

        void Awake()
        {
            canUse = true;
            
            var currentPosition = transform.position;
            Vector3[] waypoints = {currentPosition, currentPosition + loopOffset, currentPosition};

            loopAnimation = transform.DOPath(waypoints, loopDuration, PathType.Linear, PathMode.Sidescroller2D)
                .SetLoops(-1)
                .SetEase(Ease.Linear);
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
            loopAnimation.Pause();
            
            if (TryGetComponent(out SpriteRenderer spriteRenderer))
            {
                spriteRenderer.transform.DOMove(transform.position - fadeOffset, fadeDuration);
                spriteRenderer
                    .DOColor(Color.clear, fadeDuration)
                    .OnComplete(() => gameObject.SetActive(false));
            }
            
            canUse = false;
            hearingAidOnPlayer.SetActive(true);
            AudioManager.Instance.Initialize();
            exitWallBlock.SetActive(false);

            DialogueManager.Instance.OnDialogueFinished += ActivateSideItems;
        }

        private void ActivateSideItems()
        {
            sideItemsHolder.SetActive(true);
            DialogueManager.Instance.OnDialogueFinished -= ActivateSideItems;
        }
    }
}
