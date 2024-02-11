using UnityEngine;

namespace Managers
{
    public class GameOverManager : MonoBehaviour
    {
        public static GameOverManager Instance { get; private set; }

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
            // instantiate death VFX here
            Debug.Log("Game Over Triggered");
        }
    }
}
