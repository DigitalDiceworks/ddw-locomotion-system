namespace NaturalLocomotion
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
    public class LocomotionHub : MonoBehaviour
    {
        private List<ModifierHandler> _modifiers = new List<ModifierHandler>();
        private List<NaturalInput> _inputs = new List<NaturalInput>();
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public virtual void Register(NaturalInput input)
        {
            _inputs.Add(input);
        }

        internal void Register(ModifierHandler modifier)
        {
            _modifiers.Add(modifier);
        }

        private void Update()
        {
            float modifier = 1f;
            foreach(ModifierHandler mod in _modifiers)
            {
                if (mod.enabled)
                {
                    modifier = mod.Calculate(modifier);
                }
            }

            foreach (NaturalInput input in _inputs)
            {
                if (input.enabled)
                {
                    input.Calculate(_rigidbody, modifier);
                }
            }
        }
    }
}
