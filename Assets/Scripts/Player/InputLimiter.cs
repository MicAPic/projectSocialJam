using UnityEngine;

namespace Player
{
    public class InputLimiter : MonoBehaviour
    {
        public static InputLimiter Instance { get; private set; }

        public bool CanMove { get; private set; } = true;
        public bool CanSwitch { get; private set; } = true;

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void LimitInput(bool isOn)
        {
            CanMove = !isOn;
            CanSwitch = !isOn;
        }
    }
}