namespace NaturalLocomotion
{
    using UnityEngine;

    /// <summary>
    /// Moves the player using a rigidbodys velocity allowing the player
    /// to interact with physics.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyMovement : HubConnector
    {
        [Header("How fast the user should move without modifiers"), SerializeField]
        private float _maxSpeed;

        private Rigidbody _rigidbody;

        protected override void Awake()
        {
            base.Awake();

            _rigidbody = GetComponent<Rigidbody>();

            hub.onInput += InputHandler;
            hub.onEndPrimary += EndPrimaryHandler;
        }

        private void EndPrimaryHandler()
        {
            _rigidbody.velocity = Vector3.zero;
        }

        private void InputHandler(InputContainer input)
        {
            if (!input.isPrimary)
            {
                return;
            }

            Vector3 direction = input.direction * _maxSpeed;
            _rigidbody.velocity = direction;
        }
    }
}
