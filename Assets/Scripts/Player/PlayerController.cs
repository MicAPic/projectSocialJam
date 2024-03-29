using System;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [SelectionBase]
    public class PlayerController : MonoBehaviour
    {
        public event Action OnInteractPressed;
        public event Action OnSwitchModePressed;

        [Header("Movement Stats")]
        [SerializeField, Range(0f, 20f)][Tooltip("Maximum movement speed")]
        public float maxSpeed = 10f;
        
        public IReadOnlyReactiveProperty<float> Direction => _directionX;
        private ReactiveProperty<float> _directionX;
        private Vector2 desiredVelocity;
        private Vector2 velocity;

        private Rigidbody2D _rigidbody;

        void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();

            _directionX = new ReactiveProperty<float>(0);
        }
        
        public void OnMove(InputValue value)
        {
            if (InputLimiter.Instance.CanMove)
            {
                _directionX.Value = value.Get<float>();
            }
        }
        
        public void OnInteract(InputValue value)
        {
            OnInteractPressed?.Invoke();
        }
        
        public void OnSwitchMode(InputValue value)
        {
            if (InputLimiter.Instance.CanSwitch)
            {
                OnSwitchModePressed?.Invoke();
            }
        }
        
        // Start is called before the first frame update
        // void Start()
        // {
        //
        // }

        // Update is called once per frame
        void Update()
        {
            desiredVelocity = new Vector2(_directionX.Value, 0f) * Mathf.Max(maxSpeed, 0f);
        }

        void FixedUpdate()
        {
            velocity = _rigidbody.velocity;
            Run();
        }

        private void Run()
        {
            velocity.x = desiredVelocity.x;
            if (InputLimiter.Instance.CanMove)
                _rigidbody.velocity = velocity;
            else
            {
                _rigidbody.velocity = Vector2.zero;
                _directionX.Value = 0.0f;
            }
        }
    }
}
