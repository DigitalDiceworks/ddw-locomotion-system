namespace NaturalLocomotion
{
    using UnityEngine;

    public abstract class NaturalInput : HubConnector
    {
        protected override void Awake()
        {
            base.Awake();

            hub.Register(this);
        }

        public abstract void Calculate(Rigidbody rigidbody, float modifier);
    }
}