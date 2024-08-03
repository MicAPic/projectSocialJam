using DG.Tweening;
using Managers;
using ScriptableObjects;
using UnityEngine;

namespace UI
{
    public class IntroStartSequence : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup logo;
        [SerializeField]
        private float logoAppearanceDuration = 1.0f;
        [SerializeField]
        private float logoStayDuration = 1.0f;
        [SerializeField]
        private float logoDisappearanceDuration = .2f;
    
        [SerializeField]
        private DialogueInfo introDialogue;
    
        [SerializeField]
        private AudioClip introClip;
    
        [SerializeField]
        private GameObject[] activateOnFinish;
    
        // Start is called before the first frame update
        void Start()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(logo.DOFade(1.0f, logoAppearanceDuration));
            sequence.AppendInterval(logoStayDuration);
            sequence.AppendCallback(() => GetComponent<AudioSource>().PlayOneShot(introClip));
            sequence.Append(logo.DOFade(0.0f, logoDisappearanceDuration));

            sequence.AppendInterval(logoStayDuration);
            sequence.AppendCallback(() => GetComponent<AudioListener>().enabled = false);
            sequence.AppendCallback(() =>
            {
                foreach (var obj in activateOnFinish)
                {
                    obj.SetActive(true);
                }
            });
        
            sequence.AppendInterval(logoStayDuration);
            sequence.AppendCallback(() => DialogueManager.Instance.Show(introDialogue));
        }
    }
}
