namespace NaturalLocomotion
{
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
    public class ModifierHub : MonoBehaviour
    {
        private List<ModifierHandler> _modifiers = new List<ModifierHandler>();
        public float modifier { get; private set; }

        public virtual void Register(ModifierHandler modifier)
        {
            _modifiers.Add(modifier);
        }

        private void Update()
        {
            modifier = 1f;
            foreach (ModifierHandler mod in _modifiers)
            {
                if (mod.isActive)
                {
                    modifier = mod.Calculate(modifier);
                }
            }
        }
    }
}
