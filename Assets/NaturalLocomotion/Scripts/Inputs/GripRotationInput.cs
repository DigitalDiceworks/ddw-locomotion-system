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

        private float _startingRotationAxis;

        protected override void GrippedHandler(object sender, ClickedEventArgs e)
        {
            base.GrippedHandler(sender, e);
            _startingRotationAxis = transform.localEulerAngles.y;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public override Vector3 GetSecondary()
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

            // turning rotation logic
            //float rotationAxis = transform.localEulerAngles.y;
            //float deltaAngle = Mathf.DeltaAngle(_startingRotationAxis, rotationAxis);
            //deltaAngle -= Mathf.Sign(deltaAngle) * _deadzone;
            //return new Vector3(0f, deltaAngle, 0f);
        }
    }
}
