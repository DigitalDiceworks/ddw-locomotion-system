namespace NaturalLocomotion
{
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyMovement : HubConnector
    {
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _sprintModifier;
        [SerializeField] private float _allowedSprintPressTime;

        private Rigidbody _rigidbody;
        private bool _isSprinting;
        private float _beginTime;

        protected override void Awake()
        {
            base.Awake();

            _rigidbody = GetComponent<Rigidbody>();

            hub.onInput += InputHandler;
            hub.onEndPrimary += EndPrimaryHandler;
            hub.onBeginSecondary += BeginSecondaryHandler;
            hub.onEndSecondary += EndSecondaryHandler;
        }

        private void EndPrimaryHandler()
        {
            _isSprinting = false;
            _rigidbody.velocity = Vector3.zero;
        }

        private void BeginSecondaryHandler()
        {
            _beginTime = Time.time;
        }

        private void EndSecondaryHandler()
        {
            if (Time.time - _beginTime < _allowedSprintPressTime)
            {
                _isSprinting = !_isSprinting;
            }
        }

        private void InputHandler(InputContainer input)
        {
            if (!input.isPrimary)
            {
                return;
            }

            Vector3 direction = input.direction * _maxSpeed;
            if (_isSprinting)
            {
                direction *= _sprintModifier;
            }
            _rigidbody.velocity = direction;
        }
    }
}
