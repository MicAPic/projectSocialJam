using System;
using UniRx;
using UnityEngine;

namespace Managers
{
    public class FearManager : MonoBehaviour
    {
        public static FearManager Instance { get; private set; }
        
        public IReadOnlyReactiveProperty<float> Fear => _currentFear;
        private ReactiveProperty<float> _currentFear;

        [SerializeField]
        private float maxFearValue = 100.0f;
        [SerializeField, Range(0, 1)]
        private float cooldownDecrement = 0.001f;

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            
            _currentFear = new ReactiveProperty<float>(0.0f);
        }

        void LateUpdate()
        {
            // decrease fear with time
            _currentFear.Value = Mathf.Clamp(_currentFear.Value - cooldownDecrement, 0.0f, maxFearValue);
        }

        public void AddFear(float value)
        {
            _currentFear.Value += value;
            if (_currentFear.Value >= maxFearValue)
            {
                GameOverManager.Instance.GameOver();
            }
        }

        public float GetFearPercent()
        {
            return _currentFear.Value / maxFearValue;
        }
    }
}
