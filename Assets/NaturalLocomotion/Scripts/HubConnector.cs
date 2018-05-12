namespace NaturalLocomotion
{
    using UnityEngine;

    public class HubConnector : MonoBehaviour
    {
        protected LocomotionHub hub { get; private set; }

        protected virtual void Awake()
        {
            hub = GetComponentInParent<LocomotionHub>();
        }
    }
}