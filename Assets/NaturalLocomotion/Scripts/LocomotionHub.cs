namespace NaturalLocomotion
{
    using System;
    using System.Collections.Generic;
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

        private List<IModifierHandler> _modifiers = new List<IModifierHandler>();

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

        public void AddModifier(IModifierHandler modifier)
        {
            if (!_modifiers.Contains(modifier))
            {
                _modifiers.Add(modifier);
            }
        }

        public void RemoveModifier(IModifierHandler modifier)
        {
            if (_modifiers.Contains(modifier))
            {
                _modifiers.Remove(modifier);
            }
        }

        public void ToggleModifier(IModifierHandler modifier)
        {
            if (_modifiers.Contains(modifier))
            {
                _modifiers.Remove(modifier);
            }
            else
            {
                _modifiers.Add(modifier);
            }
        }

        private Vector3 CalculateDirectionWithModifiers(Vector3 inputDirection)
        {
            Vector3 direction = inputDirection;
            foreach(IModifierHandler modifier in _modifiers)
            {
                direction = modifier.ModifyDirection(direction);
            }
            return direction;
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
                    direction = CalculateDirectionWithModifiers(_primaryInput.GetVector())
                };
                onInput(container);
            }
            if (_secondaryInput != null)
            {
                InputContainer container = new InputContainer
                {
                    isPrimary = false,
                    direction = CalculateDirectionWithModifiers(_secondaryInput.GetVector())
                };
                onInput(container);
            }
        }
    }
}
