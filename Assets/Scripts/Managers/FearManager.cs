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

        public void AddFear(float value)
        {
            _currentFear.Value += value;
            if (_currentFear.Value >= maxFearValue)
            {
                throw new NotImplementedException("Death logic is not implemented yet");
            }
        }
    }
}
