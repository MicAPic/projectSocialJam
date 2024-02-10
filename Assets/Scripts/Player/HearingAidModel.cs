using Audio;
using UniRx;
using UnityEngine;

namespace Player
{
    public class HearingAidModel : MonoBehaviour
    {
        [SerializeField]
        private PlayerController playerController;

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

        private void ToggleUltraSound()
        {
            _isUltraSound.Value = !_isUltraSound.Value;
            AudioManager.Instance.ToggleMonsterSounds(_isUltraSound.Value);
        }
    }
}
