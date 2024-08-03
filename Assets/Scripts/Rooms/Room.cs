using Audio;
using DG.Tweening;
using Managers;
using UnityEngine;
using UnityEngine.Events;

namespace Rooms
{
    public class Room : MonoBehaviour
    {
        [Header("RoomArgs")]
        [SerializeField]
        private AudioClip _ambient;

        [Header("Animation")]
        [SerializeField]
        private SpriteRenderer fogRenderer;
        [SerializeField]
        private float fogFadeSpeed = 1.0f;

        [Space]

        [Header("Events")]
        public UnityEvent<PlayerEnterEventArgs> PlayerEnter;
        public UnityEvent PlayerOut;

        public bool IsMonsterHere
        {
            get => _isMonsterHere;
            set
            {
                AudioManager.Instance.SetMonsterAudioAt(transform.position);
                _isMonsterHere = value;
            }
        }
        private bool _isMonsterHere;

        void Awake()
        {
            var color = fogRenderer.color;
            color = new Color(color.r, color.g, color.b, 1.0f);
            fogRenderer.color = color;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            
            FadeOutFog();
                
            PlayerEnter?.Invoke(new PlayerEnterEventArgs()
            {
                cameraPosition = transform.position,
                ambient = _ambient
            });
                
            if (!_isMonsterHere) return;
            FearManager.Instance.AddFear(float.MaxValue);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;
            
            FadeInFog();
                
            PlayerOut?.Invoke();
        }

        private void FadeInFog()
        {
            fogRenderer.DOFade(1.0f, fogFadeSpeed);
        }

        private void FadeOutFog()
        {
            fogRenderer.DOFade(0.0f, fogFadeSpeed);
        }
    }
}
