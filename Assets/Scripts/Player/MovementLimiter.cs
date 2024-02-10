using UnityEngine;

namespace Player
{
    public class MovementLimiter : MonoBehaviour
    {
        public static MovementLimiter Instance { get; private set; }

        public bool CanMove { get; } = true;

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }
    }
}