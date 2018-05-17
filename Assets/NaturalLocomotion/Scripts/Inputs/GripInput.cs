namespace NaturalLocomotion
{
    using UnityEngine;

    /// <summary>
    /// Core Input style of Natural Locomotion using the Grip Buttons.
    /// Pressing the grip will activate and releasing the grip will deactivate the input.
    /// Input Vector is the delta between where our controller was when we pressed the grip
    /// button and now.
    /// </summary>
    [RequireComponent(typeof(SteamVR_TrackedController))]
    public class GripInput : NaturalInput
    {
        [Header("How far you need to move the controller before you reach the maximum value"), SerializeField]
        private float _distanceToMax = 0.5f;
        [Header("Whether or not we use the Y axis in our vector calculation"), SerializeField]
        private bool _ignoreYAxis = true;

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

        /// <summary>
        /// Returns a vector in the direction of where the user has moved the controller.
        /// The vector will have a max magnitude of 1 to keep in line with the standard.
        /// Uses distance to max to scale the resulting direction.
        /// Y will be zero if ignore Y axis is set to true.
        /// </summary>
        /// <returns></returns>
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
