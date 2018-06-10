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
    public class GripRotationInput : GripInput
    {
        [Header("WRITE ME BETTER"), SerializeField]
        private float _deadzone = 0.15f;

        private Plane _plane;

        protected override void GrippedHandler(object sender, ClickedEventArgs e)
        {
            base.GrippedHandler(sender, e);
            _plane = new Plane(-transform.right, _startPosition);
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public override Vector3 GetSecondary()
        {
            float distance = _plane.GetDistanceToPoint(transform.localPosition);
            if (Mathf.Abs(distance) - _deadzone < 0f)
            {
                return Vector3.zero;
            }
            if (distance > 0f)
            {
                distance -= _deadzone;
            }
            else
            {
                distance += _deadzone;
            }
            return Vector3.up * distance;
        }
    }
}
