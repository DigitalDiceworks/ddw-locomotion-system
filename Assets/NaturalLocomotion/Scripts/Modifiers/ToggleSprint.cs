namespace NaturalLocomotion
{
    using UnityEngine;

    /// <summary>
    /// Simple modifier that scales the input by the sprint modifier value.
    /// Sprint is toggled by tapping the secondary controllers activation.
    /// </summary>
    public class ToggleSprint : HubConnector, IModifierHandler
    {
        [Header("How much to scale the direction vector"), SerializeField]
        private float _sprintModifier = 2f;
        [Header("How quickly you need to press the secondary controller button"), SerializeField]
        private float _allowedSprintPressTime = 0.2f;

        private float _beginTime;

        protected override void Awake()
        {
            base.Awake();

            hub.onEndPrimary += EndPrimaryHandler;
            hub.onBeginSecondary += BeginSecondaryHandler;
            hub.onEndSecondary += EndSecondaryHandler;
        }

        private void EndPrimaryHandler()
        {
            hub.RemoveModifier(this);
        }

        private void BeginSecondaryHandler()
        {
            _beginTime = Time.time;
        }

        private void EndSecondaryHandler()
        {
            if (Time.time - _beginTime < _allowedSprintPressTime)
            {
                hub.ToggleModifier(this);
            }
        }

        /// <summary>
        /// Scale the vector by our modifier value
        /// </summary>
        /// <param name="direction">The input vector</param>
        /// <returns>Scaled vector based on our sprint modifier value</returns>
        public Vector3 ModifyDirection(Vector3 direction)
        {
            return direction * _sprintModifier;
        }
    }
}