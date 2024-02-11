using Player;
using UnityEngine;

namespace Managers
{
    public class GameOverManager : MonoBehaviour
    {
        public static GameOverManager Instance { get; private set; }

        [SerializeField]
        private Animator lightsAnimator;
        private bool _isGameOver;

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
            if (_isGameOver) return;
            
            _isGameOver = true;
            lightsAnimator.SetTrigger("Blink");
            InputLimiter.Instance.LimitInput(true);
            
            Debug.Log("Game Over Triggered");
        }
    }
}
