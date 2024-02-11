using Audio;
using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;

namespace Player
{
    public class PlayerView : MonoBehaviour
    {
        [Header("References")]
        [SerializeField]
        private PlayerController playerController;
        [SerializeField]
        private HearingAidView hearingAidView;
        
        [Header("Animation")]
        [SerializeField]
        private float animatorStateSensitivity = 0.5f;
        [Space]
        [SerializeField]
        private TMP_Text interactablePopUp;
        [SerializeField]
        private float interactableAnimationDuration = 1.0f;
        [SerializeField]
        private float interactableEndPosY = 0.5f;
        private float interactableStartPosY;
        
        [Space]
        
        public Transform monsterSpawnPoint;

        [Space]
        
        public GameObject flashlight;
        
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        private readonly int _isWalking = Animator.StringToHash("IsWalking");

        void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();

            interactableStartPosY = interactablePopUp.rectTransform.anchoredPosition.y;
        }

        // Start is called before the first frame update
        void Start()
        {
            playerController.Direction
                .Where(x => x != 0)
                .Subscribe(x =>
                {
                    _spriteRenderer.flipX = x < 0;
                    hearingAidView.FlipX(x < 0);
                    _animator.SetBool(_isWalking, true);
                    AudioManager.Instance.ToggleFootsteps(true);
                })
                .AddTo(this);
            playerController.Direction
                .Where(x => Mathf.Abs(x) < animatorStateSensitivity)
                .Subscribe(_ =>
                {
                    _animator.SetBool(_isWalking, false);
                    AudioManager.Instance.ToggleFootsteps(false);
                })
                .AddTo(this);
        }

        public void AnimateInteractable(bool state)
        {
            interactablePopUp.rectTransform.DOAnchorPosY(
                state ? interactableEndPosY : interactableStartPosY, 
                interactableAnimationDuration);
            interactablePopUp.DOFade(
                state ? 1.0f : 0.0f,
                interactableAnimationDuration);
        }

        public void SetTriggerFoundFlashLight()
        {
            _animator.SetTrigger("FoundFlashLight");
            flashlight.SetActive(true);
        }
    }
}
