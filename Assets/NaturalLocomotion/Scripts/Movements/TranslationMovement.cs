namespace NaturalLocomotion
{
    using UnityEngine;

    /// <summary>
    /// Moves the player by translating the transform, will not allow
    /// for interacting with physics.
    /// </summary>
    public class TranslationMovement : HubConnector
    {
        [Header("How fast the user should move without modifiers"), SerializeField]
        private float _maxSpeed;

        protected override void Awake()
        {
            base.Awake();

            hub.onInput += InputHandler;
        }

        private void InputHandler(InputContainer input)
        {
            if (!input.isPrimary)
            {
                return;
            }

            Vector3 direction = input.direction * _maxSpeed * Time.deltaTime;
            transform.Translate(direction);
        }
    }
}
