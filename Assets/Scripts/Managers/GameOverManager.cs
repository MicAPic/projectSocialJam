using Audio;
using Player;
using UnityEngine;

namespace Managers
{
    public class GameOverManager : MonoBehaviour
    {
        public static GameOverManager Instance { get; private set; }

        public bool IsGameOver { get; private set; }
        
        [SerializeField]
        private Animator lightsAnimator;
        [SerializeField]
        private PlayerView playerView;
        [SerializeField]
        private GameObject monsterPrefab;

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void GameOver()
        {
            if (IsGameOver) return;
            
            IsGameOver = true;
            lightsAnimator.SetTrigger("Blink");
            InputLimiter.Instance.LimitInput(true);
            AudioManager.Instance.StopBGM();

            Instantiate(monsterPrefab, playerView.monsterSpawnPoint);
            
            Debug.Log("Game Over Triggered");
        }
    }
}
