using UnityEngine;

namespace NaturalLocomotion
{
    public class ToggleSprint : HubConnector, IModifierHandler
    {
        [SerializeField] private float _sprintModifier;
        [SerializeField] private float _allowedSprintPressTime;

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

        public Vector3 ModifyDirection(Vector3 direction)
        {
            return direction * _sprintModifier;
        }
    }
}