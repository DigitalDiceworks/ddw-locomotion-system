namespace NaturalLocomotion
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// Structure holding the data from inputs
    /// </summary>
    public struct InputContainer
    {
        public bool isPrimary;
        public Vector3 direction;
    }

    /// <summary>
    /// Delegate to handle inputs
    /// </summary>
    /// <param name="container"></param>
    public delegate void InputDelegate(InputContainer container);

    /// <summary>
    /// Provides callbacks and events for inputs, modifiers and movements.
    /// Inputs can register themeselves by using Begin and End input.
    /// Movements can respond to inputs by adding to the onInput delegate.
    /// Modifiers can register themeselves using Add, Toggle and Remove Modifier.
    /// Registering is a little easier if you inherit from HubConnector and place
    /// your game object as a child to the LocomotionHub.
    /// 
    /// Primary and secondary inputs are calculated separately, the input container
    /// specifies whether or not the input is from the primary or secondary input.
    /// 
    /// In order to calculate the final vector the following happens:
    /// Take the original input vector
    /// Go through each modifier setting the input vector as the return value of the modifier
    /// Then pass that into the onInput callback
    /// </summary>
    public class LocomotionHub : MonoBehaviour
    {
        private NaturalInput _primaryInput;
        private NaturalInput _secondaryInput;

        private HashSet<IModifierHandler> _modifiers = new HashSet<IModifierHandler>();

        /// <summary>
        /// Delegate callback for handling input data
        /// </summary>
        public event InputDelegate onInput;

        /// <summary>
        /// Callback fired when a new primary input is started
        /// </summary>
        public event Action onBeginPrimary;

        /// <summary>
        /// Callback fired when the primary input is ended
        /// </summary>
        public event Action onEndPrimary;

        /// <summary>
        /// Callback fired when a new secondary input is started
        /// </summary>
        public event Action onBeginSecondary;

        /// <summary>
        /// Callback fired when the secondary input is ended, or if
        /// the primary input ends while secondary is active
        /// </summary>
        public event Action onEndSecondary;

        /// <summary>
        /// Begins a new input. If there is no primary input the new
        /// input will become the primary. If there is no secondary,
        /// then the new input will become the secondary. If both
        /// primary and secondary are active, nothing happens.
        /// </summary>
        /// <param name="input">New input to begin</param>
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
            else if(_secondaryInput == null)
            {
                _secondaryInput = input;
                if (onBeginSecondary != null)
                {
                    onBeginSecondary();
                }
            }
        }

        /// <summary>
        /// Ends an active input. If the input is the primary
        /// input, then both primary and secondary are ended.
        /// If it is the secondary input, just the secondary is
        /// ended. If the input is neither the primary or secondary
        /// nothing happens.
        /// </summary>
        /// <param name="input"></param>
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
            else if (_secondaryInput == input)
            {
                _secondaryInput = null;
                if (onEndSecondary != null)
                {
                    onEndSecondary();
                }
            }
        }

        /// <summary>
        /// Adds a new modifier to that is part of the calculation
        /// step during each update.
        /// </summary>
        /// <param name="modifier">The modifier to add</param>
        public void AddModifier(IModifierHandler modifier)
        {
            _modifiers.Add(modifier);
        }

        /// <summary>
        /// Removes a modifier to no longer be part of the calculation
        /// step.
        /// </summary>
        /// <param name="modifier">The modifier to remove</param>
        public void RemoveModifier(IModifierHandler modifier)
        {
            _modifiers.Remove(modifier);
        }

        /// <summary>
        /// Toggles a modifier by either adding it if it's not already,
        /// or removing it if it's already included.
        /// </summary>
        /// <param name="modifier">The modifier to toggle</param>
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
