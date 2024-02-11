using System;
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
        }

        // void Start()
        // {
        //     
        // }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player.PlayerController playerController))
            {
                PlayerEnter?.Invoke(new PlayerEnterEventArgs()
                {
                    cameraPosition = transform.position,
                    ambient = _ambient
                });
                
                if (!_isMonsterHere) return;
                FearManager.Instance.AddFear(float.MaxValue);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player.PlayerController playerController))
            {
                PlayerOut?.Invoke();
            }
        }
    }
}
