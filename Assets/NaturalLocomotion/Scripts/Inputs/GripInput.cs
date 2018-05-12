namespace NaturalLocomotion
{
    using UnityEngine;

    [RequireComponent(typeof(SteamVR_TrackedController))]
    public class GripInput : NaturalInput
    {
        [SerializeField] private float _distanceToMax;
        [SerializeField] private bool _ignoreYAxis;

        private Vector3 _startPosition;

        protected override void Awake()
        {
            base.Awake();

            SteamVR_TrackedController trackedController = GetComponent<SteamVR_TrackedController>();
            trackedController.Gripped += GrippedHandler;
            trackedController.Ungripped += UngrippedHandler;
        }

        private void GrippedHandler(object sender, ClickedEventArgs e)
        {
            _startPosition = transform.localPosition;
            hub.BeginInput(this);
        }

        private void UngrippedHandler(object sender, ClickedEventArgs e)
        {
            hub.EndInput(this);
        }

        public override Vector3 GetVector()
        {
            Vector3 delta = transform.localPosition - _startPosition;
            if (_ignoreYAxis)
            {
                delta.y = 0f;
            }
            delta /= _distanceToMax;
            if (delta.sqrMagnitude > 1)
            {
                delta.Normalize();
            }
            return delta;
        }
    }
}
