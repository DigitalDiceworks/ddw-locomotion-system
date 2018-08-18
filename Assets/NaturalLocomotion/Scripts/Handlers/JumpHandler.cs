namespace NaturalLocomotion
{
    using System;
    using UnityEngine;

    /// <summary>
    /// Moves the player using a rigidbodys velocity allowing the player
    /// to interact with physics.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class JumpHandler : HubConnector, IModifierHandler
    {
        [SerializeField] private Transform _headTransform;

        [SerializeField] private float _distanceToJump;
        [SerializeField] private float _verticalAdditive;
        [SerializeField] private float _jumpMultiplier;
        [SerializeField] private float _jumpLimit;
        [SerializeField] private float _airStrafeAmount;
        [SerializeField] private float _feetSize;

        private float _startTime;
        private Rigidbody _rigidbody;
        private bool _inAir;
        private SphereCollider _feetCollider;

        protected override void Awake()
        {
            base.Awake();

            _rigidbody = GetComponent<Rigidbody>();

            GameObject feetObject = new GameObject("Feet Collider");
            _feetCollider = feetObject.AddComponent<SphereCollider>();
            _feetCollider.radius = _feetSize;
            feetObject.transform.SetParent(_headTransform, true);
            feetObject.SetActive(false);

            ColliderEventForwarder forwarder = feetObject.AddComponent<ColliderEventForwarder>();
            forwarder.onCollisionEnter += FeetCollisionHandler;

            hub.onInput += InputHandler;
            hub.onBeginSecondary += BeginSecondaryHandler;
        }

        private void BeginSecondaryHandler()
        {
            _startTime = Time.time;
        }

        private void InputHandler(InputContainer input)
        {
            if (input.isPrimary || _inAir)
            {
                return;
            }

            float distSquared = input.direction.sqrMagnitude;
            Debug.Log(distSquared);
            if (distSquared > _distanceToJump * _distanceToJump)
            {
                float jumpTime = Time.time - _startTime;
                Vector3 force = input.direction / jumpTime * _jumpMultiplier;
                force.y += _verticalAdditive;
                if (force.sqrMagnitude > _jumpLimit * _jumpLimit)
                {
                    force.Normalize();
                    force *= _jumpLimit;
                }
                Debug.Log("Jumping!");
                Debug.Log(force);
                input.direction = force;
                ToggleFeet();
            }
        }

        private void ToggleFeet()
        {
            _inAir = !_inAir;
            _feetCollider.gameObject.SetActive(!_feetCollider.gameObject.activeSelf);
            hub.ToggleModifier(this);
        }

        private void FeetCollisionHandler(Collision collision)
        {
            ToggleFeet();
        }

        private void Update()
        {
            if (!_inAir)
            {
                return;
            }

            // keep the feet collider on the ground
            Vector3 headPosition = _headTransform.position;
            // add 0.5f which is half the 1 unit radius
            headPosition.y = transform.position.y + _feetSize * 0.5f;
            _feetCollider.transform.localPosition = headPosition;
        }

        public Vector3 ModifyDirection(Vector3 direction)
        {
            direction.x *= _airStrafeAmount;
            direction.z *= _airStrafeAmount;
            return direction;
        }
    }

    public class ColliderEventForwarder : MonoBehaviour
    {
        public event Action<Collision> onCollisionEnter;

        private void OnCollisionEnter(Collision collision)
        {
            if (onCollisionEnter != null)
            {
                onCollisionEnter(collision);
            }
        }
    }
}
