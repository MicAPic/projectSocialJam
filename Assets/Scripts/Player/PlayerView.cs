using UniRx;
using UnityEngine;

namespace Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField]
        private PlayerController playerController;
        [SerializeField]
        private float animatorStateSensitivity = 0.5f;
        [SerializeField]
        private HearingAidView hearingAidView;
        
        private SpriteRenderer _spriteRenderer;
        private Animator _animator;
        private readonly int _isWalking = Animator.StringToHash("IsWalking");

        void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _animator = GetComponent<Animator>();
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
                })
                .AddTo(this);
            playerController.Direction
                .Where(x => Mathf.Abs(x) < animatorStateSensitivity)
                .Subscribe(x => _animator.SetBool(_isWalking, false))
                .AddTo(this);
        }

        // Update is called once per frame
        // void Update()
        // {
        //
        // }
    }
}
