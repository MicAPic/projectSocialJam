using Managers;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FearBar : MonoBehaviour
    {
        [Header("Visuals")]
        [SerializeField]
        private Image barFill;
        [SerializeField]
        private TMP_Text fearText;

        // Start is called before the first frame update
        void Start()
        {
            FearManager.Instance.Fear
                .Subscribe(_ => barFill.fillAmount = FearManager.Instance.GetFearPercent())
                .AddTo(this);
        }
    }
}
