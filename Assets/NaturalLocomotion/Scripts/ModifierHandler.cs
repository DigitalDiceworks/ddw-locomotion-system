namespace NaturalLocomotion
{
    public abstract class ModifierHandler : HubConnector
    {
        protected override void Awake()
        {
            base.Awake();

            enabled = false;
            hub.Register(this);
        }

        public abstract float Calculate(float currentModifier);
    }
}