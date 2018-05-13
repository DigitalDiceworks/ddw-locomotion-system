namespace NaturalLocomotion
{
    using UnityEngine;

    public class TranslationMovement : HubConnector
    {
        [SerializeField] private float _maxSpeed;

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
