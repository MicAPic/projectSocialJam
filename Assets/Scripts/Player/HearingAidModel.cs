using System;
using Audio;
using Managers;
using UniRx;
using UnityEngine;

namespace Player
{
    public class HearingAidModel : MonoBehaviour
    {
        [SerializeField]
        private PlayerController playerController;
        
        [SerializeField, Range(0, 1)]
        private float fearIncrement = 0.0002f;

        public IReadOnlyReactiveProperty<bool> IsUltraSound => _isUltraSound;
        private ReactiveProperty<bool> _isUltraSound = new(false);

        void OnEnable()
        {
            playerController.OnSwitchModePressed += ToggleUltraSound;
        }
        
        void OnDisable()
        {
            playerController.OnSwitchModePressed -= ToggleUltraSound;
        }

        void Update()
        {
            if (!_isUltraSound.Value) return;
            
            FearManager.Instance.AddFear(fearIncrement);
        }

        private void ToggleUltraSound()
        {
            _isUltraSound.Value = !_isUltraSound.Value;
            AudioManager.Instance.ToggleMonsterSounds(_isUltraSound.Value);
        }
    }
}
