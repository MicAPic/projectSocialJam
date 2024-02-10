using System;
using UniRx;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public event Action OnInteractPressed;

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
            if (MovementLimiter.Instance.CanMove)
            {
                _directionX.Value = value.Get<float>();
            }
        }
        
        public void OnInteract(InputValue value)
        {
            OnInteractPressed?.Invoke();
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

            _rigidbody.velocity = velocity;
        }
    }
}
