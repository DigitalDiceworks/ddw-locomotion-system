namespace NaturalLocomotion
{
    using UnityEngine;

    public abstract class ModifierHandler : MonoBehaviour
    {
        protected ModifierHub hub { get; private set; }

        // a separate flag to enable and disable modifiers, 
        // this is so they can be active in the scene without being used
        public bool isActive { get; set; }

        protected virtual void Awake()
        {
            hub = GetComponentInParent<ModifierHub>();
            hub.Register(this);
            isActive = false; // default to off
        }

        public void Toggle()
        {
            isActive = !isActive;
        }

        public abstract float Calculate(float currentModifier);
    }
}