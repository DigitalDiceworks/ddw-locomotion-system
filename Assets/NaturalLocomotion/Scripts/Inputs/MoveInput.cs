namespace NaturalLocomotion
{
    using UnityEngine;

    [RequireComponent(typeof(SteamVR_TrackedController))]
    public class MoveInput : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigRigidbody;
        [SerializeField] private ModifierHub _modifierHub;
        [SerializeField] private SprintModifier _sprintModifier;
        [SerializeField] private MoveData _data;
        [SerializeField] private GroundHandlerBase _ground;

        private Vector3 _startPosition;
        private SteamVR_TrackedController _trackedController;
        private bool _padDown;

        private void Start()
        {
            _trackedController = GetComponent<SteamVR_TrackedController>();
            _trackedController.PadClicked += PadClickedHandler;
            _trackedController.PadUnclicked += PadUnclickedHandler;
        }

        private void Reset()
        {
            _rigRigidbody = GetComponentInParent<Rigidbody>();
            _modifierHub = GetComponentInParent<ModifierHub>();
            _sprintModifier = GetComponentInParent<SprintModifier>();
            _ground = GetComponentInParent<GroundHandlerBase>();
        }

        private void PadClickedHandler(object sender, ClickedEventArgs e)
        {
            _startPosition = transform.localPosition;
            _padDown = true;
        }

        private void PadUnclickedHandler(object sender, ClickedEventArgs e)
        {
            _sprintModifier.isActive = false;
            _padDown = false;
        }

        private void Update()
        {
            if (!_padDown)
            {
                return;
            }

            _sprintModifier.isActive = _trackedController.controllerState.rAxis0.y > _data.minimumSprintValue;

            Vector3 delta = transform.localPosition - _startPosition;
            Vector3 speed = delta * _data.speedModifier * _modifierHub.modifier;
            speed.y = _rigRigidbody.velocity.y;
            if (_ground.isOnGround)
            {
                _rigRigidbody.velocity = speed;
            }
            else
            {
                speed.y = 0f;
                _rigRigidbody.velocity += speed;
            }
        }
    }
}