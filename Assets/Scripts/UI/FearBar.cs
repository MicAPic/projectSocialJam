using Managers;
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

        // Start is called before the first frame update
        void Start()
        {
            FearManager.Instance.Fear
                .Subscribe(_ => barFill.fillAmount = FearManager.Instance.GetFearPercent())
                .AddTo(this);
        }

        // Update is called once per frame
        // void Update()
        // {
        //
        // }
    }
}
