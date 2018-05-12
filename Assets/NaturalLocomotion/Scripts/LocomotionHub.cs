namespace NaturalLocomotion
{
    using System;
    using UnityEngine;

    public struct InputContainer
    {
        public bool isPrimary;
        public Vector3 direction;
    }

    public delegate void InputDelegate(InputContainer container);

    public class LocomotionHub : MonoBehaviour
    {
        private NaturalInput _primaryInput;
        private NaturalInput _secondaryInput;

        public event InputDelegate onInput;
        public event Action onBeginPrimary;
        public event Action onEndPrimary;
        public event Action onBeginSecondary;
        public event Action onEndSecondary;

        public void BeginInput(NaturalInput input)
        {
            if (_primaryInput == null)
            {
                _primaryInput = input;
                onBeginPrimary?.Invoke();
            }
            else
            {
                _secondaryInput = input;
                onBeginSecondary?.Invoke();
            }
        }

        public void EndInput(NaturalInput input)
        {
            if (_primaryInput == input)
            {
                _primaryInput = null;
                _secondaryInput = null;
                onEndSecondary?.Invoke();
                onEndPrimary?.Invoke();
            }
            else
            {
                _secondaryInput = null;
                onEndSecondary?.Invoke();
            }
        }

        private void Update()
        {
            if (onInput == null)
                return;

            if (_primaryInput != null)
            {
                InputContainer container = new InputContainer
                {
                    isPrimary = true,
                    direction = _primaryInput.GetVector()
                };
                onInput(container);
            }
            if (_secondaryInput != null)
            {
                InputContainer container = new InputContainer
                {
                    isPrimary = false,
                    direction = _secondaryInput.GetVector()
                };
                onInput(container);
            }
        }
    }
}
