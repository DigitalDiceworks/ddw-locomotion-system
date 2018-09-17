using NaturalLocomotion;
using UnityEngine;

[RequireComponent(typeof(SteamVR_TrackedController))]
public class FlyingInput : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigRigidbody;
    [SerializeField] private ModifierHub _modifierHub;
    [SerializeField] private SprintModifier _sprintModifier;
    [SerializeField] private AirControlModifier _airControlModifier;
    [SerializeField] private FlyingData _data;
    [SerializeField] private GroundHandlerBase _ground;

    private Vector3 _startPosition;
    private SteamVR_TrackedController _trackedController;
    private bool _padDown;

    private void Start()
    {
        _trackedController = GetComponent<SteamVR_TrackedController>();
        _trackedController.PadClicked += PadClickedHandler;
        _trackedController.PadUnclicked += PadUnclickedHandler;

        _ground.onLand += LandHandler;
    }

    private void Reset()
    {
        _rigRigidbody = GetComponentInParent<Rigidbody>();
        _modifierHub = GetComponentInParent<ModifierHub>();
        _sprintModifier = GetComponentInParent<SprintModifier>();
        _airControlModifier = GetComponentInParent<AirControlModifier>();
        _ground = GetComponentInParent<GroundHandlerBase>();
    }

    private void PadClickedHandler(object sender, ClickedEventArgs e)
    {
        if (_ground.isOnGround)
        {
            return;
        }

        _startPosition = transform.localPosition;
        _airControlModifier.isActive = false;
        _rigRigidbody.useGravity = false;
        _padDown = true;
    }

    private void PadUnclickedHandler(object sender, ClickedEventArgs e)
    {
        _rigRigidbody.velocity = Vector3.zero;
        _padDown = false;
    }

    private void LandHandler()
    {
        _padDown = false;
        _rigRigidbody.useGravity = true;
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
        _rigRigidbody.velocity = speed;
    }
}
