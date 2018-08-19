namespace NaturalLocomotion
{
    using System;
    using UnityEngine;

    public enum InputMode
    {
        None,
        Move,
        Jump,
        Jumping,
    }

    public class CollisionForwarder : MonoBehaviour
    {
        public event Action<Collision> onCollisionEnter;

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Collision");
            Debug.Log(collision.transform.name);
            if (onCollisionEnter != null)
            {
                onCollisionEnter(collision);
            }
        }
    }

    public class StandardInput : NaturalInput
    {
        [SerializeField] private GameObject _feetObject;
        [SerializeField] private SprintModifier _sprintModifier;
        [SerializeField] private AirControlModifier _airControlModifier;
        [SerializeField] private float _speedModifier;
        [SerializeField] private float _minimumSprintValue;
        [SerializeField] private float _jumpDistance;
        [SerializeField] private float _jumpTimeout;
        [SerializeField] private float _jumpScale;

        private InputMode _mode;

        private Vector3 _jumpStart;
        private float _jumpTimer;
        private Vector3 _jumpDirection;

        private Vector3 _startPosition;
        private SteamVR_TrackedController _trackedController;
        public bool _onGround;
        private bool _padDown;

        protected override void Awake()
        {
            base.Awake();

            _onGround = true;

            _trackedController = GetComponent<SteamVR_TrackedController>();
            _trackedController.PadClicked += PadClickedHandler;
            _trackedController.PadUnclicked += PadUnclickedHandler;

            CollisionForwarder feetCollision = hub.GetComponent<CollisionForwarder>();
            if (feetCollision == null)
            {
                feetCollision = hub.gameObject.AddComponent<CollisionForwarder>();
            }
            feetCollision.onCollisionEnter += OnFeetCollisionHandler;
        }

        private void PadClickedHandler(object sender, ClickedEventArgs e)
        {
            _startPosition = transform.localPosition;
            _mode = InputMode.Move;
            _padDown = true;
        }

        private void PadUnclickedHandler(object sender, ClickedEventArgs e)
        {
            _sprintModifier.enabled = false;
            _mode = _onGround ? InputMode.Jump : InputMode.None;
            _jumpStart = transform.localPosition;
            _jumpTimer = 0f;
            _padDown = false;
        }

        private void Update()
        {
            if (_mode == InputMode.Jump)
            {
                Vector3 delta = transform.localPosition - _jumpStart;
                if (delta.y >= _jumpDistance * _jumpDistance)
                {
                    _onGround = false;
                    _jumpDirection = delta;
                    _mode = InputMode.Jumping;
                    _airControlModifier.enabled = true;
                    return;
                }

                _jumpTimer += Time.deltaTime;
                if (_jumpTimer > _jumpTimeout)
                {
                    _mode = InputMode.None;
                }
            }
            else if (_mode == InputMode.Move)
            {
                _sprintModifier.enabled = _trackedController.controllerState.rAxis0.y > _minimumSprintValue;
            }
        }

        private void OnFeetCollisionHandler(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Floor"))
            {
                _onGround = true;
                _airControlModifier.enabled = false;
                _mode = _padDown ? InputMode.Move : InputMode.None;
            }
        }

        public override void Calculate(Rigidbody rigidbody, float modifier)
        {
            if (_mode == InputMode.Jumping)
            {
                rigidbody.velocity += _jumpDirection / _jumpDistance * _jumpScale / _jumpTimer;
                _mode = InputMode.None;
            }
            else if (_mode == InputMode.Move)
            {
                Vector3 delta = transform.localPosition - _startPosition;
                Vector3 speed = new Vector3(
                    delta.x * _speedModifier * modifier,
                    rigidbody.velocity.y,
                    delta.z * _speedModifier * modifier
                );
                if (_onGround)
                {
                    rigidbody.velocity = speed;
                }
                else
                {
                    speed.y = 0f;
                    rigidbody.velocity += speed;
                }
            }
        }
    }
}