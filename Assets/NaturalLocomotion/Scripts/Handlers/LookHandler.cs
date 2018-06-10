namespace NaturalLocomotion
{
    using UnityEngine;

    /// <summary>
    /// Moves the player by translating the transform, will not allow
    /// for interacting with physics.
    /// </summary>
    public class LookHandler : HubConnector
    {
        [Header("How fast the user will rotate"), SerializeField]
        private float _maxSpeed;
        [SerializeField] private Transform _head;

        protected override void Awake()
        {
            base.Awake();

            hub.onInput += InputHandler;
        }

        private void InputHandler(InputContainer input)
        {
            if (input.isPrimary)
            {
                return;
            }

            float angle = input.direction.y * _maxSpeed * Time.deltaTime;
            transform.RotateAround(_head.position, Vector3.up, angle);
        }
    }
}
