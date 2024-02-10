using UnityEngine;

namespace Player
{
    public class InputLimiter : MonoBehaviour
    {
        public static InputLimiter Instance { get; private set; }

        public bool CanMove { get; } = true;
        public bool CanSwitch { get; } = true;

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