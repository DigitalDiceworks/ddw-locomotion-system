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

        private HashSet<IModifierHandler> _modifiers = new HashSet<IModifierHandler>();

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
                if (onBeginPrimary != null)
                {
                    onBeginPrimary();
                }
            }
            else
            {
                _secondaryInput = input;
                if (onBeginSecondary != null)
                {
                    onBeginSecondary();
                }
            }
        }

        public void EndInput(NaturalInput input)
        {
            if (_primaryInput == input)
            {
                _primaryInput = null;
                _secondaryInput = null;
                if (onEndSecondary != null)
                {
                    onEndSecondary();
                }
                if (onEndPrimary != null)
                {
                    onEndPrimary();
                }
            }
            else
            {
                _secondaryInput = null;
                if (onEndSecondary != null)
                {
                    onEndSecondary();
                }
            }
        }

        public void AddModifier(IModifierHandler modifier)
        {
            _modifiers.Add(modifier);
        }

        public void RemoveModifier(IModifierHandler modifier)
        {
            _modifiers.Remove(modifier);
        }

        public void ToggleModifier(IModifierHandler modifier)
        {
            if (!_modifiers.Add(modifier))
            {
                _modifiers.Remove(modifier);
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
