using UniRx;
using UnityEngine;

namespace Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField]
        private PlayerController playerController;
        private SpriteRenderer _spriteRenderer;
        
        void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        // Start is called before the first frame update
        void Start()
        {
            playerController.Direction
                .Where(x => x != 0)
                .Subscribe(x => _spriteRenderer.flipY = x < 0)
                .AddTo(this);
        }

        // Update is called once per frame
        // void Update()
        // {
        //
        // }
    }
}
