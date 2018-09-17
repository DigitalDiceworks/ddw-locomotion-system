namespace NaturalLocomotion
{
    using UnityEngine;

    [RequireComponent(typeof(SteamVR_TrackedController))]
    public class JumpInput : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigRigidbody;
        [SerializeField] private ModifierHub _modifierHub;
        [SerializeField] private AirControlModifier _airControlModifier;
        [SerializeField] private JumpData _data;
        [SerializeField] private GroundHandlerBase _ground;

        private Vector3 _startPosition;
        private SteamVR_TrackedController _trackedController;

        private Vector3 _jumpStart;
        private float _jumpTimer;

        private void Start()
        {
            _trackedController = GetComponent<SteamVR_TrackedController>();
            _trackedController.PadUnclicked += PadUnclickedHandler;

            _ground.onLand += LandHandler;
        }

        private void Reset()
        {
            _rigRigidbody = GetComponentInParent<Rigidbody>();
            _modifierHub = GetComponentInParent<ModifierHub>();
            _airControlModifier = GetComponentInParent<AirControlModifier>();
            _ground = GetComponentInParent<GroundHandlerBase>();
        }

        private void PadUnclickedHandler(object sender, ClickedEventArgs e)
        {
            _jumpTimer = 0f;
            _jumpStart = transform.localPosition;
        }

        private void LandHandler()
        {
            _airControlModifier.isActive = false;
        }

        private void Update()
        {
            if (_jumpTimer > _data.jumpTimeout)
            {
                return;
            }

            _jumpTimer += Time.deltaTime;
            Vector3 delta = transform.localPosition - _jumpStart;
            if (delta.y >= _data.jumpDistance * _data.jumpDistance)
            {
                _airControlModifier.enabled = true;
                _rigRigidbody.velocity += delta / _data.jumpDistance * _data.jumpScale / _jumpTimer;
                _jumpTimer = Mathf.Infinity;
            }
        }
    }
}