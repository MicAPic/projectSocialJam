using System.Collections.Generic;
using Interactables;
using Player;
using UniRx;
using UnityEngine;

namespace Managers
{
    public class InteractableViewManager : MonoBehaviour
    {
        public static InteractableViewManager Instance { get; private set; }

        [SerializeField]
        private PlayerView playerView;
        
        private CompositeDisposable _disposables = new();

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        void OnDisable()
        {
            Dispose();
        }

        public void RegisterInteractable(InteractableBase interactable)
        {
            interactable.IsActive
                .Subscribe(x => playerView.AnimateInteractable(x))
                .AddTo(_disposables);
        }

        private void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
