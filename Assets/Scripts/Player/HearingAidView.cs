using UniRx;
using UnityEngine;

namespace Player
{
    public class HearingAidView : MonoBehaviour
    {
        [SerializeField]
        private Sprite[] hearingAidSprites;
        [SerializeField]
        private GameObject lightIndicator;
        
        private HearingAidModel _hearingAidModel;
        private SpriteRenderer _spriteRenderer;

        void Awake()
        {
            _hearingAidModel = GetComponent<HearingAidModel>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Start()
        {
            _hearingAidModel.IsUltraSound
                .Subscribe(ToggleHearingAid)
                .AddTo(this);
        }
        
        public void FlipX(bool flip) => transform.localScale = new Vector3(flip ? -1.0f : 1.0f, 1.0f, 1.0f);

        private void ToggleHearingAid(bool state)
        {
            var index = state ? 1 : 0;
            _spriteRenderer.sprite = hearingAidSprites[index];
            lightIndicator.SetActive(state);
        }
    }
}
