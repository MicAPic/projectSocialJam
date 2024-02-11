using DG.Tweening;
using Managers;
using UnityEngine;
using UnityEngine.Events;

namespace Rooms
{
    public class RoomEnterHandler : MonoBehaviour
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
                if (roomAudioSource != null)
                    roomAudioSource.enabled = value;
                _isMonsterHere = value;
            }
        }
        private bool _isMonsterHere;
        
        private AudioSource roomAudioSource;

        void Awake()
        {
            roomAudioSource = GetComponentInChildren<AudioSource>();
            roomAudioSource.enabled = false;

            var color = fogRenderer.color;
            color = new Color(color.r, color.g, color.b, 1.0f);
            fogRenderer.color = color;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Player")) return;
            
            FadeOut();
                
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
            
            FadeIn();
                
            PlayerOut?.Invoke();
        }

        private void FadeIn()
        {
            fogRenderer.DOFade(1.0f, fogFadeSpeed);
        }

        private void FadeOut()
        {
            fogRenderer.DOFade(0.0f, fogFadeSpeed);
        }
    }
}
