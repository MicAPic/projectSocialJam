using UnityEngine;

namespace Player
{
    public class HearingAidView : MonoBehaviour
    {
        [SerializeField]
        private Sprite[] hearingAidSprites;

        private int _current;
        private SpriteRenderer _spriteRenderer;

        void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void ToggleNextHearingAid()
        {
            _current = (_current + 1) % hearingAidSprites.Length;
            _spriteRenderer.sprite = hearingAidSprites[_current];
        }

        public void FlipX(bool flip) => _spriteRenderer.flipX = flip;
    }
}
