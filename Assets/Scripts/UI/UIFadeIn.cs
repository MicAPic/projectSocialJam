using DG.Tweening;
using UnityEngine;

namespace UI
{
    public class UIFadeIn : MonoBehaviour
    {
        [SerializeField]
        private float transitionTime = 1.0f;
        [SerializeField]
        private float delayTime = 5.0f;

        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.DOFade(1.0f, transitionTime).SetDelay(delayTime);
        }
    }
}
